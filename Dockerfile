FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY /src/ModCidadao.csproj .
COPY /src/appsettings.json .
COPY /src/appsettings.Homologacao.json .
RUN dotnet clean
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ModCidadao.dll"]
EXPOSE 8080
