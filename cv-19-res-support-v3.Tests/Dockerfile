FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./cv19ResRupportV3.sln ./
COPY ./cv19ResRupportV3/cv19ResRupportV3.csproj ./cv19ResRupportV3/
COPY ./cv19ResRupportV3.Tests/cv19ResRupportV3.Tests.csproj ./cv19ResRupportV3.Tests/

RUN dotnet restore ./cv19ResRupportV3/cv19ResRupportV3.csproj
RUN dotnet restore ./cv19ResRupportV3.Tests/cv19ResRupportV3.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out cv19ResRupportV3.Tests/cv19ResRupportV3.Tests.csproj

CMD dotnet test
