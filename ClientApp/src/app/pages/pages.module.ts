import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { ChartComponent } from './chart/chart.component';
import { RouterModule } from '@angular/router';
import { ChartsModule } from 'ng2-charts';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ChartsModule
  ],
  declarations: [
    CounterComponent,
    FetchDataComponent,
    HomeComponent,
    ChartComponent
  ],
  exports: [
    CounterComponent,
    FetchDataComponent,
    HomeComponent,
    ChartComponent
  ]
})
export class PagesModule { }
