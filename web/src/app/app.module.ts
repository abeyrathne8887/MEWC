import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginOldComponent } from './Login/LoginOld/LoginOld.component';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { LoginComponent } from './Login/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginOldComponent,
    LoginComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
