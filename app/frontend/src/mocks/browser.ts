import { setupWorker } from 'msw/browser';
import { handlers } from './handlers';

// Setup MSW worker for browser (development mode)
export const worker = setupWorker(...handlers);
