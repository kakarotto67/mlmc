import { Injectable } from "@angular/core";
import { RestService } from "../../service/rest.service";
import { Observable } from "rxjs";
import { Missile, MissileStatus } from "../models/missile.model";
import { environment } from "src/environments/environment";
import { DeploymentPlatform } from "../models/deploymentPlatform.model";
import { HttpHeaders } from "@angular/common/http";
import { GpsLocation } from "../models/gpsLocation.model";

@Injectable()
export class OperationRepository {
  private baseUri = environment.services.operation.uri;

  constructor(private restService: RestService) {}

  // Get list of filtered missiles
  public getMissiles(status: MissileStatus): Observable<Missile[]> {
    var url =
      status >= 0 ? `${this.baseUri}/missiles?status=${status}` : `${this.baseUri}/missiles`;

    return this.restService.sendRequest<Missile[]>("GET", url);
  }

  // Create new missile
  public createMissile(missile: Missile) {
    if (!missile) {
      return;
    }

    var url = `${this.baseUri}/missiles`;
    var body = {
      deploymentPlatformId: missile.deploymentPlatformId,
      name: missile.name,
      // For some reason missile.type might come as string ignoring the fact that it is specified to be a number
      type: parseInt(missile.type.toString())
    };
    var headers = new HttpHeaders({ "Content-Type": "application/json" });

    // Subscribe needs to be called so the request will be performed
    this.restService.sendRequest("POST", url, body, headers).subscribe();
  }

  // Launch a missile
  public launchMissile(serviceIdentityNumber: string, targetLocation: GpsLocation) {
    if (!serviceIdentityNumber || !targetLocation) {
      return;
    }

    var url = `${this.baseUri}/launchmissile`;
    var body = {
      serviceIdentityNumber: serviceIdentityNumber,
      targetLocation: targetLocation
    };
    var headers = new HttpHeaders({ "Content-Type": "application/json" });

    // Subscribe needs to be called so the request will be performed
    this.restService.sendRequest("POST", url, body, headers).subscribe();
  }

  // Decommission a missile
  public decommissionMissile(serviceIdentityNumber: string) {
    if (!serviceIdentityNumber) {
      return;
    }

    // TODO
    // Currently missile is deleted on Decommission,
    // but it is enough to just update its status to 'Decommissioned'.
    var url = `${this.baseUri}/missiles`;
    var body = {
      serviceIdentityNumber: serviceIdentityNumber
    };
    var headers = new HttpHeaders({ "Content-Type": "application/json" });

    // Subscribe needs to be called so the request will be performed
    this.restService.sendRequest("DELETE", url, body, headers).subscribe();
  }

  // Get list of all available deployment platforms
  public getDeploymentPlatforms(): Observable<DeploymentPlatform[]> {
    var url = `${this.baseUri}/deploymentplatforms`;

    return this.restService.sendRequest<DeploymentPlatform[]>("GET", url);
  }
}
