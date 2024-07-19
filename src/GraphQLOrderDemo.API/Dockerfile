FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:6060
EXPOSE 6060

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GraphQLOrderDemo.API/GraphQLOrderDemo.API.csproj", "src/GraphQLOrderDemo.API/"]
COPY ["src/GraphQLOrderDemo.ServiceDefaults/GraphQLOrderDemo.ServiceDefaults.csproj", "src/GraphQLOrderDemo.ServiceDefaults/"]
RUN dotnet restore "src/GraphQLOrderDemo.API/GraphQLOrderDemo.API.csproj"
COPY . .
WORKDIR "/src/src/GraphQLOrderDemo.API"
RUN dotnet build "GraphQLOrderDemo.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GraphQLOrderDemo.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQLOrderDemo.API.dll"]
