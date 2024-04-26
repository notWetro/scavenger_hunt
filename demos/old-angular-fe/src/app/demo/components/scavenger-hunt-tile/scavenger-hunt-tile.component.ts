import { Component, Input } from '@angular/core';

@Component({
	selector: 'app-scavenger-hunt-tile',
	templateUrl: './scavenger-hunt-tile.component.html',
	standalone: true,
	styleUrl: './scavenger-hunt-tile.component.scss'
})
export class ScavengerHuntTileComponent {
	@Input() huntName: string;
	@Input() numberOfStations: number;
	@Input() numberOfParticipants: number;
}
