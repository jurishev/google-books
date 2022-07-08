import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Bookshelf } from '../models/bookshelf.model';
import { Volume } from '../models/volume.model';
import { EntityService } from '../services/entity.service';
import { ViewStateService } from '../services/view-state.service';

@Component({
    selector: 'app-views',
    templateUrl: './views.component.html',
    styleUrls: ['./views.component.scss']
})
export class ViewsComponent implements OnDestroy {

    private readonly volumeListSubscription: Subscription;
    private readonly volumeSubscription: Subscription;
    private readonly bookshelfListSubscription: Subscription;
    private readonly bookshelfSubscription: Subscription;

    volumeList: Volume[] | undefined;
    volume: Volume | undefined;
    bookshelfList: Bookshelf[] | undefined;
    bookshelf: Bookshelf | undefined;

    constructor(
        readonly state: ViewStateService,
        private readonly entityService: EntityService,
    ) {
        this.volumeListSubscription = this.entityService.volumeList$.subscribe(vl => this.volumeList = vl);
        this.volumeSubscription = this.entityService.volume$.subscribe(v => this.volume = v);
        this.bookshelfListSubscription = this.entityService.bookshelfList$.subscribe(bshl => this.bookshelfList = bshl);
        this.bookshelfSubscription = this.entityService.bookshelf$.subscribe(bsh => this.bookshelf = bsh);
    }

    ngOnDestroy(): void {
        this.volumeListSubscription.unsubscribe();
        this.volumeSubscription.unsubscribe();
        this.bookshelfListSubscription.unsubscribe();
        this.bookshelfSubscription.unsubscribe();
    }
}
