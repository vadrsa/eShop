import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BrandService } from '../../Services/brand.service';
import { Brand, Product } from '../../Entities';
import { Observable, Subscription, Subject } from 'rxjs';
import { takeUntil, catchError, first } from 'rxjs/operators';
import { AlertService } from '../../Services/alert.service';
import Rxmq from 'rxmq';
import { LimitChangedEventArgs } from '../../EventArgs';
import { FilterChangedEventArgs } from '../../EventArgs/FilterChangedEventArgs';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.css']
})
export class BrandComponent implements OnInit, OnDestroy {
  brand:Brand;
  products:Product[];
  productCount: number;
  routeSub: Subscription;
  page: number;
  destroy$ = new Subject();
  resetLimit$= new Subject<boolean>();
  brandID: number;
  limit: number;
  start:number;
  orderBy:string;
  isAscending: boolean;
  productGetter$: Subscription;
  loading: boolean = true;
  brandGetter$: Subscription;
  
  constructor(private route: ActivatedRoute,
    private brandService: BrandService) {
  }

  ngOnInit() {
      this.routeSub = this.route
      .queryParams
      .pipe(takeUntil(this.destroy$))
      .subscribe(params => {
        const id = params['id'];
        let _page = params['page'];
        if(id != undefined){
          if(this.brandID != id){
            this.brandID = id;
            this.resetLimit$.next(true);
            this.brand = null;
            let sub = this.brandService.getProductsCountByID(this.brandID)
            .subscribe(
              (data) => {
                this.productCount = data;
                sub.unsubscribe();
              }
            );
          }
          if(this.brandGetter$) this.brandGetter$.unsubscribe();
          this.brandGetter$ = this.brandService.getByID(this.brandID)
          .pipe(first())
          .subscribe(
            (data) => {
              this.brand = data;
            }
          );
          this.page = _page? _page: 1;
        }
      });
  }

  ngOnDestroy(){
    this.routeSub.unsubscribe();
    this.destroy$.next(true);
  }

  
  filterChanged(e: FilterChangedEventArgs){
    this.limit = e.limit;
    this.start = e.start
    this.orderBy = e.orderBy;
    this.isAscending = e.ascending;
    this.getProducts(this.brandID);
  }

  getProducts(brandID:number){
    if(this.productGetter$)
      this.productGetter$.unsubscribe();
    setTimeout(() => this.loading = true, 0);
    let obs = this.brandService.getProductsByID(brandID, this.limit, this.start, this.orderBy, this.isAscending);

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
