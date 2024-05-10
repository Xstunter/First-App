import { Component } from '@angular/core'
import { CommonModule } from '@angular/common'
import { FormsModule } from '@angular/forms'
import { HttpHistoryService } from '../../services/history.service'
import { Input } from '@angular/core'
import { IHistory } from '../../models/history'

@Component({
    selector : "app-history",
    standalone : true,
    imports : [CommonModule, FormsModule],
    templateUrl : './history.component.html'
})

export class HistoryComponent{
    @Input() boardId! : number;

    historyMas : IHistory[] = [];
    historyVisible = false;

    constructor(private httpService: HttpHistoryService) {}

    ngOnInit() {
        this.getAllBoardHistoryNgOnInit();
    }

    getAllBoardHistoryNgOnInit(){
        this.httpService.getBoardHistory(this.boardId).subscribe(result=>{
            this.historyMas = result;
            console.log(result);
        })
    }
    
    toggleHistory() {
        this.historyVisible = !this.historyVisible;
        if (this.historyVisible) {
          this.getAllBoardHistoryNgOnInit();
        } else {
          this.historyMas = []; // Очистка истории при скрытии панели
        }
      }

}