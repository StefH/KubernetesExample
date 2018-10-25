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

### 4. Push
```
docker push sheyenrath/kubernetes-example:latest
```

## Common
### 5. Test using curl
```
curl http://localhost:54331/api/values
curl http://localhost:54331/api/values/0
curl http://localhost:54331/api/values/1
```


# Kubernetes
## Commands
### 0. Go to your cluster 

### 1. Create the solution
```
kubectl apply -f kubernetes_deployment.yml
```

### 2a. Get service information
```
kubectl get svc
>>
NAME                                TYPE           CLUSTER-IP     EXTERNAL-IP   PORT(S)        AGE
kubernetes                          ClusterIP      10.96.0.1      <none>        443/TCP        56m
kubernetes-example-service-redis    ClusterIP      10.108.44.31   <none>        6379/TCP       43m
kubernetes-example-service-webapi   LoadBalancer   10.110.194.8   <pending>     80:30750/TCP   1m
```

### 2b. Describe all services
```
kubectl describe svc
```

### 3. Get all Pods
```
kubectl get pods
>>
NAME                                                    READY     STATUS    RESTARTS   AGE
kubernetes-example-deployment-redis-57cf4bdcb8-5897m    1/1       Running   0          53m
kubernetes-example-deployment-webapi-7bf95cf4b8-pnpmx   1/1       Running   0          3m
```

### 4. Describe all pods
```
kubectl describe pods

```

### 5. Test using curl
test redis connectionstring via ip-address from the pod (**TODO**)
```
curl http://10.110.194.8/api/test/10-40-0-1.default.cluster.local
```

test redis connectionstring via ip-address from the kubernetes-example-service-webapi service (**TODO**)
```
curl ???
```

test redis connectionstring via svc
**TODO** : my-svc.my-namespace.svc.cluster.local

non cached
```
curl http://10.110.194.8/api/values
curl http://10.110.194.8/api/values/0
curl http://10.110.194.8/api/values/1
```

cached
```
curl http://10.110.194.8/api/valuescached
curl http://10.110.194.8/api/valuescached/0
curl http://10.110.194.8/api/valuescached/1
```