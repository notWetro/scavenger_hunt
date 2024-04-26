import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ScavengerHuntFormComponent } from './scavenger-hunt-form.component';

@NgModule({
	imports: [RouterModule.forChild([{ path: '', component: ScavengerHuntFormComponent }])],
	exports: [RouterModule]
})
export class ScavengerHuntFormRoutingModule {}
