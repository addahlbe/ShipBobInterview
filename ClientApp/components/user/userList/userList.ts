import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { webAPIService } from '../../../shared/helpers/webapi.service';
import User from '../User';


@Component
export default class UserList extends Vue {
    users: User[] = [];
    totalCount: number = 0;
    searchTerm: string = '';

    mounted() {
        webAPIService.get('user')
            .then((data:any) => {
                this.users = data.users;
                this.totalCount = data.totalCount;
            });
       
    }

    search() {
        var params = new URLSearchParams();
        params.append('search', this.searchTerm);
        webAPIService.get('user', params)
            .then((data: any) => {
            this.users = data.users;
            this.totalCount = data.totalCount;
        });
    }
}
