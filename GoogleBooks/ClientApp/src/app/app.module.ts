import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from "@angular/forms";

import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';

import { AppComponent } from './app.component';
import { ControlsComponent } from './controls/controls.component';
import { ViewsComponent } from './views/views.component';
import { VolumeComponent } from './volume/volume.component';
import { BookshelfComponent } from './bookshelf/bookshelf.component';
import { NoResultsComponent } from './no-results/no-results.component';

import { JoinPipe } from './pipes/join.pipe';

@NgModule({
    declarations: [
        AppComponent,
        ControlsComponent,
        ViewsComponent,
        VolumeComponent,
        BookshelfComponent,
        NoResultsComponent,
        JoinPipe,
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
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
