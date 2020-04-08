import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Guid } from '../models';
import { PaginationDto, TransactionDto, PersonOverviewDto } from '../dtos';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class TransactionService {
    private readonly apiBaseUrl: string = "https://localhost:5001/api/transaction";

    constructor(private http: HttpClient) { }

    public getByGroupId(groupId: Guid, paginationConfig: { offset: number, take: number }): Observable<PaginationDto<TransactionDto[]>> {
        const { offset, take } = paginationConfig;
        return this.http.get<PaginationDto<TransactionDto[]>>(`${this.apiBaseUrl}/group/${groupId}?offset=${offset}&take=${take}`).pipe(
            map(t => {
                t.items.forEach(i => i.paidOn = new Date(i.paidOn));
                return t;
            })
        );
    }

    public getPersonOverviews(groupId: Guid): Observable<PersonOverviewDto[]> {
        return this.http.get<PersonOverviewDto[]>(`${this.apiBaseUrl}/group/${groupId}/persons`);
    }

    public create(transaction: TransactionDto): Observable<number> {
        return this.http.post<number>(this.apiBaseUrl, transaction);
    }
}
