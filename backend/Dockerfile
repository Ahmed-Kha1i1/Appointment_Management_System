#first-stage base image
FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /sourse

COPY **/*.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish AMS.API/AMS.API.csproj -c release -o /app

#final stage image 
FROM  mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app .

ENTRYPOINT [ "dotnet", "AMS.API.dll" ]