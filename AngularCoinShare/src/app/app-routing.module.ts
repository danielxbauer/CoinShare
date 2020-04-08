import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditGroupComponent } from './components/edit-group/edit-group.component';
import { GroupDetailComponent } from './components/group-detail/group-detail.component';
import { GroupPersonsComponent } from './components/group-persons/group-persons.component';
import { GroupTransactionsComponent } from './components/group-transactions/group-transactions.component';
import { HomeComponent } from './components/home/home.component';
import { NewTransactionComponent } from './components/new-transaction/new-transaction.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: ':id/settings', component: EditGroupComponent },
    { path: ':id/transaction/:transactionType', component: NewTransactionComponent },
    { path: ':id', component: GroupDetailComponent, children: [
            { path: 'persons', component: GroupPersonsComponent },
            { path: 'transactions', component: GroupTransactionsComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
