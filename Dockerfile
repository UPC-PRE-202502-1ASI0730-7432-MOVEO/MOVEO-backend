FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder
WORKDIR /app
COPY Moveo_backend/*.csproj Moveo_backend/
RUN dotnet restore ./Moveo_backend
COPY . .
RUN dotnet publish ./Moveo_backend -c Release -o out /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Moveo_backend.dll"]
