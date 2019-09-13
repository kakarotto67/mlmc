import { GpsLocation } from './gpsLocation.model';

export class DeploymentPlatform {
  constructor(
    public deploymentPlatformId: number,
    public serviceIdentityNumber: string,
    public name: string,
    public location: GpsLocation
  ) {}
}