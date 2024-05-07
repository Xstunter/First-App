import { Component } from '@angular/core'
import { IBoard } from '../../models/board'
import { HttpBoardService } from '../../services/board.sevice';
import { CreateBoardRequest } from '../../models/requests/createboard.request';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ListComponent } from '../lists/list.component';

@Component({
    selector: 'app-board',
    standalone: true,
    imports: [CommonModule, FormsModule, ListComponent],
    templateUrl: './board.component.html'
})

export class BoardComponent {

    board : IBoard = {boardId : 1, name : 'Aboba    ', description : ''};
    showCreateBoardModal = false;

    constructor(private httpService: HttpBoardService) {}

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
    createBoardNgOnInit(){
        let board : CreateBoardRequest = {Name : this.board.name, Description : this.board.description}

        this.httpService.createBoard(board).subscribe(result=>{
            this.board.boardId=result;
            console.log(result);
        })
        this.closeCreateBoardModal();          
    }
    deleteBoardNgOnInit(){
        let isDeleted : Boolean = false;

        this.httpService.deleteBoard(this.board.boardId).subscribe(result=>{
            isDeleted=result;
            console.log(result);
        })          
    }
}  