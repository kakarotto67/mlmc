import { Component } from "@angular/core";
import { OperationRepository } from "../repositories/operation/operation.repository";
import { GpsLocation } from "../repositories/models/gpsLocation.model";

@Component({
  selector: "launch-missile",
  templateUrl: "launchMissile.component.html"
})
export class LaunchMissileComponent {
  private missileServiceIdentityNumber: string;
  private targetLocation: GpsLocation;

  constructor(private operationRepo: OperationRepository) {
      this.missileServiceIdentityNumber = null;
      this.targetLocation = new GpsLocation(0, 0);
  }

  public launchMissile() {
    this.operationRepo.launchMissile(this.missileServiceIdentityNumber, this.targetLocation);
  }
}
