import { Component, Input, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Volume } from '../models/volume.model';
import { EntityService } from '../services/entity.service';

@Component({
    selector: 'app-volume-view',
    templateUrl: './volume-view.component.html',
    styleUrls: ['./volume-view.component.scss']
})
export class VolumeViewComponent implements OnDestroy {

    private readonly volumeSubscription: Subscription;

    volume: Volume | null | undefined;

    constructor(
        private readonly entityService: EntityService
    ) {
        this.volumeSubscription = this.entityService.volume$.subscribe(v => this.volume = v);
    }

    ngOnDestroy(): void {
        this.volumeSubscription.unsubscribe();
    }
}
