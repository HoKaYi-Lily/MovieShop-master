import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FavoritesComponent } from './favorites/favorites.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { UserAppComponent } from './user-app.component';

const routes: Routes = [
{
// localhost:4200/user
path:'', component:UserAppComponent,

// localhost:4200/user/favorites
  children:[
    {path:'purchases', component:PurchasesComponent},
    {path:'favorites', component:FavoritesComponent}
  ]
}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
