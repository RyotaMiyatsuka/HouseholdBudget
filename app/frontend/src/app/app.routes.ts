import { Routes } from '@angular/router';
import { Calender } from './components/calender/calender';
import { Home } from './components/home/home';
import { Input } from './components/input/input';
import { Report } from './components/report/report';
import { Settings } from './components/settings/settings';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { ApiTestComponent } from './components/api-test/api-test.component';

export const routes: Routes = [
  {
    path: '',
    component: Home,
    title: 'Budget',
  },
  {
    path: 'calender',
    component: Calender,
    title: 'Calender',
  },
  {
    path: 'input',
    component: Input,
    title: 'Input',
  },
  {
    path: 'report',
    component: Report,
    title: 'Report',
  },
  {
    path: 'settings',
    component: Settings,
    title: 'Settings',
  },
  {
    path: 'login',
    component: Login,
    title: 'Login',
  },
  {
    path: 'register',
    component: Register,
    title: 'Register',
  },
  {
    path: 'api-test',
    component: ApiTestComponent,
    title: 'API Test',
  }
];
