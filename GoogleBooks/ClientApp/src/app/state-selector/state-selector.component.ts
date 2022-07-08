import { Component } from '@angular/core';
import { State, StateService } from '../services/state.service';

@Component({
    selector: 'app-state-selector',
    templateUrl: './state-selector.component.html',
    styleUrls: ['./state-selector.component.scss']
})
export class StateSelectorComponent {

    constructor(
        readonly state: StateService
    ) { }

    set(arg: State): void {
        this.state.set(arg);
    }
}
