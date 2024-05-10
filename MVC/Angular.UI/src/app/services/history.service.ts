import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IHistory } from "../models/history";

@Injectable({
    providedIn: 'root'
})

export class HttpHistoryService {
    apiUrl = "http://localhost:5000";

    constructor(private http: HttpClient) {}

    getHistoryCard(id : number){
        return this.http.get<IHistory>(this.apiUrl + `/GetHistoryCard?historyId=${id}`)
    }
    getBoardHistory(id : number){
        return this.http.get<IHistory[]>(this.apiUrl + `/GetBoardHistory?boardId=${id}`)
    }
}