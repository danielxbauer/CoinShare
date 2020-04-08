import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GroupDto } from 'src/app/dtos';
import { ApiResource, empty, Guid, TransactionType } from 'src/app/models';
import { AppState } from 'src/app/services/app-state.service';

@Component({
    selector: 'app-group-detail',
    templateUrl: './group-detail.component.html',
    styleUrls: ['./group-detail.component.css']
})
export class GroupDetailComponent implements OnInit {

    public group: ApiResource<GroupDto> = empty();

    constructor(
        private state: AppState,
        private route: ActivatedRoute) { }

    ngOnInit() {
        this.route.params.subscribe(async params => {
            const id: Guid = params['id'];
            this.state.getGroupById(id).subscribe(g => {
                this.group = g;
                console.log(this.group.kind);
            });
        });
    }

    Spending = TransactionType.Spending;
    Payment = TransactionType.Payment;
}
