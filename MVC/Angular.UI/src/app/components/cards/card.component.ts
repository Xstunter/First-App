import { Component } from '@angular/core'
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Input } from '@angular/core';
import { ICard } from '../../models/card';
import { HttpCardService } from '../../services/card.service';
import { CreateCardRequest } from '../../models/requests/createcard.request';

@Component({
    selector: 'app-card',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './card.component.html'
})

export class CardComponent{
    @Input() listId!: number;

    showCreateCardModal = false;
    showEditModal = false;

    card : ICard = {cardId : 0, name : '', description : '', priority : '',  listId : 0, createdAt : ''};
    cardMas : ICard[] = [];

    isMenuOpen: { [key: string]: boolean } = {};

    constructor(private httpService: HttpCardService) {}
    
    openEditModal(id : number, editName : string, editDescription : string, editPriority : string) {
        this.card.cardId = id;
        this.card.name = editName;
        this.card.description = editDescription;
        this.card.priority = editPriority;
        this.showEditModal = true;
    }
    closeEditModal() {
        this.showEditModal = false;
    }

    openCreateCardModal() {
        this.showCreateCardModal = true;
    }
    closeCreateCardModal() {
        this.showCreateCardModal = false;
    }

    toggleMenu(card: ICard) {
        if (this.isMenuOpen[card.cardId]) {
            this.isMenuOpen[card.cardId] = false;
        } else {
            this.isMenuOpen[card.cardId] = true;
        }
    }
    ngOnInit() {
        this.getAllCardsNgOnInit();
    }
    getAllCardsNgOnInit(){
        this.httpService.getAllCards(this.listId).subscribe(result=>{
            this.cardMas = result;
            console.log(result);
        })
    }

    createCardNgOnInit(){
        let card : CreateCardRequest = { Name : this.card.name, Description : this.card.description, Priority : this.card.priority, ListId : this.listId}

        this.httpService.createCard(card).subscribe(result=>{
            this.card.cardId = result;
            console.log(result);
            this.getAllCardsNgOnInit();
        })
        this.closeCreateCardModal();          
    }
    deleteCardNgOnInit(card : ICard){
        let isDeleted : Boolean = false;

        this.httpService.deleteCard(card.cardId).subscribe(result=>{
            isDeleted=result;
            console.log(result);
            this.getAllCardsNgOnInit();
        })          
    }
    updateCardNgOnInit(id : number, cardName : string, cardDescription : string, cardPriority : string){
        let isUpdated : Boolean = false;

        this.httpService.updateCard(id, cardName, cardDescription, cardPriority).subscribe(result=>{
            isUpdated=result;
            console.log(result);
            this.getAllCardsNgOnInit();
        })         
    }

}