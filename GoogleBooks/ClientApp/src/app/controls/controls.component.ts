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

    sortBy: "title" | "author" | undefined;

    constructor(
        private readonly entityService: EntityService
    ) { }

    isGetVolumesDisabled(): boolean {
        return !this.volumeId &&
            !this.volumeQuery.title &&
            !this.volumeQuery.author &&
            !this.volumeQuery.subject &&
            !this.volumeQuery.publisher &&
            !this.volumeQuery.isbn;
    }

    getVolumes(): void {
        if (!this.isGetVolumesDisabled()) {
            if (!this.volumeId) {
                this.entityService.getVolumeList(this.volumeQuery);
            }
            else {
                this.entityService.getVolume(this.volumeId);
            }
        }
    }

    getBookshelves(): void {
        if (this.userId && this.shelf) {
            this.entityService.getBookshelf(this.shelf, this.userId);
        }
        else if (this.userId) {
            this.entityService.getBookshelfList(this.userId);
        }
    }

    onSortByChange(): void {
        this.entityService.sortVolumeListBy(this.sortBy);
    }
}
