import { Component } from "@angular/core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  private activeMissilesLoaded: boolean;

  constructor() {
    this.activeMissilesLoaded = false;
  }

  public loadActiveMissiles() {
    this.activeMissilesLoaded = true;
  }

  public unloadActiveMissiles() {
    this.activeMissilesLoaded = false;
  }

  public get showActiveMissiles(): boolean {
    return this.activeMissilesLoaded;
  }
}
