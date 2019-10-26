# Integration and Deployment
The information below describes of how the CI/CD process was set up for MLMC project.

## Setup Continuous Integration pipeline in Azure DevOps
- Created pipeline in Azure DevOps - MLMC project with name `Run Docker Compose`
  - Agent - ubuntu-16.04
  - Sources - GitHub's `mlmc` project -> `master` branch
- Added few steps to that pipeline
  - Install Docker action
  - Run Docker Compose for MongoDB and RabbitMq (file `docker-compose.infrastructure.yml`, command - `build --no-cache`)
  - Run Docker Compose for Microservices (file `docker-compose.app.yml`, command - `build --no-cache`)
- Added triggers for that pipeline
  - On commit to `master`
  - On pull request to `master`

## Setup Continuous Integration and Deployment pipeline for the Azure Kubernetes Service cluster

### 1. Create Azure Container Registry
- Created `mlmc-containers` resource group
- Created Azure Container Registry (ACR) service with name `mlmccontainers` in the `mlmc-containers` resource group

### 2. Create Azure Kubernetes Service cluster
- Created `mlmc-cluster` resource group
- Created AKS cluster with name `mlmccluster` in the `mlmc-cluster` resource group

### 3. Setup the CI/CD YAML-based pipeline for AKS
- Create YAML-based pipeline
- Choose GitHub sources and repo
- Choose Deploy to Azure Kubernetes Service
  - Choose cluster, namespace, container registry, container name and port
- The YAML will be generated, the following files will be added to your repository
  - `azure-pipelines.yaml` - describes all the steps needed to build containers and deploy them to AKS
  - `manifests` folder
    - `deployment.yaml` - all the services to be deployed to AKS
    - `service.yaml` - services endpoints setup in the AKS

Notes:
- All these files have to be manually configured to include all the containers you have
- Resulted pipeline will contain Build Stages and Deploy Stages
- The `azure-pipelines.yaml` files describes all the steps required
  - Deploy MongoDB and RabbitMq to the AKS
  - Build all the containers to ACR and then deploy them to AKS
- The `azure-pipelines.yaml` file uses different k8s settings files from `manifests` folder to deploy and run services on AKS, but, generally speaking, those files can be combined into single one and then pipeline jobs/tasks have to be modified accordingly
- There are other ways of how to configure the deployment to AKS
  - Create pipeline using classic editor and add all required jobs/tasks to build containers, push them to ACR and then deploy them to AKS cluster
  - Use Azure Portal -> AKS cluster page -> Deployment Center (preview) page -> Setup CI/CD pipeline from there

## Further Investigation
- How to deploy MongoDB and RabbitMq containers directly from DockerHub into AKS cluster
- How to reach any service after it is deployed to AKS cluster
  - External IP is logged in the respective pipeline stage/job or can be get using kubectl console command (`kubectl get services mgcc-service`)
  - I guess, service can be reached only if there is at least one related Pod with status Ok (but not Warn, Done, etc., which happens due to errors like no connection to MongoDB or RabbitMq)
