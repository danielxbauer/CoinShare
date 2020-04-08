import { Guid } from '../models/guid.model';
import { TransactionType } from '../models/transaction-type.model';

export interface TransactionDto {
    id: number,
    text: string,
    amount: number,
    paidOn: Date,
    paidBy: number,
    paidFor: number[],
    transactionType: TransactionType,
    groupId: Guid | null
}
