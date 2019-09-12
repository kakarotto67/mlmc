import { Injectable } from "@angular/core";
import { RestService } from "../../service/rest.service";
import { Observable } from "rxjs";
import { Missile, MissileStatus } from "../models/missile.model";
import { environment } from "src/environments/environment";
import { DeploymentPlatform } from "../models/deploymentPlatform.model";
import { HttpHeaders } from "@angular/common/http";

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
  public createMissile(missile: Missile) {
    if (!missile) {
      return;
    }

    var url = `${this.baseUri}/missiles`;
    console.log(url);
    var body = {
      deploymentPlatformId: missile.deploymentPlatformId,
      name: missile.name,
      type: missile.type
    };
    var headers = new HttpHeaders({ "Content-Type": "application/json" });

    // Subscribe needs to be called so the request will be performed
    this.restService.sendRequest("POST", url, body, headers).subscribe();
  }

  // Get list of all available deployment platforms
  public getDeploymentPlatforms(): Observable<DeploymentPlatform[]> {
    var url = `${this.baseUri}/deploymentplatforms`;

    return this.restService.sendRequest<DeploymentPlatform[]>("GET", url);
  }
}
