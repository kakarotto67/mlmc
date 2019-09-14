import { Component, Input } from "@angular/core";
import { OperationRepository } from "../repositories/operation/operation.repository";

@Component({
  selector: "decommission-missile",
  templateUrl: "decommissionMissile.component.html"
})
export class DecommissionMissileComponent {
  @Input("missileToDecommissionServiceIdentityNumber") public missileServiceIdentityNumber: string;

  constructor(private operationRepo: OperationRepository) {}

  public decommissionMissile() {
    this.operationRepo.decommissionMissile(this.missileServiceIdentityNumber);
  }

  // TODO: Just for demonstration purposes
  // Displays new value of missileServiceIdentityNumber whenever it is changed on parent component
  //   ngOnChanges(changes: {[propKey: string]: SimpleChange}) {
  //     for (let propName in changes) {
  //       let changedProp = changes[propName];
  //       let to = JSON.stringify(changedProp.currentValue);
  //       if (changedProp.isFirstChange()) {
  //         console.log(`Initial value of ${propName} set to ${to}`);
  //       } else {
  //         let from = JSON.stringify(changedProp.previousValue);
  //         console.log(`${propName} changed from ${from} to ${to}`);
  //       }
  //     }
  //   }
}
