import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  getUserFavorite(id:number): Observable<MovieCard[]> {
    return this.http
      .get(`${environment.apiUrl}` + 'user/'+ id +'/favorites')
      .pipe(map((resp) => resp as MovieCard[]));
  }
  // getUserFavorite(): Observable<MovieCard[]> {
  //   return this.http
  //     .get(`${environment.apiUrl}` + 'user/{id:int}/favorites')
  //     .pipe(map((resp) => resp as MovieCard[]));
  // }

  getUserPurchases(id:number): Observable<MovieCard[]> {
    return this.http
      .get(`${environment.apiUrl}` + 'user/'+id+'/purchases')
      .pipe(map((resp) => resp as MovieCard[]));
  }
}
