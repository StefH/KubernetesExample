FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 58904
EXPOSE 44387

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["KubernetesExampleWebApi-Linux.csproj", "."]
RUN dotnet restore "KubernetesExampleWebApi-Linux.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "KubernetesExampleWebApi-Linux.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "KubernetesExampleWebApi-Linux.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KubernetesExampleWebApi.dll"]