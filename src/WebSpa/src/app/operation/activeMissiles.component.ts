import { Component } from "@angular/core";
import { OperationRepository } from "../repositories/operation/operation.repository";
import { Observable } from "rxjs";
import { Missile } from "../repositories/models/missile.model";

@Component({
  selector: "active-missiles",
  templateUrl: "activeMissiles.component.html"
})
export class ActiveMissiles {
  constructor(private operationRepo: OperationRepository) {}

  public get missiles(): Observable<Missile[]> {
    return this.operationRepo.getMissiles();
  }
}
