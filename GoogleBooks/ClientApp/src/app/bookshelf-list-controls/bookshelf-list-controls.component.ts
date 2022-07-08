import { Component } from '@angular/core';
import { EntityService } from '../services/entity.service';

@Component({
    selector: 'app-bookshelf-list-controls',
    templateUrl: './bookshelf-list-controls.component.html',
    styleUrls: ['./bookshelf-list-controls.component.scss']
})
export class BookshelfListControlsComponent {

    userId = "";

    constructor(
        private readonly entityService: EntityService
    ) { }

    search(): void {
        if (this.userId) {
            this.entityService.getBookshelfList(this.userId);
        }
    }
}
