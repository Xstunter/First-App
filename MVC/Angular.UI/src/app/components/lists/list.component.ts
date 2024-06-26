import { Component } from '@angular/core'
import { IList } from '../../models/list';
import { HttpListService } from '../../services/list.service';
import { CreateListRequest } from '../../models/requests/createlist.request';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Input } from '@angular/core';
import { CardComponent } from '../cards/card.component';
import { Store } from '@ngrx/store';
import { getlists } from '../ngrx/selectors/list.select';
import { loadList } from '../ngrx/actions/list.action';


@Component({
    selector: 'app-list',
    standalone: true,
    imports: [CommonModule, FormsModule, CardComponent],
    templateUrl: './list.component.html'
})

export class ListComponent {

    @Input() boardId!: number;

    list : IList = {listId : 0, statusName : '', boardId : 0}; //For change one list
    listMas : IList[] = []; //For GetAllList
    listsName : {name : string, id : number}[] = []; // For Change list for card

    showCreateListModal = false;
    showEditModal = false;

    isMenuOpen: { [key: string]: boolean } = {};

    constructor(private httpService: HttpListService, private store: Store) {}
    
    openEditModal(id : number, editName : string) {
        this.list.listId = id;
        this.list.statusName = editName;
        this.showEditModal = true;
    }
    closeEditModal() {
        this.showEditModal = false;
    }

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
    
    ngOnInit() {
        this.getAllListsNgOnInit();    
    }

    ngDoCheck() {
            this.listMas.forEach(list => {
            this.saveList(list.statusName, list.listId);
            });
    }

    saveList(name: string, id: number) {

            const existingList = this.listsName.find(list => list.name === name);

            if (existingList) {
            existingList.id = id;
            } else {
            this.listsName.push({ name, id });
            }
    }

    updateListsName() {
        this.listsName = [];
        this.listMas.forEach(list => {
          this.saveList(list.statusName, list.listId);
        });
    }

    getAllListsNgOnInit(){
        this.store.dispatch(loadList({id : this.boardId}));
        this.store.select(getlists).subscribe(item=>{
            this.listMas = item;
        });
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
    updateListNgOnInit(id : number, editName : string){
        let isUpdated : Boolean = false;

        this.httpService.updateList(id, editName).subscribe(result=>{
            isUpdated=result;
            console.log(result);
            this.getAllListsNgOnInit();
        })
        this.closeEditModal();            
    }
}  