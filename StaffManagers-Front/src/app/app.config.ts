import { routes } from './app-routing.module';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';


export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes)]
};
