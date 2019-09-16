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
      // Initialize with default values
      this.missileServiceIdentityNumber = null;
      // TODO: For Further Polishing milestone - Add possibility to choose target on a map
      this.targetLocation = new GpsLocation(37.617561, 55.752035);
  }

  public launchMissile() {
    this.operationRepo.launchMissile(this.missileServiceIdentityNumber, this.targetLocation);
  }
}
