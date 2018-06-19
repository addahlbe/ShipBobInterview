import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import { webAPIService } from '../../../shared/helpers/webapi.service';
import User from '../User';

@Component
export default class UserEdit extends Vue {
    user: User = new User();
    alertMessage = '';
    alertTimer = 0;
    userId: number = 0;
    saveText: string = 'Create';
    mounted() {
        this.getUser(this.$route.params);
    }


    @Watch('$route.params')
    onPropertyChanged(value: any, oldValue: any) {
        this.getUser(value);
    }

    getUser(value: any) {
        var userId = value.userId;

        if (!isNaN(userId)) {
            this.userId = Number(userId);
            this.saveText = "Update";
            webAPIService.get('user/' + userId)
                .then((data: any) => {
                    this.user = data;
                });
        } else {
            this.userId = 0;
            this.saveText = "Create";
            this.user = new User();
        }
    }

    save(event: any) { 
        event.preventDefault();
        if (this.userId > 0) {
            webAPIService.put('user/' + this.userId, this.user).then(() => {
                this.alertMessage = 'User update successfully';
                this.alertTimer = 5;
            });
        } else {
            webAPIService.post('user', this.user)
                .then((id: any) => {
                    this.$router.push(id.toString());
                    this.alertMessage = 'User created successfully';
                    this.alertTimer = 5;
                }, reason => {
                    this.alertMessage = 'Error Saving the user';
                });
        }
    }

    _delete() {
        webAPIService.delete('user/' + this.userId)
            .then((id: any) => {
                this.$router.push('/user');
                this.alertMessage = 'User deleted successfully';
                this.alertTimer = 5;
            }, reason => {
            this.alertMessage = 'Error deleting the user';
        });
    }
    countDownChanged(dismissCountDown: number) {
        this.alertTimer = dismissCountDown
    }

}
