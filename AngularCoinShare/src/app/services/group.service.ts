import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Guid } from '../models/guid.model';
import { GroupDto, PersonDto } from '../dtos';

@Injectable({
    providedIn: 'root'
})
export class GroupService {
    private readonly apiBaseUrl: string = "https://localhost:5001/api/group";

    constructor(private http: HttpClient) { }

    public getById(id: Guid): Observable<GroupDto> {
        if (id != null) {
            return this.http.get<GroupDto>(`${this.apiBaseUrl}/${id}`);
        }
        else {
            const newGroup: GroupDto = {
                id: null,
                name: '',
                persons: [
                    { id: 0, name: '', groupId: null },
                    { id: 0, name: '', groupId: null },
                    { id: 0, name: '', groupId: null },
                ]
            };

            return of(newGroup);
        }
    }

    public save(group: GroupDto): Observable<Guid> {
        if (group.id == null) {
            return this.http.post<Guid>(this.apiBaseUrl, group);
        }
        else {
            return this.http.put(this.apiBaseUrl, group).pipe(
                map(_ => group.id)
            );
        }
    }
}
