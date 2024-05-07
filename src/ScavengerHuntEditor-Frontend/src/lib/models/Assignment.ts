import type { Hint } from "./Hint";
import type { Solution } from "./Solution";

export interface Assignment {
    hint: Hint;
    solution: Solution;
}