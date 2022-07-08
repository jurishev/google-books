import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Bookshelf } from '../models/bookshelf.model';
import { Volume } from '../models/volume.model';

@Injectable({
    providedIn: 'root'
})
export class EntityService {

    private readonly bookshelfSubject = new BehaviorSubject<Bookshelf | null | undefined>(undefined);
    private readonly bookshelfListSubject = new BehaviorSubject<Bookshelf[]>([]);

    private readonly volumeSubject = new BehaviorSubject<Volume | null | undefined>(undefined);
    private readonly volumeListSubject = new BehaviorSubject<Volume[]>([]);

    bookshelfList$: Observable<Bookshelf[]>;
    bookshelf$: Observable<Bookshelf | null | undefined>;

    volumeList$: Observable<Volume[]>;
    volume$: Observable<Volume | null | undefined>;

    constructor() {
        this.bookshelfList$ = this.bookshelfListSubject.asObservable();
        this.bookshelf$ = this.bookshelfSubject.asObservable();
        this.volumeList$ = this.volumeListSubject.asObservable();
        this.volume$ = this.volumeSubject.asObservable();
    }

    getVolume(volumeId: string): void {
        fetch(`api/volumes/${volumeId}`)
            .then(response => response.ok ? response.json() : null)
            .then(json => this.volumeSubject.next(json as Volume))
            .catch(error => console.error(error));
    }
}
