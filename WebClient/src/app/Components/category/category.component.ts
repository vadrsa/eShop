import { Component, OnInit, OnDestroy, AfterViewInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { Category, Product } from '../../Entities';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from '../../Services/category.service';
import { Observable, Subscription, Subject } from 'rxjs';
import Rxmq from 'rxmq';
import { CategorySelectedEventArgs, LimitChangedEventArgs } from '../../EventArgs';
import { AlertService } from '../../Services/alert.service';
import { Config } from '../../Helpers/config';
import { takeWhile, takeUntil } from 'rxjs/operators';
import {  ProductDisplayComponent } from '../product-display/product-display.component';
import { FilterChangedEventArgs } from '../../EventArgs/FilterChangedEventArgs';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit, OnDestroy {

  productCount:number;
  category:Category;
  products:Product[];
  routeSub: Subscription;
  page: number;
  destroy$ = new Subject();
  resetLimit$= new Subject<boolean>();
  categoryID: number;
  categoryService: CategoryService;
  limit: number;
  start:number;
  orderBy:string;
  isAscending: boolean;
  productGetter$: Subscription;
  loading: boolean = true;
  constructor(private route: ActivatedRoute,
    _categoryService: CategoryService) {
    this.categoryService = _categoryService;
  }
  
  ngOnInit() {
    this.routeSub = this.route
      .queryParams
      .pipe(takeUntil(this.destroy$))
      .subscribe(params => {
        const id = params['id'];
        let _page = params['page'];
        if(id != undefined){
          if(this.categoryID != id){
            this.categoryID = id;
            this.resetLimit$.next(true);
            this.category = null;
            let sub = this.categoryService.getProductsCountByID(this.categoryID)
            .subscribe(
              (data) => {
                this.productCount = data;
                sub.unsubscribe();
              }
            );
          }
          let sub = this.categoryService.getByID(this.categoryID)
          .subscribe(
            (data) => {
              this.category = data;
              sub.unsubscribe();
            }
          );
            this.page = _page? _page: 1;
          Rxmq.channel('categories').subject('selected').next(<CategorySelectedEventArgs>{
            selectedID: id
          });
        }
      });
  }

  ngOnDestroy(){
    Rxmq.channel('categories').subject('unselected').next();
    this.destroy$.next(true);
  }

  filterChanged(e: FilterChangedEventArgs){
    this.limit = e.limit;
    this.start = e.start
    this.orderBy = e.orderBy;
    this.isAscending = e.ascending;
    this.getProducts(this.categoryID);
  }

  getProducts(categoryID:number){
    if(this.productGetter$)
      this.productGetter$.unsubscribe();
    setTimeout(() => this.loading = true, 0)
    let obs = this.categoryService.getProductsByID(categoryID, this.limit, this.start, this.orderBy, this.isAscending);
    this.productGetter$ = obs.subscribe(
      (data) =>{
        this.products = data;
        this.productGetter$.unsubscribe();
        this.loading = false;
      },
      (e) => this.loading = false
    );
  }
}