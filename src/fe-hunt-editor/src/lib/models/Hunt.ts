import type { Assignment } from './Assignment';

export interface Hunt {
	id: number;
	title: string;
	description: string;
	assignments: Assignment[];
}
