FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY . .

RUN dotnet restore "./API/Dispo.API/Dispo.API.csproj"
RUN dotnet publish "./API/Dispo.API/Dispo.API.csproj" -c Release -o /app/out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "Dispo.API.dll"]