FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /app

ENV PATH ~/.dotnet/tools:$PATH

COPY *.csproj /app
RUN dotnet restore
RUN dotnet tool install --global dotnet-ef --version 3.1.1
