import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import { webAPIService } from '../../../shared/helpers/webapi.service';
import UserOrder from './../UserOrder';
import User from '../User';
import { Address } from '../../address/Address';

@Component
export default class UserOrderEdit extends Vue {
    alertMessage = '';
    alertTimer = 0;
    userOrder: UserOrder = new UserOrder();
    userId: number = 0;
    user: User = new User();
    userOrderId: number = 0;
    saveText: string = 'Create';
    mounted() {
        this.getUserOrder(this.$route.params);
    }

    @Watch('$route.params')
    onPropertyChanged(value: any, oldValue: any) {
        this.getUserOrder(value);
    }

    getUserOrder(value: any) {
        var userId = value.userId;
        var userOrderId = value.orderId;
        if (!isNaN(userId)) {
            this.userId = Number(userId);
            webAPIService.get('user/' + userId)
            .then((data: any) => {
                this.user = data;
            });

            if (!isNaN(userOrderId)) {
                this.userOrderId = Number(userOrderId);
                this.saveText = "Update";
                webAPIService.get('order/' + userOrderId)
                    .then((data: any) => {
                        this.userOrder = data;
                    });
            } else {
                this.userOrderId = 0;
                this.saveText = "Create";
                this.userOrder = new UserOrder();
                this.userOrder.address = new Address();
            }
        }
    }
    save(event: any) {
        event.preventDefault();
        this.userOrder.userId = this.userId;
        if (this.userOrderId > 0) {
            webAPIService.put('order/' + this.userOrderId, this.userOrder)
                .then(() => {
                    this.alertMessage = 'User Order update successfully';
                    this.alertTimer = 5;
                });
        } else {
            webAPIService.post('order', this.userOrder)
                .then((id: any) => {
                    this.$router.push(id.toString());
                    this.alertMessage = 'User order created successfully';
                    this.alertTimer = 5;
                });
        }
    }

    countDownChanged(dismissCountDown: number) {
        this.alertTimer = dismissCountDown
    }
}
