import { Injectable } from "@angular/core";
import { RestService } from "../../service/rest.service";
import { Observable } from "rxjs";
import { Missile } from "../models/missile.model";
import { environment } from "src/environments/environment";

@Injectable()
export class OperationRepository {
  private baseUri = environment.services.operation.uri;

  constructor(private restService: RestService) {}

  // Get list of ready-to-use missiles
  public getMissiles(): Observable<Missile[]> {
    return this.restService.sendRequest<Missile[]>("GET", `${this.baseUri}/missiles`);
  }
}
