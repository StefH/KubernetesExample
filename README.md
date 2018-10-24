# KubernetesExample
A example project about Kubernetes and C# WebApi

# Docker
## Common
### 0. Go to the `KubernetesExampleWebApi` folder
```
cd src\KubernetesExampleWebApi
```

## Linux Commands
### 1. Build
```
docker build -t sheyenrath/kubernetes-example -f .\Dockerfile.linux .
```

### 2. Delete dangling images (optional)
```
docker rmi $(docker images -f "dangling=true" -q)
```

### 3. Run
```
docker run -it -p 54331:80 --rm sheyenrath/kubernetes-example
```

#### 3a. Test using curl
```
curl http://localhost:54331/api/values
```

#### 3b. Stop
Just press `CTRL-C` to stop the web-api.

### 4. Push
```
docker push sheyenrath/kubernetes-example:latest
```

## Windows Nano Commands
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

# Kubernetes
## Commands
### 0. Go to your cluster 

### 1. Create the solution
```
kubectl apply -f kubernetes.yml
```

### 2. Get all Pods
```
kubectl get pods
>>
NAME                        READY     STATUS    RESTARTS   AGE
kubernetes-example-redis    1/1       Running   0          8m
kubernetes-example-webapi   1/1       Running   0          18m
```

### 3a. Describe the `kubernetes-example-webapi` and `kubernetes-example-redis` pod
```
kubectl describe pod kubernetes-example-webapi

kubectl describe pod kubernetes-example-redis
```

### 4. Get service information
```
kubectl get svc
>>
NAME                           TYPE           CLUSTER-IP      EXTERNAL-IP   PORT(S)        AGE
kubernetes                     ClusterIP      10.96.0.1       <none>        443/TCP        42m
kubernetes-example-subdomain   LoadBalancer   10.104.240.18   <pending>     80:30011/TCP   4m
```

### 5. Test using curl
```
curl http://10.104.240.18/api/values
```