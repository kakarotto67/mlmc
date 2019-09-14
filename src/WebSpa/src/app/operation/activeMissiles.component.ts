import { Component } from "@angular/core";
import { OperationRepository } from "../repositories/operation/operation.repository";
import { Missile, MissileStatus } from "../repositories/models/missile.model";

@Component({
  selector: "active-missiles",
  templateUrl: "activeMissiles.component.html"
})
export class ActiveMissilesComponent {
  private activeMissilesArray: Missile[];
  private showDecommissionMissilePopup: boolean;
  public missileToDecommissionServiceIdentityNumber : string;

  constructor(private operationRepo: OperationRepository) {
    this.getActiveMissiles();
    this.showDecommissionMissilePopup = false;
  }

  // This leads to real time calls to API
  private getActiveMissiles() {
    return this.operationRepo.getMissiles(MissileStatus.InService).subscribe(response => {
      this.activeMissilesArray = response.map(
        x =>
          new Missile(
            x.missileId,
            x.deploymentPlatformId,
            x.serviceIdentityNumber,
            x.name,
            x.type,
            x.status,
            x.inServiceDateStart,
            x.inServiceDateEnd
          )
      );
    });
  }

  public get activeMissiles(): Missile[] {
    return this.activeMissilesArray;
  }

  public decommissionMissile(serviceIdentityNumber: string) {
    this.missileToDecommissionServiceIdentityNumber = serviceIdentityNumber;
    this.showDecommissionMissilePopup = true;
  }

  public get showDecommissionMissile(): boolean {
    return this.showDecommissionMissilePopup;
  }
}
