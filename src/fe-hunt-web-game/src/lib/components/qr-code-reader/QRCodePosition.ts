import type { Point } from './Point';

/**
 * Represents the position of a QR code in an image.
 */
export interface QRCodePosition {
	/**
	 * The top-left corner of the QR code.
	 */
	topLeft: Point;
	
	/**
	 * The top-right corner of the QR code.
	 */
	topRight: Point;
	
	/**
	 * The bottom-right corner of the QR code.
	 */
	bottomRight: Point;
	
	/**
	 * The bottom-left corner of the QR code.
	 */
	bottomLeft: Point;
}
