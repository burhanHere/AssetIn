import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BoardRoutingModule } from './board-routing.module';
import { NevbarComponent } from '../../shared/components/nevbar/nevbar.component';
import { PageLoaderComponent } from '../../shared/components/page-loader/page-loader.component';
import { AlertCardComponent } from '../../shared/components/alert-card/alert-card.component';
import { MainBoardComponent } from './main-board/main-board.component';
import { TopbarComponent } from '../../shared/components/topbar/topbar.component';
import { ErrorPageComponent } from '../../shared/components/error-page/error-page.component';
import { RouterModule } from '@angular/router';
import { SettingsComponent } from './settings/settings.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [MainBoardComponent, SettingsComponent],
  imports: [
    CommonModule,
    TopbarComponent,
    NevbarComponent,
    BoardRoutingModule,
    PageLoaderComponent,
    AlertCardComponent,
    ErrorPageComponent,
    RouterModule,
    ReactiveFormsModule
  ]
})
export class BoardModule { }
