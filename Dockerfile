# Dockerfile – CHẠY NGON 100% TRÊN WINDOWS + VS2022 + .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

RUN mkdir -p /tmp/nuget/packages && \
    mkdir -p /tmp/nuget/fallback && \
    dotnet nuget locals all --clear

ENV NUGET_PACKAGES=/tmp/nuget/packages
ENV NUGET_FALLBACK_PACKAGES=/tmp/nuget/fallback
ENV DOTNET_NOLOGO=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true

# Copy solution + csproj
COPY *.sln .
COPY PraticeV1.API/PraticeV1.API.csproj ./PraticeV1.API/
COPY PracticeV1.Application/PracticeV1.Application.csproj ./PracticeV1.Application/
COPY PracticeV1.Domain/PracticeV1.Domain.csproj ./PracticeV1.Domain/
COPY PracticeV1.Infrastructure/PracticeV1.Infrastructure.csproj ./PracticeV1.Infrastructure/

# Restore – BẮT BUỘC DÙNG --no-cache + source rõ ràng
RUN dotnet restore "PraticeV1.API/PraticeV1.API.csproj" \
    --source https://api.nuget.org/v3/index.json \
    --no-cache \
    --verbosity normal

# Copy the rest of the repo
COPY . .

WORKDIR /src/PraticeV1.API

RUN dotnet restore

# Publish (do not use --no-restore)
RUN dotnet publish "PraticeV1.API.csproj" -c Release -o /app/publish --self-contained false /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PraticeV1.API.dll"]