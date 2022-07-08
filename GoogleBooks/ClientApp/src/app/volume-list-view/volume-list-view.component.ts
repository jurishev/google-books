import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Volume } from '../models/volume.model';
import { EntityService } from '../services/entity.service';

@Component({
    selector: 'app-volume-list-view',
    templateUrl: './volume-list-view.component.html',
    styleUrls: ['./volume-list-view.component.scss']
})
export class VolumeListViewComponent implements OnDestroy {

    private readonly volumeListSubscription: Subscription;

    volumeList: Volume[] | null | undefined;

    constructor(
        private readonly entityService: EntityService
    ) {
        this.volumeListSubscription = this.entityService.volumeList$.subscribe(vl => this.volumeList = vl);
    }

    ngOnDestroy(): void {
        this.volumeListSubscription.unsubscribe();
    }
}
