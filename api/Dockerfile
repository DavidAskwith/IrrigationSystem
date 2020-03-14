FROM mcr.microsoft.com/dotnet/core/sdk:3.1
ENV PATH ~/.dotnet/tools:$PATH

WORKDIR /app/src
COPY ./src/*.csproj ./
RUN dotnet restore

WORKDIR /app/test
COPY ./test/*.csproj ./
RUN dotnet restore

RUN dotnet tool install --global dotnet-ef --version 3.1.1
