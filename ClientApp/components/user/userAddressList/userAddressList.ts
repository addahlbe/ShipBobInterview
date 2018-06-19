import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { webAPIService } from '../../../shared/helpers/webapi.service';
import User, { UserAddress } from '../user';


@Component
export default class UserList extends Vue {
    userAddress: UserAddress[] = [];
    totalCount: number = 0;

    mounted() {
        webAPIService.get('user/' + this.$route.params.userId + '/address')
            .then((data:any) => {
                this.userAddress = data.userAddress;
                this.totalCount = data.totalCount;
            });
       
        }
}
