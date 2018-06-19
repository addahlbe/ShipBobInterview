
export default class User {
    public userId?: number;
    public firstName?: string;
    public lastName?: string;

    public User() {
        this.firstName = '';
        this.lastName = '';
    }
}

export class UserAddress {
    public UserAddressId?: number;
    public UserId?: number;
    public AddressId?: number;
}