FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["/SecretsSharing/SecretsSharing.csproj", "SecretsSharing/"]
COPY ["/SecretsSharing.BL/SecretsSharing.BL.csproj", "SecretsSharing.BL/"]
COPY ["/SecretsSharing.DAL/SecretsSharing.DAL.csproj", "SecretsSharing.DAL/"]
RUN dotnet restore "SecretsSharing/SecretsSharing.csproj"
COPY . .
WORKDIR "/src/SecretsSharing"
RUN dotnet build "SecretsSharing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecretsSharing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecretsSharing.dll"]
