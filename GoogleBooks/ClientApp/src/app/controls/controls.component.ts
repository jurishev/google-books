import { Component } from '@angular/core';
import { VolumeQuery } from '../models/volume-query.model';
import { EntityService } from '../services/entity.service';

@Component({
    selector: 'app-controls',
    templateUrl: './controls.component.html',
    styleUrls: ['./controls.component.scss']
})
export class ControlsComponent {

    volumeQuery = new VolumeQuery();

    volumeId = "";
    userId = "";
    shelf = "";

    constructor(
        private readonly entityService: EntityService
    ) { }

    isGetVolumeListDisabled(): boolean {
        return !this.volumeQuery.title &&
            !this.volumeQuery.author &&
            !this.volumeQuery.publisher;
    }

    getVolumeList(): void {
        if (!this.isGetVolumeListDisabled()) {
            this.entityService.getVolumeList(this.volumeQuery);
        }
    }

    getVolume(): void {
        if (this.volumeId) {
            this.entityService.getVolume(this.volumeId);
        }
    }

    getBookshelfList(): void {
        if (this.userId) {
            this.entityService.getBookshelfList(this.userId);
        }
    }

    getBookshelf(): void {
        if (this.userId && this.shelf) {
            this.entityService.getBookshelf(this.shelf, this.userId);
        }
    }
}
