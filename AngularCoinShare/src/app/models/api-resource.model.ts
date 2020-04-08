import { Observable, of } from 'rxjs';
import { map, catchError, startWith } from 'rxjs/operators';

export interface Empty { kind: 'Empty' }
export interface Loading { kind: 'Loading' }
export interface Data<T> { kind: 'Data', item: T }
export interface Error { kind: 'Error', message: string }

export type ApiResource<T> = Empty | Loading | Data<T> | Error;

export const data = <T>(value: T): Data<T> => ({
    kind: 'Data', item: value
});

export const empty = (): Empty => ({ kind: 'Empty' });
export const loading = (): Loading => ({ kind: 'Loading' });
export const error = (message: string): Error => ({ kind: 'Error', message });

export function getResource<TData>(from: Observable<TData>): Observable<ApiResource<TData>> {
    return from.pipe(
        map((dto: TData) => data(dto)),
        catchError(e => of(error('error while loading'))),
        startWith(loading())
    );
}
