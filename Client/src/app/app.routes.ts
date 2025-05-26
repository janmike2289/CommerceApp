import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { TestErrorComponent } from './features/test-error/test-error.component';
import { ServerErrorComponent } from './shared/components/server-error/server-error.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';

//defines the routers for the components

export const routes: Routes = [
    {path: '', component: HomeComponent}, 
    {path: 'shop', component: ShopComponent}, 
    {path: 'shop/:id', component: ProductDetailsComponent}, //Product detail components uses dynamic route parameter using colon and the id
    {path: 'test-error', component: TestErrorComponent}, // component for testing error handling
    {path: 'server-error', component: ServerErrorComponent}, // component for handling any server errors encountered
    {path: 'not-found', component: NotFoundComponent}, // component for handling any not found issues encountered
    {path: '**', redirectTo: 'not-found', pathMatch: 'full'}, //wildcard routing, if the routing path does not match any of the above, it will redirect to the home page
];
