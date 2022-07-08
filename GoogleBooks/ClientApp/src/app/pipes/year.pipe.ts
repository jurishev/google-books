import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'year'
})
export class YearPipe implements PipeTransform {

    transform(value: string): string {
        return value.substring(0, 4);
    }
}
