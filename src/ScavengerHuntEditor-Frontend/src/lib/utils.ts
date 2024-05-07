import type { HintType } from "./models/Hint";
import type { SolutionType } from "./models/Solution";

export function hintTypeToString(type: HintType) {
    if (type === 0) return 'Text'
    if (type === 1) return 'Image'
    return 'NaN'
}

export function solutionTypeToString(type: SolutionType) {
    if (type === 0) return 'QR-Code'
    if (type === 1) return 'Text'
    if (type === 2) return 'Location'
    return 'NaN'
}