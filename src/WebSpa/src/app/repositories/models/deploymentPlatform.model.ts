export class DeploymentPlatform {
  constructor(
    public deploymentPlatformId: number,
    public serviceIdentityNumber: string,
    public name: string,
    public location: Location
  ) {}
}

class Location {
    constructor(
        public longitude: number,
        public latitude: number
      ) {}
}
