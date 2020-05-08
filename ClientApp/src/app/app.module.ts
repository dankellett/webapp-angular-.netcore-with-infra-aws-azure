import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import { CoreModule } from 'src/app/core/core.module'
import { SharedModule } from 'src/app/shared/shared.module'
import { PagesModule } from 'src/app/pages/pages.module'

import { AuthorizeGuard } from 'src/app/core/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/app/core/api-authorization/authorize.interceptor';

import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { FetchDataComponent } from './pages/fetch-data/fetch-data.component';
import { ChartComponent } from './pages/chart/chart.component';
import { UsersComponent } from './pages/users/users.component';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    NgbModule,
    FormsModule,
    NgSelectModule,
    CoreModule,
    SharedModule,
    PagesModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent, canActivate: [AuthorizeGuard]},
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'chart', component: ChartComponent, canActivate: [AuthorizeGuard]  },
      { path: 'users', component: UsersComponent, canActivate: [AuthorizeGuard]  },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
