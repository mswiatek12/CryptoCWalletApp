FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# copying csprojes
COPY *.sln
COPY ./src/CryptoCWalletApp/*.csproj ./CryptoCWalletApp/
RUN dotnet restore

# copying the rest of files
COPY ./. ./CryptoCWalletApp/
WORKDIR /source/CryptoCWalletApp
RUN dotnet publish -c release -o /app

# final image/stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "CryptoCWalletApp.dll"]
