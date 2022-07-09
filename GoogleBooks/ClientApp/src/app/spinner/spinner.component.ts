import { Component } from '@angular/core';
import { ThemePalette } from '@angular/material/core';

@Component({
    selector: 'app-spinner',
    templateUrl: './spinner.component.html',
    styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent {

    color: ThemePalette = "accent";
}
