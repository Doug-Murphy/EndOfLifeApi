FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EndOfLifeApi/EndOfLifeApi.csproj", "EndOfLifeApi/"]
RUN dotnet restore "EndOfLifeApi/EndOfLifeApi.csproj"
COPY . .
WORKDIR "/src/EndOfLifeApi"
RUN dotnet build "EndOfLifeApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EndOfLifeApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EndOfLifeApi.dll"]
