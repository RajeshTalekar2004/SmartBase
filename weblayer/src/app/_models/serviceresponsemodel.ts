export class ServiceResponseModel {
    data: any;
    success: boolean;
    message: string;

    constructor(
        data: any,
        success: boolean,
        message: string,
    ) {
        this.data = data;
        this.success = success;
        this.message = message;
    }
}