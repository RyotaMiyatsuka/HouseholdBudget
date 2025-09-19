import { Routes } from '@angular/router';
import { Calender } from './components/calender/calender';
import { Home } from './components/home/home';
import { Input } from './components/input/input';
import { Report } from './components/report/report';
import { Settings } from './components/settings/settings';
import { Signin } from './components/signin/signin';
import { Signup } from './components/signup/signup';

export const routes: Routes = [
  {
    path: '',
    component: Home,
    title: 'Home',
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
    path: 'signin',
    component: Signin,
    title: 'Signin',
  },
  {
    path: 'signup',
    component: Signup,
    title: 'Signup',
  }
];
