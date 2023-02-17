FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 44492
EXPOSE 44493
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY package*.json ./

RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

WORKDIR /src
COPY ["MyProfile/MyProfile.csproj", "MyProfile/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Migrations/Migrations.csproj", "Migrations/"]
RUN dotnet restore "MyProfile/MyProfile.csproj"
COPY . .
WORKDIR "/src/MyProfile"
RUN dotnet build "MyProfile.csproj" -c Release -o /app/build

FROM node:10-alpine
WORKDIR /app/
COPY package*.json ./
RUN npm install

FROM build AS publish
RUN dotnet publish "MyProfile.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyProfile.dll"]
