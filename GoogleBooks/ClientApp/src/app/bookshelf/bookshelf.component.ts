import { Component, Input } from '@angular/core';
import { Bookshelf } from '../models/bookshelf.model';

@Component({
    selector: 'app-bookshelf',
    templateUrl: './bookshelf.component.html',
    styleUrls: ['./bookshelf.component.scss']
})
export class BookshelfComponent {

    @Input() bookshelf: Bookshelf | undefined;
}
