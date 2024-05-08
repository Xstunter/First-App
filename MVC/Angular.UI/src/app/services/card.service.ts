import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ICard } from "../models/card";
import { CreateCardRequest } from "../models/requests/createcard.request";

@Injectable({
    providedIn: 'root'
})

export class HttpCardService {
    apiUrl = "http://localhost:5000";

    constructor(private http: HttpClient) {}

    createCard(card : CreateCardRequest){
        return this.http.post<number>(this.apiUrl + "/CreateCard", card)
    }
    getCard(id : number){
        return this.http.get<ICard>(this.apiUrl + `/GetCard?cardId=${id}`)
    }
    getAllCards(id : number){
        return this.http.get<ICard[]>(this.apiUrl + `/GetAllListsCard?listId=${id}`)
    }
    deleteCard(id : number){
        const requestBody = { cardId: id };
        return this.http.request<Boolean>('delete', `${this.apiUrl}/DeleteCard`, { body: requestBody });
    }
    updateCard(id : number, cardName : string, cardDescription : string, cardPriority : string){
        const requestBody = { cardId: id, name : cardName, description : cardDescription, priority : cardPriority};
        return this.http.request<Boolean>('patch', `${this.apiUrl}/UpdatedCard`, { body: requestBody });
    }
}