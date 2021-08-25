import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { MovieCard } from 'src/app/shared/models/movieCard';


@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css']
})
export class PurchasesComponent implements OnInit {
 movies!: MovieCard[];
  constructor(private userService: UserService) { }

  ngOnInit(): void {

    this.userService.getUserPurchases(32882).subscribe((p) =>{
      this.movies = p;
      console.log('inside home component init method');
      console.table(this.movies);
    });
  }

}
