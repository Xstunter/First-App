import { Component } from '@angular/core'
import { IList } from '../../models/list';
import { HttpListService } from '../../services/list.service';
import { CreateListRequest } from '../../models/requests/createlist.request';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Input } from '@angular/core';
import { CardComponent } from '../cards/card.component';

@Component({
    selector: 'app-list',
    standalone: true,
    imports: [CommonModule, CardComponent, FormsModule],
    templateUrl: './list.component.html'
})

export class ListComponent {

    @Input() boardId!: number;

    list : IList = {listId : 0, statusName : '', boardId : 0}; //For change one list
    listMas : IList[] = []; //For GetAllList
    
    showCreateListModal = false;
    isMenuOpen: { [key: string]: boolean } = {};

    constructor(private httpService: HttpListService) {}

    openCreateListModal() {
        this.showCreateListModal = true;
    }
    
      closeCreateListModal() {
        this.showCreateListModal = false;
    }

    toggleMenu(list: IList) {
        if (this.isMenuOpen[list.listId]) {
            this.isMenuOpen[list.listId] = false;
        } else {
            this.isMenuOpen[list.listId] = true;
        }
    }
    getAllListsNgOnInit(){
        this.httpService.getAllLists(this.boardId).subscribe(result=>{
            this.listMas = result;
            console.log(result);
        })
    }

    createListNgOnInit(){
        let list : CreateListRequest = {StatusName : this.list.statusName, BoardId : this.boardId}

        this.httpService.createList(list).subscribe(result=>{
            this.list.listId = result;
            console.log(result);
            this.getAllListsNgOnInit();
        })
        this.closeCreateListModal();          
    }
    deleteListNgOnInit(list : IList){
        let isDeleted : Boolean = false;

        this.httpService.deleteList(list.listId).subscribe(result=>{
            isDeleted=result;
            console.log(result);
            this.getAllListsNgOnInit();
        })          
    }
    updateListNgOnInit(list : IList){
        let isUpdated : Boolean = false;

        this.httpService.updateList(list.listId).subscribe(result=>{
            isUpdated=result;
            console.log(result);
            this.getAllListsNgOnInit();
        })          
    }
}  