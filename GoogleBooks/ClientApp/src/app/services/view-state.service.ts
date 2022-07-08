import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators'

@Injectable({
    providedIn: 'root'
})
export class ViewStateService {

    private readonly viewStateSubject = new BehaviorSubject<ViewState>(ViewState.None);

    isSpinner$ = this.trueFor(ViewState.Spinner);
    isNoResults$ = this.trueFor(ViewState.NoResults);
    isVolumeList$ = this.trueFor(ViewState.VolumeList);
    isVolume$ = this.trueFor(ViewState.Volume);
    isBookshelfList$ = this.trueFor(ViewState.BookshelfList);
    isBookshelf$ = this.trueFor(ViewState.Bookshelf);

    set(state: ViewState): void {
        this.viewStateSubject.next(state);
    }

    private trueFor(state: ViewState): Observable<boolean> {
        return this.viewStateSubject
            .asObservable()
            .pipe(
                map(s => s == state)
            );
    }
}

export enum ViewState {
    None,
    Spinner,
    NoResults,
    VolumeList,
    Volume,
    BookshelfList,
    Bookshelf,
}
