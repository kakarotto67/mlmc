import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AppComponent } from "./app.component";
import { ActiveMissilesComponent } from "./operation/activeMissiles.component";

const routes: Routes = [
  { path: "", redirectTo: "/index", pathMatch: "full" },
  {
    path: "index",
    component: AppComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
