import { Injectable } from "@angular/core";
import { RestService } from "../../service/rest.service";
import { Observable } from "rxjs";
import { Missile, MissileStatus } from "../models/missile.model";
import { environment } from "src/environments/environment";
import { DeploymentPlatform } from "../models/deploymentPlatform.model";

@Injectable()
export class OperationRepository {
  private baseUri = environment.services.operation.uri;

  constructor(private restService: RestService) {}

  // Get list of filtered missiles
  public getMissiles(status: MissileStatus): Observable<Missile[]> {
    var url = status ? `${this.baseUri}/missiles?status=${status}` : `${this.baseUri}/missiles`;

    return this.restService.sendRequest<Missile[]>("GET", url);
  }

  // Create new missile
  public createMissile(missile: Missile): Observable<Missile> {
    if (!missile) {
      return;
    }

    var url = `${this.baseUri}/missiles?deploymentPlatformId=${missile.deploymentPlatformId}&name=${missile.name}&type=${missile.type}`;
    return this.restService.sendRequest<Missile>("POST", url, missile);
  }

  // Get list of all available deployment platforms
  public getDeploymentPlatforms(): Observable<DeploymentPlatform[]> {
    var url = `${this.baseUri}/deploymentplatforms`;

    return this.restService.sendRequest<DeploymentPlatform[]>("GET", url);
  }
}
