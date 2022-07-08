import { Component } from '@angular/core';
import { ViewStateService } from './services/view-state.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {

    title = 'GoogleBooks';

    constructor(
        readonly state: ViewStateService
    ) { }
}
