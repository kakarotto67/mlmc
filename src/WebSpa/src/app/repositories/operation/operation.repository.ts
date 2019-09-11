import { Injectable } from "@angular/core";
import { RestService } from "../../service/rest.service";
import { Observable } from "rxjs";
import { Missile, MissileStatus } from "../models/missile.model";
import { environment } from "src/environments/environment";

@Injectable()
export class OperationRepository {
  private baseUri = environment.services.operation.uri;

  constructor(private restService: RestService) {}

  // Get list of ready-to-use missiles
  public getMissiles(status: MissileStatus): Observable<Missile[]> {
    var url =
      status ? `${this.baseUri}/missiles?status=${status}` : `${this.baseUri}/missiles`;

    return this.restService.sendRequest<Missile[]>("GET", url);
  }
}
