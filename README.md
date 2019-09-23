# Missile Launch & Monitoring Center

A microservices-based app that shows list of missiles with possibilities to launch them and track their realtime movement on the map.

Current state: In Active Development.

## Documentation
Summary
- [Architectural diagram](https://github.com/kakarotto67/mlmc/blob/master/README.md#architectural-diagram)
- [Make it run](https://github.com/kakarotto67/mlmc/blob/master/README.md#make-it-run)
- [Architectural decisions log](https://github.com/kakarotto67/mlmc/blob/master/README.md#architectural-decisions-log)
- [Other](https://github.com/kakarotto67/mlmc/blob/master/README.md#other)

### Architectural diagram
[ Will be updated soon ]


![Architectural reference diagram](https://github.com/kakarotto67/mlmc/blob/master/MLMC_Design_1.0.png)


### Make it run
#### Prerequisities
- [.NET Core](https://dotnet.microsoft.com/download)
- [Docker for Desktop](https://www.docker.com/products/docker-desktop)
- [NodeJS and NPM](https://www.npmjs.com/get-npm)
- [Angular CLI](https://angular.io/cli)

#### Steps
[ Will be simplified soon ]

1. Run MongoDb and RabbitMQ under `{root}` dir using following command:

`docker-compose -f "docker-compose.yml" up --build`

2. Run WebSPA Angular client under `{root}\src\WebSpa` dir using following command:

`ng serve`

3. Run Operation service under `{root}\src\Services\Operation` dir using following command:

`dotnet run`

4. Run MGCC.API service under `{root}\src\Services\MGCC.Api` dir using following command:

`dotnet run`

#### Notes
You might need to update these config files and set correct URIs to respective services:
- `{root}\src\WebSpa\src\environments`
  - environment.ts - `services:operation:uri` setting
  - environment.prod.ts - `services:operation:uri` setting
- `{root}\src\Services\MGCC.Api`
  - appsettings.json - `ApiPath:Mgcc` and `WebSpaUri` settings
  - appsettings.Development.json - `AiPath:Mgcc` and `WebSpaUri` settings
- `{root}\src\WebSpa\src\assets\js`
  - config.js - `context.mgccUri` setting

### Architectural decisions log
- For architectural decisions see list of applied [ADR records](https://github.com/kakarotto67/mlmc/blob/master/docs/adr/index.md).

### Other
- For more documentation check [docs](https://github.com/kakarotto67/mlmc/blob/master/docs) folder.
