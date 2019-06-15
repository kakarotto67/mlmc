import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ActiveMissilesComponent } from './operation/activeMissiles.component';
import { OperationRepository } from './repositories/operation/operation.repository';
import { RestService } from './service/rest.service';

@NgModule({
  declarations: [
    AppComponent,
    ActiveMissilesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [ OperationRepository, RestService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
