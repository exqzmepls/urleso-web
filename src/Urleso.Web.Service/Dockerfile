FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/Directory.Build.props", ""]
COPY ["src/Directory.Packages.props", ""]
COPY ["src/Urleso.Web.Service/Urleso.Web.Service.csproj", "Urleso.Web.Service/"]
COPY ["src/Urleso.Web/Urleso.Web.csproj", "Urleso.Web/"]
COPY ["src/Urleso.Api.Client/Urleso.Api.Client.csproj", "Urleso.Api.Client/"]
RUN dotnet restore "Urleso.Web.Service/Urleso.Web.Service.csproj"
COPY src/ .
WORKDIR "/src/Urleso.Web.Service"
RUN dotnet publish "Urleso.Web.Service.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app
USER $APP_UID
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Urleso.Web.Service.dll"]
