import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiAuthorizationModule } from 'src/app/core/api-authorization/api-authorization.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ApiAuthorizationModule
  ],
  exports:[
    ApiAuthorizationModule
  ]
})
export class CoreModule { }
