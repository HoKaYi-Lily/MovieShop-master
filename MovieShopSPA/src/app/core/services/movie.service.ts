import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { MovieCard } from 'src/app/shared/models/movieCard';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class MovieService {
//should have all the methods that deals with movies, getById, getTopRevenue, 
//GetTopRated
//HttpClient to make AJAX request
//XMLHttpRequest => 
//private readonly, inject, but in Angular this is enough
  constructor(private http: HttpClient) { }

  getTopRevenueMovies() : Observable<MovieCard[]>
  {
    //https://localhost:44387/api/Movies/toprated
    //we need to create a model based on JSON data
    //Observables are lazy, only when you subscribe to a observable you will get the data...
    //Youtube => Chanels
    //Xbox => videos
    //notification when Xbox posts a new video
    //subscribe to it
    //newspaper, 
    //HttpClient in Angular => Observables 
    //localhost:4200() =>AJAX call to http://localhost:///
    //yahoo.com =>google.com different domain

   return this.http.get(`${environment.apiUrl}`+'movies/toprated')
    .pipe(
        map( resp => resp as MovieCard[])
    )
    //movies.where(m => m.budgeted>10000).select(m => new MovieCard { id = m.id, title = m.title}).Tolist()

  }
}



