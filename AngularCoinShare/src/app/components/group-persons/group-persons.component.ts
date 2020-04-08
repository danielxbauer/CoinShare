import { Component, OnInit } from '@angular/core';
import { TransactionService } from 'src/app/services/transaction.service';
import { ApiResource, empty, loading, data, error, getResource } from 'src/app/models';
import { PersonOverviewDto, GroupDto, PersonDto } from 'src/app/dtos';
import { AppState } from 'src/app/services/app-state.service';
import { map, catchError, startWith } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
    selector: 'app-group-persons',
    templateUrl: './group-persons.component.html',
    styleUrls: ['./group-persons.component.css']
})
export class GroupPersonsComponent implements OnInit {
    public groupResource: ApiResource<GroupDto> = empty();
    public personOverviewsResource: ApiResource<PersonOverviewDto[]> = empty();

    constructor(
        private state: AppState,
        private transactionService: TransactionService) { }

    ngOnInit() {
        this.state.getLatestGroup().subscribe(g => {
            this.groupResource = g;

            if (this.groupResource.kind === 'Data') {
                const resource$ = this.transactionService.getPersonOverviews(this.groupResource.item.id);
                getResource(resource$).subscribe(t => this.personOverviewsResource = t);
            }
        });
    }

    getPerson(group: GroupDto, id: number): PersonDto {
        return group.persons.find(p => p.id === id);
    }
}
