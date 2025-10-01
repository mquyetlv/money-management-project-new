import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'admin',
        loadChildren: () => import('../modules/admin/admin.routes').then(m => m.adminRoutes),
    },
    {
        path: '',
        loadChildren: () => import('../modules/transactions/transactions.routes').then(m => m.transactionsRoutes),
    }
];
