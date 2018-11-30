import { Component, OnInit } from '@angular/core';
import { Subject, Subscription } from '../../../../node_modules/rxjs';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';
import { ProductService } from '../../Services/product.service';
import { takeUntil, map, first } from '../../../../node_modules/rxjs/operators';
import { Product } from '../../Entities';
import { FilterChangedEventArgs } from '../../EventArgs/FilterChangedEventArgs';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.css']
})
export class SearchResultComponent implements OnInit {
  resetLimit$ = new Subject<boolean>();
  constructor(private route: ActivatedRoute,
    private productService: ProductService) { }
  destroy$ = new Subject();
  products : Product[];
  categoryID$ = this.route.queryParams.pipe(map(p => p['categoryID']));
  categoryID: number;
  queryString$ = this.route.queryParams.pipe(map(p => p['query']));
  queryString: string;
  page$ = this.route.queryParams.pipe(map(p => p['page']));
  page:number;
  limit: number;
  start:number;
  orderBy:string;
  isAscending: boolean;
  productGetter$: Subscription;
  loading: boolean = true;
  productCount:number;
  title:string;
  prodCountGetter$: Subscription;
  ngOnInit() {
    this.title = "Search results";
    this.queryString$
    .pipe(takeUntil(this.destroy$))
    .subscribe((data) => {
      if(this.queryString != data){
        this.queryString = data;
        this.title = "Search results: " + data;
        this.resetLimit$.next(true);
        this.products = null;
        this.prodCountGetter$ = this.productService.searchCount(this.queryString, 0)
          .pipe(first())
          .subscribe(data => {
            this.productCount = data;
          });
      }
    });
    this.categoryID$
    .pipe(takeUntil(this.destroy$))
    .subscribe(data => {
      if(this.categoryID != data){
        this.categoryID = data;
        this.resetLimit$.next(true);
        if(this.prodCountGetter$) this.prodCountGetter$.unsubscribe();
        if(this.queryString){
          this.prodCountGetter$ = this.productService.searchCount(this.queryString, this.categoryID)
          .pipe(first())
          .subscribe(data => {
            this.productCount = data;
          });
        }
      }
    });
    this.page$
    .pipe(takeUntil(this.destroy$))
    .subscribe((data) => {
      this.page = data? data: 1;
    });
}

ngOnDestroy(){
  this.destroy$.next(true);
}


filterChanged(e: FilterChangedEventArgs){
  this.limit = e.limit;
  this.start = e.start
  this.orderBy = e.orderBy;
  this.isAscending = e.ascending;
  this.getProducts(this.queryString);
}

getProducts(query:string){
  if(this.productGetter$)
    this.productGetter$.unsubscribe();
  setTimeout(() => this.loading = true, 0);
  let obs = this.productService.serach(query, this.categoryID, this.limit, this.start, this.orderBy, this.isAscending);

  this.productGetter$ = obs
  .subscribe(
    (data) =>{
      this.loading = false;
      this.products = <Product[]>data;
      this.productGetter$.unsubscribe();
    },
    (e) => console.log(e)
  );
}
}
