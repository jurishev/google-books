import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { Bookshelf } from '../models/bookshelf.model';
import { EntityService } from '../services/entity.service';
import { State, StateService } from '../services/state.service';

@Component({
    selector: 'app-bookshelf-list-view',
    templateUrl: './bookshelf-list-view.component.html',
    styleUrls: ['./bookshelf-list-view.component.scss']
})
export class BookshelfListViewComponent {

    private readonly bookshelfListSubscription: Subscription;

    bookshelfList: Bookshelf[] | null | undefined;

    constructor(
        private readonly entityService: EntityService,
        private readonly stateService: StateService,
    ) {
        this.bookshelfListSubscription = this.entityService.bookshelfList$.subscribe(bshl => this.bookshelfList = bshl);
    }

    ngOnDestroy(): void {
        this.bookshelfListSubscription.unsubscribe();
    }

    selectBookshelf(shelf: number, userId: string): void {
        this.entityService.setBookshelf(shelf, userId);
        this.stateService.set(State.Bookshelf);
    }
}
