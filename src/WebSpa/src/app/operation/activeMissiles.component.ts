import { Component } from "@angular/core";
import { OperationRepository } from "../repositories/operation/operation.repository";
import { Observable } from "rxjs";
import { Missile } from "../repositories/models/missile.model";

@Component({
  selector: "active-missiles",
  templateUrl: "activeMissiles.component.html"
})
export class ActiveMissilesComponent {
  private activeMissilesArray: Missile[];

  constructor(private operationRepo: OperationRepository) {
    this.getActiveMissiles();
  }

  // This leads to real time calls to API
  private getActiveMissiles() {
    return this.operationRepo.getMissiles().subscribe(response => {
      this.activeMissilesArray = response;
    });
  }

  public get activeMissiles(): Missile[] {
    return this.activeMissilesArray;
  }
}
