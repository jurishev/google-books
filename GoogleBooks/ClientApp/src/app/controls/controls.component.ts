import { Component } from '@angular/core';
import { EntityService } from '../services/entity.service';
import { ViewState, ViewStateService } from '../services/view-state.service';

@Component({
    selector: 'app-controls',
    templateUrl: './controls.component.html',
    styleUrls: ['./controls.component.scss']
})
export class ControlsComponent {

    q = "";
    volumeId = "";
    userId = "";
    shelf = "";

    constructor(
        private readonly entityService: EntityService,
        private readonly stateService: ViewStateService,
    ) { }

    getVolumeList(): void {
        if (this.q) {
            this.entityService.getVolumeList(this.q);
            this.stateService.set(ViewState.VolumeList);
        }
    }

    getVolume(): void {
        if (this.volumeId) {
            this.entityService.getVolume(this.volumeId);
            this.stateService.set(ViewState.Volume);
        }
    }

    getBookshelfList(): void {
        if (this.userId) {
            this.entityService.getBookshelfList(this.userId);
            this.stateService.set(ViewState.BookshelfList);
        }
    }

    getBookshelf(): void {
        if (this.userId && this.shelf) {
            this.entityService.getBookshelf(this.shelf, this.userId);
            this.stateService.set(ViewState.Bookshelf);
        }
    }
}
