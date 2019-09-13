import { Component } from "@angular/core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  private activeMissilesLoaded: boolean;
  private commissionMissile: boolean;

  constructor() {
    this.activeMissilesLoaded = false;
    this.commissionMissile = false;
  }

  public loadActiveMissiles() {
    this.activeMissilesLoaded = true;
  }

  public get showActiveMissiles(): boolean {
    return this.activeMissilesLoaded;
  }

  public commissionNewMissile(){
    this.commissionMissile = true;
  }

  public get showCommissionMissile(): boolean{
    return this.commissionMissile;
  }
}
