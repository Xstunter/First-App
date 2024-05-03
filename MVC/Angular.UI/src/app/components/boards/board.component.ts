import {Component, Input} from '@angular/core'
import { IBoard } from '../../models/board'
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';

@Component({
    selector: 'app-board',
    standalone: true,
    templateUrl: './board.component.html'
})

export class BoardComponent {
    readonly _url = 'http://localhost:5000/GetBoard';

    constructor(private http: HttpClient) {}

    getBoard() : Observable<IBoard>{
        return this.http.get<IBoard>(this._url + '?boardId=1');
    }

}  