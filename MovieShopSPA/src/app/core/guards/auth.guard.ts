import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private authService:AuthService, private router:Router){

  }
  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    
      //user/purchases
      return this.checkLogin(state.url);

  }
  
  checkLogin(url:string):Observable<boolean>{
    return this.authService.isLoggedIn.pipe(map(
      resp=>{
        if(resp){
          return true;
        }
        else{
          //before return false, redirect to login page
          this.router.navigate(['/account/login']);
          return false;
        }
      }
    ))
  }
}
