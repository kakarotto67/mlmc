import { Component } from "@angular/core";
import { OperationRepository } from "../repositories/operation/operation.repository";
import { Missile } from "../repositories/models/missile.model";
import { DeploymentPlatform } from "../repositories/models/deploymentPlatform.model";

@Component({
  selector: "commission-missile",
  templateUrl: "commissionMissile.component.html",
  styleUrls: ["./commissionMissile.component.scss"]
})
export class CommissionMissileComponent {
  private deploymentPlatformsArray: DeploymentPlatform[];

  private newMissile: Missile;

  constructor(private operationRepo: OperationRepository) {
    // TODO: Fix the intialization below
    // Initialize new missile with default values
    this.newMissile = new Missile(0, 1, "", "", 0, 0, "");
    this.getDeploymentPlatforms();
  }

  // This leads to real time calls to API
  private getDeploymentPlatforms() {
    return this.operationRepo.getDeploymentPlatforms().subscribe(response => {
      this.deploymentPlatformsArray = response.map(
        x =>
          new DeploymentPlatform(
            x.deploymentPlatformId,
            x.serviceIdentityNumber,
            x.name,
            x.location
          )
      );
    });
  }

  public get deploymentPlatforms(): DeploymentPlatform[] {
    return this.deploymentPlatformsArray;
  }

  public commissionMissile() {
     this.operationRepo.createMissile(this.newMissile);
  }
}
