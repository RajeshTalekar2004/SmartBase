export class CgstModel {
    cgstId: number;
    cgstDetail: string;
    cgstRate: number;

    constructor(
        cgstId: number,
        cgstDetail: string,
        cgstRate: number
    ) {
        this.cgstId = cgstId;
        this.cgstDetail = cgstDetail;
        this.cgstRate = cgstRate;
    }
}