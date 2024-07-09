import type { Hint } from './Hint';
import type { Solution } from './Solution';

export interface Assignment {
	id: number;
	hint: Hint;
	solution: Solution;
}
