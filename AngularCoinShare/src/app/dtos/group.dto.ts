import { Guid } from '../models/guid.model';
import { PersonDto } from './person.dto';

export interface GroupDto {
    id: Guid | null,
    name: string,
    persons: PersonDto[]
}
