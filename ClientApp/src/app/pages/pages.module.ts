import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { ChartComponent } from './chart/chart.component';
import { RouterModule } from '@angular/router';
import { ChartsModule } from 'ng2-charts';
import { UsersComponent } from './users/users.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ChartsModule,
    FormsModule,
    NgSelectModule
  ],
  declarations: [
    CounterComponent,
    FetchDataComponent,
    HomeComponent,
    ChartComponent,
    UsersComponent
  ],
  exports: [
    CounterComponent,
    FetchDataComponent,
    HomeComponent,
    ChartComponent,
    UsersComponent
  ]
})
export class PagesModule { }
