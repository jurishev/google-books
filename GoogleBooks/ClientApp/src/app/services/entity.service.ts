import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Bookshelf } from '../models/bookshelf.model';
import { Volume } from '../models/volume.model';
import { ViewState, ViewStateService } from './view-state.service';

@Injectable({
    providedIn: 'root'
})
export class EntityService {

    private readonly bookshelfSubject = new BehaviorSubject<Bookshelf | undefined>(undefined);
    private readonly bookshelfListSubject = new BehaviorSubject<Bookshelf[] | undefined>([]);

    private readonly volumeSubject = new BehaviorSubject<Volume | undefined>(undefined);
    private readonly volumeListSubject = new BehaviorSubject<Volume[] | undefined>(undefined);

    bookshelfList$: Observable<Bookshelf[] | undefined>;
    bookshelf$: Observable<Bookshelf | undefined>;

    volumeList$: Observable<Volume[] | undefined>;
    volume$: Observable<Volume | undefined>;

    constructor(
        private readonly viewStateService: ViewStateService
    ) {
        this.bookshelfList$ = this.bookshelfListSubject.asObservable();
        this.bookshelf$ = this.bookshelfSubject.asObservable();
        this.volumeList$ = this.volumeListSubject.asObservable();
        this.volume$ = this.volumeSubject.asObservable();
    }

    getVolume(volumeId: string): void {
        fetch(`api/volumes/${volumeId}`)
            .then(response => response.ok ? response.json() : undefined)
            .then(json => {
                this.volumeSubject.next(json as Volume);
                this.checkNoResults(json);
            })
            .catch(error => console.error(error));
    }

    getVolumeList(q: string): void {
        fetch(`api/volumes?q=${q}`)
            .then(response => response.ok ? response.json() : undefined)
            .then(json => {
                this.volumeListSubject.next(json as Volume[]);
                this.checkNoResults(json);
            })
            .catch(error => console.error(error));
    }

    getBookshelf(shelf: string, userId: string): void {
        fetch(`api/bookshelves/${shelf}/user/${userId}`)
            .then(response => response.ok ? response.json() : undefined)
            .then(json => {
                this.bookshelfSubject.next(json as Bookshelf);
                this.checkNoResults(json);
            })
            .catch(error => console.error(error));
    }

    getBookshelfList(userId: string): void {
        fetch(`api/bookshelves/user/${userId}`)
            .then(response => response.ok ? response.json() : undefined)
            .then(json => {
                this.bookshelfListSubject.next(json as Bookshelf[]);
                this.checkNoResults(json);
            })
            .catch(error => console.error(error));
    }

    private checkNoResults(json: any): void {
        if (!json) {
            this.viewStateService.set(ViewState.NoResults);
        }
    }
}
