import type { Assignment } from './Assignment';

export interface Hunt {
	title: string;
	description: string;
	assignments: Assignment[];
}
