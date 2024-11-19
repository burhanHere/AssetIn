import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-alert-card',
  standalone: true,
  imports: [],
  templateUrl: './alert-card.component.html',
  styleUrl: './alert-card.component.css',
})
export class AlertCardComponent {
  @Input() public title: string = '';
  @Input() public message: string = '';
  @Output() public closeCard: EventEmitter<boolean> = new EventEmitter();
}
