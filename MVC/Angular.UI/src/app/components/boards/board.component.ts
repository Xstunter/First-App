import { Component } from '@angular/core'
import { IBoard } from '../../models/board'
import { HttpService } from '../../http.sevice';
import { CreateBoardRequest } from '../../models/requests/createboard.request';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-board',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './board.component.html'
})

export class BoardComponent {

    board: IBoard = { boardId: 0, name: '', description: '' };
    showCreateBoardModal = false;

    constructor(private httpService: HttpService) {}

    openCreateBoardModal() {
        this.showCreateBoardModal = true;
    }
    
      closeCreateBoardModal() {
        this.showCreateBoardModal = false;
    }

    getBoardNgOnInit(){
        this.httpService.getBoard(this.board.boardId).subscribe(result=>{
            this.board=result;
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