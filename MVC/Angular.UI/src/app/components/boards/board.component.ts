import { Component } from '@angular/core'
import { IBoard } from '../../models/board'
import { HttpBoardService } from '../../services/board.sevice';
import { CreateBoardRequest } from '../../models/requests/createboard.request';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ListComponent } from '../lists/list.component';
import { HistoryComponent } from '../histories/history.component';

@Component({
    selector: 'app-board',
    standalone: true,
    imports: [CommonModule, FormsModule, ListComponent, HistoryComponent],
    templateUrl: './board.component.html'
})

export class BoardComponent {

    board : IBoard = {boardId : 0, name : '', description : ''};
<<<<<<< HEAD
=======
    boardMas : IBoard[] = [];
>>>>>>> feature/board
    showCreateBoardModal = false;
    showEditBoardModal = false;
    
    boardId! : number;

    constructor(private httpService: HttpBoardService) {}

    
    ngOnInit(): void {
        this.getAllBoardNgOnInit();
    }

    openEditBoardModal(id : number) {
        this.showEditBoardModal = true;
        this.boardId = id;
    }
    
    closeEditBoardModal() {
        this.showEditBoardModal = false;
    }

    openCreateBoardModal() {
        this.showCreateBoardModal = true;
    }
    
    closeCreateBoardModal() {
        this.showCreateBoardModal = false;
    }

    getBoardNgOnInit(){
        this.httpService.getBoard(this.board.boardId).subscribe(result=>{
            this.board = result;
            console.log(result);
        })
    }
    getAllBoardNgOnInit(){
        this.httpService.getAllBoard().subscribe(result=>{
            this.boardMas = result;
            console.log(result);
        })
    }
    updateBoardNgOnInit(){
        this.httpService.updateBoard(this.boardId, this.board.name).subscribe(result=>{
            console.log(result);
            this.getAllBoardNgOnInit(); 
        })
        this.closeEditBoardModal();  
    }
    createBoardNgOnInit(){
        let board : CreateBoardRequest = {Name : this.board.name, Description : this.board.description}

        this.httpService.createBoard(board).subscribe(result=>{
            console.log(result);
            this.getAllBoardNgOnInit(); 
        })
        this.closeCreateBoardModal();         
    }
    deleteBoardNgOnInit(id : number){
        let isDeleted : Boolean = false;

        this.httpService.deleteBoard(id).subscribe(result=>{
            isDeleted=result;
            console.log(result);
            this.getAllBoardNgOnInit();
        })          
    }
    selectBoardNgOnInit(id : number, name : string){
        this.board.boardId = id;
        this.board.name = name;
        console.log(this.board.boardId);
    }
}  