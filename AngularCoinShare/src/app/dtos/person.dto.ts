import { Guid } from '../models/guid.model';

export interface PersonDto {
    id: number,
    name: string,
    groupId: Guid | null
}
