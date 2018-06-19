import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import { webAPIService } from '../../../shared/helpers/webapi.service';
import Order from '../../user/UserOrder';

@Component
export default class UserList extends Vue {
    userOrders: Order[] = [];
    userId: number = 0;

    mounted() {
        this.getUserOrder(this.$route.params);
    }

    @Watch('$route.params')
    onPropertyChanged(value: any, oldValue: any) {
        this.getUserOrder(value);
    }

    getUserOrder(route: any) {
        var userId = route.userId;

        if (!isNaN(userId)) {
            this.userId = Number(userId);
            webAPIService.get('order/user/' + userId)
                .then((data: any) => {
                    this.userOrders = data;
                });
        } 
    }
    
}
