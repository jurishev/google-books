import { Component } from '@angular/core';
import { EntityService } from '../services/entity.service';

@Component({
    selector: 'app-volume-controls',
    templateUrl: './volume-controls.component.html',
    styleUrls: ['./volume-controls.component.scss']
})
export class VolumeControlsComponent {

    volumeId = "";

    constructor(
        private readonly entityService: EntityService
    ) { }

    search(): void {
        if (this.volumeId) {
            this.entityService.getVolume(this.volumeId);
        }
    }
}
