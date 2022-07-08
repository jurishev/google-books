import { Component, Input } from '@angular/core';
import { Volume } from '../models/volume.model';

@Component({
    selector: 'app-volume-item',
    templateUrl: './volume-item.component.html',
    styleUrls: ['./volume-item.component.scss']
})
export class VolumeItemComponent {

    @Input() volume!: Volume;
}
