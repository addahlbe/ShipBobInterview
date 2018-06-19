export class Address {
    public addressId?: number;
    public addressLine1?: string;
    public addressLine2?: string;
    public city?: string;
    public stateCode?: string;
    public zipCode?: string;

    public Address() {
        this.addressLine1 = '';
        this.addressLine2 = '';
        this.city = '';
        this.stateCode = '';
        this.zipCode = '';
    }
}