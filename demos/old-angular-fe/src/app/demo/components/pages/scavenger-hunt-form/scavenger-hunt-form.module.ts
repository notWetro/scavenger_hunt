import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScavengerHuntFormRoutingModule } from './scavenger-hunt-form-routing.module';
import { ScavengerHuntFormComponent } from './scavenger-hunt-form.component';
import { InputMaskModule } from 'primeng/inputmask';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';

@NgModule({
	imports: [
		CommonModule,
		ScavengerHuntFormRoutingModule,
		InputMaskModule,
		InputGroupModule,
		InputGroupAddonModule,
		CheckboxModule,
		FormsModule,
		InputTextModule,
		ButtonModule
	],
	declarations: [ScavengerHuntFormComponent]
})
export class ScavengerHuntFormModule {}
