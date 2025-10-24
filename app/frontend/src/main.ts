import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';

async function enableMocking() {
  if (process.env['NODE_ENV'] !== 'production') {
    const { worker } = await import('./mocks/browser');
    return worker.start();
  }
  return
}

enableMocking().then(() => {
  bootstrapApplication(App, appConfig)
    .catch((err) => console.error(err));
});
