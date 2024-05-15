import { ApplicationConfig, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideStore } from '@ngrx/store';
import { provideEffects } from '@ngrx/effects';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { ListEffects } from './components/ngrx/effects/list.effect';
import { ListReducer } from './components/ngrx/reducers/list.reducer';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideClientHydration(), provideHttpClient(), provideStore({list : ListReducer }), provideEffects(ListEffects), provideStoreDevtools({ maxAge: 25, logOnly: !isDevMode() })]
};
