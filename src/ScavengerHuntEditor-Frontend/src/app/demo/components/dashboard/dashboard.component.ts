import { Component, OnDestroy } from '@angular/core';
import { debounceTime, Subscription } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { Router } from '@angular/router';

@Component({
	templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnDestroy {
	subscription!: Subscription;

	//scavengerHunts$!: Observable<string>;

	constructor(
		public layoutService: LayoutService,
		private router: Router
	) {
		this.subscription = this.layoutService.configUpdate$.pipe(debounceTime(25)).subscribe();
	}

	addNewHunt(): void {
		console.log('test');
		this.router.navigate(['/pages/scavenger-hunt-form']).then();
	}

	ngOnDestroy() {
		if (this.subscription) {
			this.subscription.unsubscribe();
		}
	}
}
