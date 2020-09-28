#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RegistrationService/Web/SingingPractice.RegistrationService.Web.Api/SingingPractice.RegistrationService.Web.Api.csproj", "RegistrationService/Web/SingingPractice.RegistrationService.Web.Api/"]
RUN dotnet restore "RegistrationService/Web/SingingPractice.RegistrationService.Web.Api/SingingPractice.RegistrationService.Web.Api.csproj"
COPY . .
WORKDIR "/src/RegistrationService/Web/SingingPractice.RegistrationService.Web.Api"
RUN dotnet build "SingingPractice.RegistrationService.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SingingPractice.RegistrationService.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SingingPractice.RegistrationService.Web.Api.dll"]