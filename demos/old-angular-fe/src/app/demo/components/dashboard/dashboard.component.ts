import { Component, OnDestroy } from '@angular/core';
import { debounceTime, Subscription } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { Router } from '@angular/router';

@Component({
	templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnDestroy {
	subscription!: Subscription;

	hunts = [];

	constructor(
		public layoutService: LayoutService,
		private router: Router
	) {
		this.subscription = this.layoutService.configUpdate$.pipe(debounceTime(25)).subscribe();
		this.generateHunts();
	}

	addNewHunt(): void {
		this.router.navigate(['/pages/scavenger-hunt-form']).then();
	}

	generateHunts(): void {
		const numberOfHunts = 18; // Change this number as needed

		for (let i = 1; i <= numberOfHunts; i++) {
			this.hunts.push({
				name: `Scavenger Hunt ${i}`,
				stations: [`Station 1`, `Station 2`], // Example stations
				participants: [`Participant 1`, `Participant 2`, `Participant 3`, `Participant 4`] // Example participants
			});
		}
	}

	ngOnDestroy() {
		if (this.subscription) {
			this.subscription.unsubscribe();
		}
	}
}
