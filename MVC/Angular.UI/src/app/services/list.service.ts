import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IList } from "../models/list";
import { ICard } from "../models/card";
import { CreateListRequest } from "../models/requests/createlist.request";

@Injectable({
    providedIn: 'root'
})

export class HttpListService {
    apiUrl = "http://localhost:5000";

    constructor(private http: HttpClient) {}

    createList(list : CreateListRequest){
        return this.http.post<number>(this.apiUrl + "/CreateList", list)
    }
    getList(id : number){
        return this.http.get<IList>(this.apiUrl + `/GetList?listId=${id}`)
    }
    getAllLists(id : number){
        return this.http.get<IList[]>(this.apiUrl + `/GetAllLists?boardId=${id}`)
    }
    deleteList(id : number){
        const requestBody = { listId: id };
        return this.http.request<Boolean>('delete', `${this.apiUrl}/DeleteList`, { body: requestBody });
    }
    updateList(id : number, name : string){
        const requestBody = { listId: id, statusName: name};
        return this.http.request<Boolean>('patch', `${this.apiUrl}/UpdatedList`, { body: requestBody });
    }
}