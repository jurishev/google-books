import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Volume } from '../models/volume.model';
import { EntityService } from '../services/entity.service';
import { State, StateService } from '../services/state.service';

@Component({
    selector: 'app-volume-list-view',
    templateUrl: './volume-list-view.component.html',
    styleUrls: ['./volume-list-view.component.scss']
})
export class VolumeListViewComponent implements OnDestroy {

    private readonly volumeListSubscription: Subscription;

    volumeList: Volume[] | null | undefined;

    constructor(
        private readonly entityService: EntityService,
        private readonly stateService: StateService,
    ) {
        this.volumeListSubscription = this.entityService.volumeList$.subscribe(vl => this.volumeList = vl);
    }

    ngOnDestroy(): void {
        this.volumeListSubscription.unsubscribe();
    }

    selectVolume(id: string): void {
        this.entityService.setVolume(id);
        this.stateService.set(State.Volume);
    }
}
