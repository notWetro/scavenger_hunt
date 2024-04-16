import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
	selector: 'app-menu',
	templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {
	model: any[] = [];

	constructor(public layoutService: LayoutService) {}

	ngOnInit() {
		this.model = [
			{
				label: 'Home',
				items: [{ label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/'] }]
			},
			{
				label: 'Get Started',
				items: [
					{
						label: 'Documentation',
						icon: 'pi pi-fw pi-question',
						routerLink: ['/documentation']
					},
					{
						label: 'View Source',
						icon: 'pi pi-fw pi-search',
						url: ['https://github.com/primefaces/sakai-ng'],
						target: '_blank'
					}
				]
			}
		];
	}
}
