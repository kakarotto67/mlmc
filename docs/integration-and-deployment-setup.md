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
- All these files have to be manually configured to include all the containers you have
