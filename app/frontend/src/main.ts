import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { environment } from './environments/environment';

async function enableMocking() {
  // Only enable MSW if useMocks is true in environment config
  if (environment.useMocks && process.env['NODE_ENV'] !== 'production') {
    const { worker } = await import('./mocks/browser');
    return worker.start();
  }
  return Promise.resolve();
}

enableMocking().then(() => {
  bootstrapApplication(App, appConfig)
    .catch((err) => console.error(err));
});
