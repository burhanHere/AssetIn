import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-alert-card',
  standalone: true,
  imports: [],
  templateUrl: './alert-card.component.html',
  styleUrl: './alert-card.component.css',
})
export class AlertCardComponent {
  @Input() public errorTitle: string = '';
  @Input() public errorMessage: string = '';
  @Output() public closeCard: EventEmitter<boolean> = new EventEmitter();
}
