FROM mcr.microsoft.com/dotnet/core/sdk:3.1.102 AS build
WORKDIR /app

# Copy everything and build
COPY . ./
RUN dotnet publish SoccerStatistics.Api.WebApi -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.1
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT Development
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet", "SoccerStatistics.Api.WebApi.dll"]