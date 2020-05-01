import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { CoreModule } from 'src/app/core/core.module'

@NgModule({
  declarations: [
    NavMenuComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    CoreModule
  ],
  exports: [
    NavMenuComponent
  ]
})
export class SharedModule { }
