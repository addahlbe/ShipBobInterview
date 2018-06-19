import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { webAPIService } from '../../../shared/helpers/webapi.service';
import Order from '../../user/UserOrder';

@Component
export default class OrderList extends Vue {
    orders: Order[] = [];
    totalCount: number = 0;
    searchTerm: string = '';

    mounted() {
        webAPIService.get('order')
            .then((data:any) => {
                this.orders = data.orders;
                this.totalCount = data.totalCount;
            });
       
    }

    search() {
        var params = new URLSearchParams();
        params.append('search', this.searchTerm);
        webAPIService.get('order', params)
            .then((data: any) => {
                this.orders = data.orders;
                this.totalCount = data.totalCount;
            });
    }
}
