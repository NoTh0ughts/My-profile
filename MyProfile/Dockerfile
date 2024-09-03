FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 44492
EXPOSE 50000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyProfile/MyProfile.csproj", "MyProfile/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Migrations/Migrations.csproj", "Migrations/"]
COPY ["Tests/Tests.csproj", "Tests/"]
RUN dotnet restore "MyProfile/MyProfile.csproj"
COPY . .

WORKDIR "/src/MyProfile"

RUN dotnet build "MyProfile.csproj" -c Release -o /app/build
RUN dotnet build "../Tests/Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyProfile.csproj" -c Release -o /app/publish
RUN dotnet publish "../Tests/Tests.csproj" -c Release -o /app/build

RUN dotnet test "../Tests/Tests.csproj"

FROM node AS node-builder
WORKDIR /node

COPY MyProfile/ClientApp/package*.json /node
RUN npm ci

COPY MyProfile/ClientApp /node
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=node-builder /node/build ./wwwroot

ENV ASPNETCORE_URLS=http://0.0.0.0:50000

ENTRYPOINT ["dotnet", "MyProfile.dll"]