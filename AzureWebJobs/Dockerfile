FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["AzureWebJobs/AzureWebJobs.csproj", "AzureWebJobs/"]
RUN dotnet restore "AzureWebJobs/AzureWebJobs.csproj"
COPY . .
WORKDIR "/src/AzureWebJobs"
RUN dotnet build "AzureWebJobs.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AzureWebJobs.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AzureWebJobs.dll"]