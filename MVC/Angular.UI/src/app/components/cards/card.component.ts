import { Component } from '@angular/core'
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-card',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './card.component.html'
})

export class CardComponent{

}