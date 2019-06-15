import { Component } from "@angular/core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  private _showActiveMissiles: boolean;

  constructor() {
    this._showActiveMissiles = false;
  }

  public loadActiveMissiles() {
    this._showActiveMissiles = true;
  }

  public unloadActiveMissiles() {
    this._showActiveMissiles = false;
  }

  public get showActiveMissiles(): boolean {
    return this._showActiveMissiles;
  }
}
