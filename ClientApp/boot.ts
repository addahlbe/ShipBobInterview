import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
import BootstrapVue from 'bootstrap-vue'

Vue.use(BootstrapVue);
Vue.use(VueRouter);

const routes = [
    { path: '/', component: require('./components/home/home.vue.html').default },
    { path: '/user', component: require('./components/user/userList/userList.vue.html').default },
    { path: '/user/:userId', component: require('./components/user/userEdit/userEdit.vue.html').default },
    { path: '/user/:userId/order', component: require('./components/user/userOrderList/userOrderList.vue.html').default },
    { path: '/user/:userId/order/:orderId', component: require('./components/user/userOrderEdit/userOrderEdit.vue.html').default },

    { path: '/order', component: require('./components/order/orderList/orderList.vue.html').default },
    //{ path: '/order/:id', component: require('./components/user/useredit/useredit.vue.html').default },

];

new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html').default)
});
