export interface Hint {
	hintType: HintType;
	data: string;
	additionalData?: string
}

export enum HintType {
	Text,
	Image,
	Video,
	Audio
}
