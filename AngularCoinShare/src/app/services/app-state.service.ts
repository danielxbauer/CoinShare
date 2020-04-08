import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GroupDto } from '../dtos';
import { ApiResource, getResource, Guid } from '../models';
import { GroupService } from './group.service';

@Injectable({
    providedIn: 'root'
})
export class AppState {
    private group$: Observable<ApiResource<GroupDto>>;

    constructor(private groupService: GroupService) { }

    public getLatestGroup(): Observable<ApiResource<GroupDto>> {
        return this.group$;
    }

    public getGroupById(id: Guid): Observable<ApiResource<GroupDto>> {
        const resource$ = this.groupService.getById(id);
        this.group$ = getResource(resource$);
        return this.group$;
    }
}
