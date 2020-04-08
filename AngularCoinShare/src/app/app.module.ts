import { DatePipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EditGroupComponent } from './components/edit-group/edit-group.component';
import { GroupDetailComponent } from './components/group-detail/group-detail.component';
import { GroupPersonsComponent } from './components/group-persons/group-persons.component';
import { GroupTransactionsComponent } from './components/group-transactions/group-transactions.component';
import { HomeComponent } from './components/home/home.component';
import { NewTransactionComponent } from './components/new-transaction/new-transaction.component';
import { CardComponent } from './components/shared/card/card.component';
import { InputValidationDirective } from './components/shared/input-validation/input-validation.directive';
import { LoadingComponent } from './components/shared/loading/loading.component';
import { ResourceActionDirective, ResourceErrorComponent } from './components/shared/resource-action/resource-action.directive';
import { ResourceLoaderComponent } from './components/shared/resource-loader/resource-loader.component';
import { ValidationMessageComponent } from './components/shared/validation-message/validation-message.component';
import { AppCurrencyPipe } from './pipes/appcurrency.pipe';

@NgModule({
    declarations: [
        AppComponent,
        GroupDetailComponent,
        ResourceLoaderComponent,
        ResourceActionDirective,
        LoadingComponent,
        GroupPersonsComponent,
        GroupTransactionsComponent,
        AppCurrencyPipe,
        CardComponent,
        EditGroupComponent,
        ValidationMessageComponent,
        ResourceErrorComponent,
        InputValidationDirective,
        HomeComponent,
        NewTransactionComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule
    ],
    providers: [
        DatePipe
    ],
    entryComponents: [
        LoadingComponent,
        ResourceErrorComponent
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }
