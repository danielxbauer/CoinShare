export interface PaginationDto<T> {
    items: T,
    offset: number,
    total: number
}
