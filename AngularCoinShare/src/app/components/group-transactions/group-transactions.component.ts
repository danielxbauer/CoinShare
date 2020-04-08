import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { GroupDto, PaginationDto, PersonDto, TransactionDto } from 'src/app/dtos';
import { ApiResource, empty, getResource, TransactionType } from 'src/app/models';
import { AppState } from 'src/app/services/app-state.service';
import { TransactionService } from 'src/app/services/transaction.service';

@Component({
    selector: 'app-group-transactions',
    templateUrl: './group-transactions.component.html',
    styleUrls: ['./group-transactions.component.css']
})
export class GroupTransactionsComponent implements OnInit {
    public groupResource: ApiResource<GroupDto> = empty();
    public transactionResource: ApiResource<PaginationDto<TransactionDto[]>> = empty();
    public loadMoreResource: ApiResource<PaginationDto<TransactionDto[]>> = empty();

    constructor(
        private state: AppState,
        private transactionService: TransactionService,
        private datePipe: DatePipe) { }

    ngOnInit() {
        this.state.getLatestGroup().subscribe(g => {
            this.groupResource = g;

            if (this.groupResource.kind === 'Data') {
                const resource$ = this.transactionService.getByGroupId(this.groupResource.item.id, { offset: 0, take: 20 });
                getResource(resource$).subscribe(t => this.transactionResource = t);
            }
        });
    }

    public groupByDate(transactions: TransactionDto[]) {
        return Object.entries(
            transactions.reduce((group, transaction) => {
                const key = this.datePipe.transform(transaction.paidOn, 'dd.MM.yyyy');

                group[key] = group[key] != null
                    ? [...group[key], transaction]
                    : [transaction];

                return group;
            }, {})
        );
    }

    public getPerson(persons: PersonDto[], personId: number) {
        return persons.find(p => p.id === personId);
    }

    public paidForNames(persons: PersonDto[], transaction: TransactionDto) {
        if (transaction.paidFor.length == persons.length)
        {
            return 'everyone';
        }

        const take = 3;
        var personNames = transaction.paidFor
            .slice(0, take).map(id => persons.find(p => p.id === id).name);

        var names = personNames.join(', ');

        return transaction.paidFor.length > take
            ? `${names} and ${transaction.paidFor.length - take} more`
            : names;
    }

    public loadMore() {
        if (this.transactionResource.kind === 'Data' && this.groupResource.kind === 'Data') {
            const transaction = this.transactionResource.item;
            const group = this.groupResource.item;

            const resource$ = this.transactionService.getByGroupId(group.id, { offset: transaction.offset, take: 20 });
            getResource(resource$).subscribe(data => {
                    this.loadMoreResource = data;

                    if (data.kind === 'Data') {
                        transaction.offset = data.item.offset;
                        transaction.total = data.item.total;
                        transaction.items = [
                            ...transaction.items,
                            ...data.item.items
                        ];
                    }
                });
        }
    }

    Spending = TransactionType.Spending;
    Payment = TransactionType.Payment;
}
