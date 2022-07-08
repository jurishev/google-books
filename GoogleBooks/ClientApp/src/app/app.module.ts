import { NgModule } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';

import { AppComponent } from './app.component';
import { VolumeViewComponent } from './volume-view/volume-view.component';
import { VolumeControlsComponent } from './volume-controls/volume-controls.component';
import { VolumeItemComponent } from './volume-item/volume-item.component';
import { NoResultsComponent } from './no-results/no-results.component';
import { VolumeListViewComponent } from './volume-list-view/volume-list-view.component';
import { VolumeListControlsComponent } from './volume-list-controls/volume-list-controls.component';
import { StateSelectorComponent } from './state-selector/state-selector.component';

@NgModule({
    declarations: [
        AppComponent,
        VolumeViewComponent,
        VolumeControlsComponent,
        VolumeItemComponent,
        NoResultsComponent,
        VolumeListViewComponent,
        VolumeListControlsComponent,
        StateSelectorComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        BrowserModule,
        BrowserAnimationsModule,
        MatIconModule,
        MatButtonModule,
        MatCardModule,
        MatDividerModule,
        MatFormFieldModule,
        MatInputModule,
        MatListModule,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
