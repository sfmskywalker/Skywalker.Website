FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/Skywalker.Website/*.csproj ./src/Skywalker.Website/
COPY src/Modules/Skywalker.OrchardCore.ContentExtensions/*.csproj ./src/Modules/Skywalker.OrchardCore.ContentExtensions/
COPY src/Modules/Skywalker.OrchardCore.Gravatar/*.csproj ./src/Modules/Skywalker.OrchardCore.Gravatar/
COPY src/Themes/TheMediumTheme/*.csproj ./src/Themes/TheMediumTheme/
COPY Skywalker.Website.sln ./
COPY NuGet.config ./
RUN dotnet restore ./Skywalker.Website.sln

# Copy everything else and build
COPY src ./src

# Install NodeJS + NPM
RUN apt-get install --yes curl
RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

# Run NPM
WORKDIR src/Themes/TheMediumTheme
RUN npm install
RUN npm run build

# Publish the solution
WORKDIR /app
RUN dotnet publish ./Skywalker.Website.sln -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

# ------------------------
# SSH Server support
# ------------------------
RUN apt-get update \ 
  && apt-get install -y --no-install-recommends openssh-server \
  && echo "root:Docker!" | chpasswd

WORKDIR /app
EXPOSE 80 2222
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Skywalker.Website.dll"]