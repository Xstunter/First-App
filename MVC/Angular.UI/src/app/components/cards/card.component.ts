import { ChangeDetectorRef, Component } from '@angular/core'
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Input } from '@angular/core';
import { ICard } from '../../models/card';
import { HttpCardService } from '../../services/card.service';
import { CreateCardRequest } from '../../models/requests/createcard.request';
import { ListComponent } from '../lists/list.component';

@Component({
    selector: 'app-card',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './card.component.html'
})

export class CardComponent{
    @Input() listId!: number;
    @Input() listsName! : {name : string, id : number}[];
    @Input() boardId : number = 0;

    showCreateCardModal = false;
    showEditModal = false;

    selectedListId!: number;

    card : ICard = {cardId : 0, name : '', description : '', priority : '',  listId : 0, createdAt : ''};
    cardMas : ICard[] = [];

    isMenuOpen: { [key: string]: boolean } = {};
    cardCountPerList: { [key: number]: number } = {};

    constructor(private httpService: HttpCardService, private listComponent : ListComponent) {} // Bad listComponent inject, this only for getAllListsNgOnInit for method changeListForCardNgOnInit
    
    onSaveClick() {
        this.updateCardNgOnInit(this.card.cardId, this.card.name, this.card.description, this.card.priority);
        this.changeListForCardNgOnInit(this.card.cardId, this.selectedListId);
    }

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
            this.countCardsPerList();
        })
    }
    createCardNgOnInit(){
        let card : CreateCardRequest = { Name : this.card.name, Description : this.card.description, Priority : this.card.priority, ListId : this.listId, BoardId : this.boardId}

        this.httpService.createCard(card).subscribe(result=>{
            this.card.cardId = result;
            console.log(result);
            this.getAllCardsNgOnInit();
        })
        this.closeCreateCardModal();          
    }
    deleteCardNgOnInit(card : ICard){
        let isDeleted : Boolean = false;

        this.httpService.deleteCard(card.cardId, this.boardId).subscribe(result=>{
            isDeleted=result;
            console.log(result);
            this.getAllCardsNgOnInit();
        })          
    }
    updateCardNgOnInit(id : number, cardName : string, cardDescription : string, cardPriority : string){
        let isUpdated : Boolean = false;

        this.httpService.updateCard(id, cardName, cardDescription, cardPriority, this.boardId).subscribe(result=>{
            isUpdated=result;
            console.log(result);
            this.getAllCardsNgOnInit();
        })         
        this.closeEditModal();
    }
    changeListForCardNgOnInit(cardId : number, listId : number){
        let isChanged : Boolean = false;

        this.httpService.changeListForCard(cardId, listId, this.boardId).subscribe(result=>{
            isChanged=result;
            console.log(result);
            this.listComponent.getAllListsNgOnInit();
        })         
    }
    formatDateTime(dateTimeString: string): string {
        const options: Intl.DateTimeFormatOptions = {
          year: 'numeric',
          month: 'long',
          day: 'numeric',
          weekday: 'long' as 'long' 
        };
        const dateTime = new Date(dateTimeString);
        return dateTime.toLocaleDateString('en-US', options);
    }

    countCardsPerList() {
        this.cardCountPerList = {};
        this.listsName.forEach(list => {
          this.httpService.getAllCards(list.id).subscribe(cards => {
            this.cardCountPerList[list.id] = cards.length;
          });
        });
    }

}