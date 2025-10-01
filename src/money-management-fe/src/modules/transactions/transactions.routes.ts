import { Routes } from "@angular/router";

export const transactionsRoutes: Routes = [
    {
        path: 'overview',
        loadComponent: () => import('./pages').then(m => m.Overview),
    }
];