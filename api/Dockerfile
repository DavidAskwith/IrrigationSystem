FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /app

RUN groupadd -r dockeruser && useradd --no-log-init -r -g dockeruser dockeruser
RUN mkdir /home/dockeruser

ENV PATH ~/.dotnet/tools:$PATH

COPY ./src/*.csproj /app

RUN dotnet restore
RUN dotnet tool install --global dotnet-ef --version 3.1.1

RUN chown -R dockeruser /app
RUN chown -R dockeruser /home/dockeruser
USER dockeruser

VOLUME /app
