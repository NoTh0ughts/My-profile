FROM alpine:3.14
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG TARGETARCH
WORKDIR /app

EXPOSE 44492
EXPOSE 50000
WORKDIR /src
COPY ["./MyProfile/MyProfile.csproj", "MyProfile/"]
COPY ["./Data/Data.csproj", "Data/"]
COPY ["./Migrations/Migrations.csproj", "Migrations/"]
COPY ["./Tests/Tests.csproj", "Tests/"]
RUN dotnet restore "MyProfile/MyProfile.csproj" -a $TARGETARCH
COPY . .

WORKDIR /src

RUN dotnet build "./MyProfile/MyProfile.csproj" -c Release -o /app/build
RUN dotnet build "./Tests/Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./MyProfile/MyProfile.csproj" --no-restore -a $TARGETARCH -c Release -o /app/publish
RUN dotnet publish "./Tests/Tests.csproj" --no-restore -a $TARGETARCH -c Release -o /app/build 

RUN dotnet test "./Tests/Tests.csproj"

FROM node:20-slim AS node_builder
WORKDIR /node
ENV PNPM_HOME="/pnpm"
ENV PATH="$PNPM_HOME:$PATH"
RUN corepack enable

COPY MyProfile/ClientApp/pnpm-lock.yaml /node
COPY MyProfile/ClientApp/package*.json /node
RUN --mount=type=cache,id=pnpm,target=/pnpm/store pnpm install --prod --frozen-lockfile

COPY MyProfile/ClientApp/. /node
RUN --mount=type=cache,id=pnpm,target=/pnpm/store pnpm install --frozen-lockfile
RUN pnpm run build

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=node_builder /node/build ./wwwroot

ENV ASPNETCORE_URLS=http://0.0.0.0:20000

ENTRYPOINT ["dotnet", "MyProfile.dll"]