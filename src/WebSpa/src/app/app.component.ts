import { Component } from "@angular/core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  private activeMissilesLoaded: boolean;
  private commissionMissile: boolean;
  private launchMissile: boolean;

  constructor() {
    this.activeMissilesLoaded = false;
    this.commissionMissile = false;
    this.launchMissile = false;
  }

  public loadActiveMissiles() {
    this.activeMissilesLoaded = true;

    // Hide others
    this.commissionMissile = false;
    this.launchMissile = false;
  }

  public get showActiveMissiles(): boolean {
    return this.activeMissilesLoaded;
  }

  public commissionNewMissile() {
    this.commissionMissile = true;

    // Hide others
    this.activeMissilesLoaded = false;
    this.launchMissile = false;
  }

  public get showCommissionMissile(): boolean {
    return this.commissionMissile;
  }

  public launchNewMissile() {
    this.launchMissile = true;

    // Hide others
    this.activeMissilesLoaded = false;
    this.commissionMissile = false;
  }

  public get showLaunchMissile(): boolean {
    return this.launchMissile;
  }
}
