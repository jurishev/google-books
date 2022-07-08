import { Component } from '@angular/core';
import { EntityService } from '../services/entity.service';

@Component({
    selector: 'app-volume-list-controls',
    templateUrl: './volume-list-controls.component.html',
    styleUrls: ['./volume-list-controls.component.scss']
})
export class VolumeListControlsComponent {

    q = "";

    constructor(
        private readonly entityService: EntityService
    ) { }

    search(): void {
        if (this.q) {
            this.entityService.getVolumeList(this.q);
        }
    }
}
