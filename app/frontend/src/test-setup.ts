
import '@angular/compiler';
import '@analogjs/vitest-angular/setup-zone';

import {
  BrowserTestingModule,
  platformBrowserTesting,
} from '@angular/platform-browser/testing';
import { getTestBed } from '@angular/core/testing';
import { destroyPlatform } from '@angular/core';

// Destroy any existing platform to avoid NG0400 error with singleFork
destroyPlatform();

getTestBed().initTestEnvironment(
  BrowserTestingModule,
  platformBrowserTesting()
);

