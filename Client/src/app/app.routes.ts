import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';

//defines the routers for the components

export const routes: Routes = [
    {path: '', component: HomeComponent}, 
    {path: 'shop', component: ShopComponent}, 
    {path: 'shop/:id', component: ProductDetailsComponent}, //Product detail components uses dynamic route parameter using colon and the id
    {path: '**', redirectTo: '', pathMatch: 'full'}, //wildcard routing, if the routing path does not match any of the above, it will redirect to the home page
];
