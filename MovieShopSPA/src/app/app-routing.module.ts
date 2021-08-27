import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { FavoritesComponent } from './user/favorites/favorites.component';
import { PurchasesComponent } from './user/purchases/purchases.component';

const routes: Routes = [

  {path:"", component: HomeComponent},
  {path:"account/login", component:LoginComponent},
  {path:"account/register", component:RegisterComponent},
  {path:"movies/:id",component:MovieDetailsComponent},
  {path:"user/favorites", component:FavoritesComponent},
  {path:"user/purchases", component:PurchasesComponent},
  {path:'user', loadChildren:()=>import('./user/user.module').then(mod =>mod.UserModule), canActivate: [AuthGuard]}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
