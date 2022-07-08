import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { EntityService } from '../services/entity.service';

@Component({
    selector: 'app-volume-controls',
    templateUrl: './volume-controls.component.html',
    styleUrls: ['./volume-controls.component.scss']
})
export class VolumeControlsComponent implements OnDestroy {

    private readonly volumeSubscription: Subscription;

    volumeId = "";

    constructor(
        private readonly entityService: EntityService
    ) {
        this.volumeSubscription = this.entityService.volume$.subscribe(v => this.volumeId = v ? v.id : "");
    }

    ngOnDestroy(): void {
        this.volumeSubscription.unsubscribe();
    }

    search(): void {
        if (this.volumeId) {
            this.entityService.getVolume(this.volumeId);
        }
    }
}
