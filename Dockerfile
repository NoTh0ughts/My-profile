# Use the official node image as the base image for the client app
FROM node:16 AS client-build

# Set the working directory to /client-app
WORKDIR /client-app

# Copy the client app files to the Docker image
COPY MyProfile/ClientApp/package*.json ./
COPY MyProfile/ClientApp/public ./public
COPY MyProfile/ClientApp/src ./src

# Install the dependencies for the client app
RUN npm install

# Build the client app
RUN npm run build

# Use the official .NET SDK image as the base image for the server app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS server-build

# Set the working directory to /app
WORKDIR /app


# Restore the NuGet packages
RUN dotnet restore MyProfile/MyProfile.csproj

# Copy the rest of the source code
COPY . .

# Copy the built client app files to the server app
COPY --from=client-build /client-app/build ./ClientApp/build

# Build and publish the server app
RUN dotnet publish -c Release -o /app/publish

# Use the official ASP.NET Core runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory to /app
WORKDIR /app

# Copy the published output from the build image to the runtime image
COPY --from=server-build /app/publish .

# Set the environment variables for the ASP.NET Core application
ENV ASPNETCORE_URLS=http://+:80
ENV ConnectionStrings__MyProfile="temp"

# Expose port 80 for the ASP.NET Core application
EXPOSE 80

# Start the ASP.NET Core application
ENTRYPOINT ["dotnet", "MyProfile.dll"]
