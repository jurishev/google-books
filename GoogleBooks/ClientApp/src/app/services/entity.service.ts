import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Bookshelf } from '../models/bookshelf.model';
import { VolumeQuery } from '../models/volume-query.model';
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
        this.set(ViewState.Spinner);
        fetch(`api/volumes/${volumeId}`)
            .then(response => response.ok ? response.json() : undefined)
            .then(json => {
                this.volumeSubject.next(json as Volume);
                this.set(json ? ViewState.Volume : ViewState.NoResults);
            })
            .catch(error => console.error(error));
    }

    getVolumeList(query: VolumeQuery): void {
        this.set(ViewState.Spinner);
        const terms = this.getTerms(query);
        if (!terms) {
            console.warn("No search terms for volumes.");
            this.set(ViewState.NoResults);
            return;
        }
        fetch(`api/volumes?${terms}`)
            .then(response => response.ok ? response.json() : undefined)
            .then(json => {
                this.volumeListSubject.next(json as Volume[]);
                this.set(json ? ViewState.VolumeList : ViewState.NoResults);
            })
            .catch(error => console.error(error));
    }

    private getTerms(query: VolumeQuery): string {
        const terms = new Array<string>();
        const addTerm = function (key: string, value: string) {
            if (value) {
                terms.push(`${key}=${value}`);
            }
        }
        addTerm("title", query.title);
        addTerm("author", query.author);
        addTerm("publisher", query.publisher);
        return terms.join("&");
    }

    getBookshelf(shelf: string, userId: string): void {
        this.set(ViewState.Spinner);
        fetch(`api/bookshelves/${shelf}/user/${userId}`)
            .then(response => response.ok ? response.json() : undefined)
            .then(json => {
                this.bookshelfSubject.next(json as Bookshelf);
                this.set(json ? ViewState.Bookshelf : ViewState.NoResults);
            })
            .catch(error => console.error(error));
    }

    getBookshelfList(userId: string): void {
        this.set(ViewState.Spinner);
        fetch(`api/bookshelves/user/${userId}`)
            .then(response => response.ok ? response.json() : undefined)
            .then(json => {
                this.bookshelfListSubject.next(json as Bookshelf[]);
                this.set(json ? ViewState.BookshelfList : ViewState.NoResults);
            })
            .catch(error => console.error(error));
    }

    private set(state: ViewState): void {
        this.viewStateService.set(state);
    }
}
