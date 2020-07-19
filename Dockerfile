FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

COPY ./src/ZPProductManagement.Application/ZPProductManagement.Application.csproj ./src/ZPProductManagement.Application/ZPProductManagement.Application.csproj
COPY ./src/ZPProductManagement.Common/ZPProductManagement.Common.csproj ./src/ZPProductManagement.Common/ZPProductManagement.Common.csproj
COPY ./src/ZPProductManagement.Domain/ZPProductManagement.Domain.csproj ./src/ZPProductManagement.Domain/ZPProductManagement.Domain.csproj
COPY ./src/ZPProductManagement.Web/ZPProductManagement.Web.csproj ./src/ZPProductManagement.Web/ZPProductManagement.Web.csproj

COPY ZPProductManagement.sln .

RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT [ "dotnet", "ZPProductManagement.Web.dll" ]
