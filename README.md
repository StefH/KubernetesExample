# KubernetesExample
A example project about Kubernetes and C# WebApi

# Docker
## Windows Nano Commands
### 0. Go to the `KubernetesExampleWebApi` folder
```
cd src\KubernetesExampleWebApi
```

### 1. Build
```
docker build -t sheyenrath/kubernetes-example-nano -f .\Dockerfile .
```

### 2. Delete dangling images (optional)
```
docker rmi $(docker images -f "dangling=true" -q)
```

### 3. Run
```
docker run -it -p 54331:80 --rm sheyenrath/kubernetes-example-nano
```

#### 3a. Test using curl
```
curl http://localhost:54331/api/values
```

#### 3b. Stop
Just press `CTRL-C` to stop the web-api.

### 4. Push
```
docker push sheyenrath/kubernetes-example-nano:latest
```