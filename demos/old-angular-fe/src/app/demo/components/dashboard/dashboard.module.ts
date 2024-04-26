import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard.component';

import { ButtonModule } from 'primeng/button';
import { StyleClassModule } from 'primeng/styleclass';
import { DashboardsRoutingModule } from './dashboard-routing.module';
import { ScavengerHuntTileComponent } from '../scavenger-hunt-tile/scavenger-hunt-tile.component';

@NgModule({
	imports: [CommonModule, FormsModule, StyleClassModule, ButtonModule, DashboardsRoutingModule, ScavengerHuntTileComponent],
	declarations: [DashboardComponent]
})
export class DashboardModule {}
