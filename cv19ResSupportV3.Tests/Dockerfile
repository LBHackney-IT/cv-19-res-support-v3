FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./cv19ResSupportV3.sln ./
COPY ./cv19ResSupportV3/cv19ResSupportV3.csproj ./cv19ResSupportV3/
COPY ./cv19ResSupportV3.Tests/cv19ResSupportV3.Tests.csproj ./cv19ResSupportV3.Tests/

RUN dotnet restore ./cv19ResSupportV3/cv19ResSupportV3.csproj
RUN dotnet restore ./cv19ResSupportV3.Tests/cv19ResSupportV3.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out cv19ResSupportV3.Tests/cv19ResSupportV3.Tests.csproj

CMD dotnet test
