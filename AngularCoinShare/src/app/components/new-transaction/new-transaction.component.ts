import { Component, OnInit } from '@angular/core';
import { GroupService } from 'src/app/services/group.service';
import { TransactionService } from 'src/app/services/transaction.service';
import { Router, ActivatedRoute } from '@angular/router';
import { TransactionType, ApiResource, Guid, empty, getResource } from 'src/app/models';
import { TransactionDto, GroupDto, PersonDto } from 'src/app/dtos';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'app-new-transaction',
    templateUrl: './new-transaction.component.html',
    styleUrls: ['./new-transaction.component.css']
})
export class NewTransactionComponent implements OnInit {
    public id: Guid = null;
    public transactionType = TransactionType.Spending;

    public groupResource: ApiResource<GroupDto>;
    public transaction: TransactionDto;
    public saveTransactionResource: ApiResource<number> = empty();

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private groupService: GroupService,
        private transactionService: TransactionService
    ) { }

    public get title(): string {
        switch (this.transactionType) {
            case TransactionType.Payment: return 'New Payment';
            case TransactionType.Spending: return 'New Spending';
        }
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.id = params['id'];
            this.transactionType = +params['transactionType'];

            const resource$ = this.groupService.getById(this.id);
            getResource(resource$).subscribe(g => {
                this.groupResource = g;

                if (this.groupResource.kind === 'Data') {
                    this.transaction = this.initTransaction(this.groupResource.item);
                }
            })
        })
    }

    private initTransaction(group: GroupDto): TransactionDto {
        let paidFor: number[] = [];
        switch (this.transactionType) {
            case TransactionType.Payment: paidFor = [group.persons[1].id]; break;
            default: paidFor = group.persons.map(p => p.id); break;
        }

        return {
            id: 0,
            text: '',
            amount: 0,
            paidOn: new Date(),
            paidBy: group.persons[0].id,
            paidFor: paidFor,
            transactionType: this.transactionType,
            groupId: group.id
        };
    }

    public changed(personId: number) {
        switch (this.transactionType) {
            case TransactionType.Payment:
                this.transaction.paidFor = [personId];
                break;

            case TransactionType.Spending:
                const isChecked = !this.transaction.paidFor.includes(personId);
                if (isChecked) {
                    this.transaction.paidFor.push(personId);
                } else {
                    const index = this.transaction.paidFor.indexOf(personId);
                    this.transaction.paidFor.splice(index, 1);
                }
                break;
        }
    }

    public save(form: NgForm, transaction: TransactionDto) {
        form.form.markAllAsTouched();

        if (form.valid) {
            const resource$ = this.transactionService.create(transaction);
            getResource(resource$).subscribe(data => {
                this.saveTransactionResource = data;

                if (data.kind === 'Data') {
                    this.router.navigate([this.id, 'transactions']);
                }
            });
        }
    }

    payment = TransactionType.Payment;
    spending = TransactionType.Spending;
}
