import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators'

@Injectable({
    providedIn: 'root'
})
export class StateService {

    private readonly stateSubject = new BehaviorSubject<State>(State.VolumeList);

    isVolumeList$ = this.trueFor(State.VolumeList);
    isVolume$ = this.trueFor(State.Volume);
    isBookshelfList$ = this.trueFor(State.BookshelfList);
    isBookshelf$ = this.trueFor(State.Bookshelf);

    set(state: State): void {
        this.stateSubject.next(state);
    }

    private trueFor(state: State): Observable<boolean> {
        return this.stateSubject
            .asObservable()
            .pipe(
                map(s => s == state)
            );
    }
}

export enum State {
    None,
    VolumeList,
    Volume,
    BookshelfList,
    Bookshelf,
}
