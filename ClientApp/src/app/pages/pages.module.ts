import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  declarations: [
    CounterComponent,
    FetchDataComponent,
    HomeComponent
  ],
  exports: [
    CounterComponent,
    FetchDataComponent,
    HomeComponent
  ]
})
export class PagesModule { }
