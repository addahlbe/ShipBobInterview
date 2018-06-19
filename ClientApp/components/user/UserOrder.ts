import User from "../user/User";
import { Address } from './../address/Address';

export default class UserOrder {
    public trackingId?: number;
    public userOrderId?: number;
    public userId?: number;
    public addressId?: number;
    public address: Address;

    public UserOrder() {
        this.address = new Address();
    }
}
