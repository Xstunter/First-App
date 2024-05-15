import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IBoard } from "../models/board";
import { CreateBoardRequest } from "../models/requests/createboard.request";

@Injectable({
    providedIn: 'root'
})

export class HttpBoardService {
    apiUrl = "http://localhost:5000";

    constructor(private http: HttpClient) {}

    createBoard(board : CreateBoardRequest){
        return this.http.post<number>(this.apiUrl + "/CreateBoard", board)
    }
    getBoard(id : number){
        return this.http.get<IBoard>(this.apiUrl + `/GetBoard?boardId=${id}`)
    }
    getAllBoard(){
        return this.http.get<IBoard[]>(this.apiUrl + "/GetAllBoard")
    }
    deleteBoard(id : number){
        const requestBody = { boardId: id };
        return this.http.request<Boolean>('delete', `${this.apiUrl}/DeleteBoard`, { body: requestBody });
    }
    updateBoard(id : number, name : string){
        const requestBody = { boardId: id, name : name, description : ''};
        return this.http.request<Boolean>('patch', `${this.apiUrl}/UpdatedBoard`, { body: requestBody });
    }
}