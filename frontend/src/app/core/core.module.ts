import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import {SharedModule} from "../shared/shared.module";
import {JwtInterceptor} from "./intercptors/jwt.interceptor";
import {BaseComponent} from "./base/base.component";


@NgModule({
  imports: [HttpClientModule, SharedModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  declarations: [BaseComponent],
})
export class CoreModule {}
