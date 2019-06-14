import { Component } from "@angular/core";
import { OperationRepository } from './repositories/operation/operation.repository';

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  private _showActiveMissiles: boolean;

  constructor(private operationRepo: OperationRepository) {
    this._showActiveMissiles = true;
  }

  public toggleShowActiveMissiles() {
    this._showActiveMissiles = !this._showActiveMissiles;
    // console.log(`toggleShowActiveMissiles: ${this._showActiveMissiles}`);
  }

  public get showActiveMissiles(): boolean {
    return this._showActiveMissiles;
  }
}
