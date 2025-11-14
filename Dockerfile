FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder
WORKDIR /app
COPY MOVEO-backend/*.csproj MOVEO-backend/
RUN dotnet restore ./MOVEO-backend/
COPY . .
RUN dotnet publish ./MOVEO-backend -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet"m "MOVEO-backend.dll"]