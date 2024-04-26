import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
	imports: [
		RouterModule.forChild([
			{
				path: 'scavenger-hunt-form',
				loadChildren: () => import('./scavenger-hunt-form/scavenger-hunt-form.module').then((m) => m.ScavengerHuntFormModule)
			},
			{ path: '**', redirectTo: '/notfound' }
		])
	],
	exports: [RouterModule]
})
export class PagesRoutingModule {}
