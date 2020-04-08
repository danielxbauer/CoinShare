import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupDto, PersonDto } from 'src/app/dtos';
import { ApiResource, empty, getResource, Guid } from 'src/app/models';
import { GroupService } from 'src/app/services/group.service';

@Component({
    selector: 'app-edit-group',
    templateUrl: './edit-group.component.html',
    styleUrls: ['./edit-group.component.css']
})
export class EditGroupComponent implements OnInit {
    public id: Guid = null;
    public groupResource: ApiResource<GroupDto> = empty();
    public saveGroupResource: ApiResource<Guid> = empty();

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private groupService: GroupService
    ) { }

    ngOnInit() {
        this.route.params.subscribe(async params => {
            this.id = params['id'];

            const resource$ = this.groupService.getById(this.id);
            getResource(resource$).subscribe(group => this.groupResource = group);
        });
    }

    public get title(): string {
        return this.id == null ? 'New Group' : 'Edit Group';
    }

    public get saveText(): string {
        return this.id == null ? 'Create group' : 'Save';
    }

    public disableAdd(group: GroupDto): boolean {
        return group.persons.length <= 3 || group.persons.length >= 12;
    }

    public addPerson(group: GroupDto) {
        const newPerson: PersonDto = { id: 0, name: '', groupId: null };
        return group.persons.push(newPerson);
    }

    public removePerson(group: GroupDto, person: PersonDto) {
        const index = group.persons.indexOf(person);
        return group.persons.splice(index, 1);
    }

    public save(form: NgForm, group: GroupDto) {
        form.form.markAllAsTouched();

        if (form.valid) {
            const resource$ = this.groupService.save(group);
            getResource(resource$).subscribe(g => {
                this.saveGroupResource = g;

                if (g.kind === 'Data') {
                    const id: Guid = g.item;
                    this.router.navigate([id, 'persons']);
                }
            })
        }
    }
}
