import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';


import { CoreModule } from 'src/app/core/core.module'
import { SharedModule } from 'src/app/shared/shared.module'

import { AuthorizeGuard } from 'src/app/core/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/app/core/api-authorization/authorize.interceptor';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './shared/components/nav-menu/nav-menu.component';
import { HomeComponent } from './pages/home/home.component';
import { CounterComponent } from './pages/counter/counter.component';
import { FetchDataComponent } from './pages/fetch-data/fetch-data.component';
import { ChartComponent } from './pages/chart/chart.component';


@NgModule({
  declarations: [
    AppComponent,
    //NavMenuComponent,
    //HomeComponent,
    //CounterComponent,
    //FetchDataComponent,
    //ChartComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CoreModule,
    SharedModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent, canActivate: [AuthorizeGuard]},
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'chart', component: ChartComponent, canActivate: [AuthorizeGuard]  },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
