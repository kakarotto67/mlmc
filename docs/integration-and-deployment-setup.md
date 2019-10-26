# Integration and Deployment
The information below describes of how the CI/CD process was set up for MLMC project.

## Setup Continuous Integration pipeline in Azure DevOps
- Created pipeline in Azure DevOps - MLMC project with name `Run Docker Compose`
  - Agent - ubuntu
  - Sources - GitHub's `mlmc` project -> `master` branch
- Added few steps to that pipeline
  - Install Docker action
  - Run Docker Compose for MongoDB and RabbitMq (file `docker-compose.infrastructure.yml`, command - `build --no-cache`)
  - Run Docker Compose for Microservices (file `docker-compose.app.yml`, command - `build --no-cache`)
- Added triggers for that pipeline
  - On commit to `master`
  - On pull request to `master`


## Setup Azure Kubernetes Service cluster and Deploy the App
TBD

## Setup Continuous Deployment pipeline in Azure DevOps
TBD
