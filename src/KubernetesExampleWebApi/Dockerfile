FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 54331
EXPOSE 44386

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["KubernetesExampleWebApi.csproj", "."]
RUN dotnet restore "KubernetesExampleWebApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "KubernetesExampleWebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "KubernetesExampleWebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KubernetesExampleWebApi.dll"]