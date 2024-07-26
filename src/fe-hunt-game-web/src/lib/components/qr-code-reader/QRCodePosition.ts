import type { Point } from "./Point";

export interface QRCodePosition {
    topLeft: Point;
    topRight: Point;
    bottomRight: Point;
    bottomLeft: Point;
}