import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'appCurrency'
})
export class AppCurrencyPipe implements PipeTransform {
    transform(value: number, ...args: any[]): string {
        return value != null
            ? `${value.toFixed(2)}€`
            : null;
    }
}
