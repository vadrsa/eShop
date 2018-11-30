import { Component, OnInit, Input, OnDestroy, Output, AfterViewInit, AfterViewChecked } from '@angular/core';
import { Product, ProductOrderType } from '../../Entities';
import { ProductService } from '../../Services/product.service';
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { takeWhile, takeUntil } from 'rxjs/operators';
import { Config } from '../../Helpers/config';
import { EventEmitter } from '@angular/core';
import { LimitChangedEventArgs } from '../../EventArgs';
import { FilterChangedEventArgs } from '../../EventArgs/FilterChangedEventArgs';
import { Router, ActivatedRoute, ParamMap } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'product-display',
  templateUrl: './product-display.component.html',
  styleUrls: ['./product-display.component.css']
})
export class ProductDisplayComponent implements OnInit, OnDestroy {

  //#region -- Inputs/Outputs -- 
  private _title = new BehaviorSubject<string>("");
  filter$: any;
  @Input()
  set title(value) { this._title.next(value); }
  get title() { return this._title.getValue(); }

  private _page = new BehaviorSubject<number>(1);
  @Input()
  set page(value) { this._page.next(+value); }
  get page() { return this._page.getValue(); }

  loadingInternal:boolean = false;
  private _loading = new BehaviorSubject<boolean>(false);
  @Input()
  set loading(value) { 
    this._loading.next(value); 
    if(value === true)this.loadingInternal = value;
  }
  get loading() { return this._loading.getValue(); }

  get showingFrom(){
    return (this.page-1)*this.limit+1;
  }

  get showingTo(){
    if(this.page === this.pageCount) return this.productCount; 
    return this.page * this.limit;
  }

  get pageCount(){
    return Math.ceil(this.productCount/this.limit);
  }

  private _products = new BehaviorSubject<Product[]>([]);
  @Input()
  set products(value) { 
    if(!value || value.length == 0)
      this.productsLoaded();
    this._products.next(value); 
  }
  get products() { return this._products.getValue(); }

  private _productCount = new BehaviorSubject<number>(0);
  @Input()
  set productCount(value) { this._productCount.next(value); }
  get productCount() { return this._productCount.getValue(); }

  @Input() resetLimit$: Subject<Boolean>;
  @Output() filterChanged = new EventEmitter<FilterChangedEventArgs>();
//#endregion
  //#region -- Properties -- 
  defaultLimitIndex = Config.defaultLimitIndex;
  limits = Config.limits;
  limit: number;
  orderBy: string;
  destroy$ = new Subject();
  orderTypes: ProductOrderType[];
  selectedOrderType: string;
  isAscending: boolean;
  //#endregion

  
  constructor(private productService: ProductService,private route: Router) { }
  ngOnInit() {
    this.limit = this.limits[this.defaultLimitIndex];
    this.isAscending = true;
    this._page
    .pipe(takeUntil(this.destroy$))
    .subscribe(
      (x) => this.onFilterChanged()
    );
    if(this.resetLimit$){
      this.resetLimit$
      .pipe(takeUntil(this.destroy$))    
      .subscribe((b) => {
        if(b) this.resetLimit();
      });  
    }
    this.filter$ = this.productService.getOrderCriterias()
    .pipe(takeUntil(this.destroy$))
    .subscribe(
      (data) => {
        this.orderTypes = data;
        this.selectedOrderType = this.orderTypes[0].name;
        this.onFilterChanged();
      }
    );
    
  }

  ngOnDestroy(){
    this.destroy$.next(true);
  }

  resetLimit(){
    this.limit = this.limits[this.defaultLimitIndex];
    this.selectedOrderType = (this.orderTypes)? this.orderTypes[0].name : undefined;
    this.isAscending = true;
    this.onFilterChanged(true);
  }

  onLimitChange(limit:number){
    this.limit = limit;
    if(this.page != 1){
      this.changePage(1);
    }
    this.onFilterChanged();
  }

  onOrderByChanged(orderType: string){
    this.orderBy = orderType;
    if(this.page != 1){
      this.changePage(1);
      console.log(this.page);
    }
    this.onFilterChanged();
  }

  toggleOrderDirection(){
    this.isAscending = !this.isAscending;
    if(this.page != 1){
      this.changePage(1);
      console.log(this.page);
    }
    this.onFilterChanged();
  }

  lastEventArgs: FilterChangedEventArgs;
  onFilterChanged(force = false){
    let eventArgs = <FilterChangedEventArgs>{ascending: this.isAscending, limit: this.limit, start: (this.page-1)*this.limit, orderBy: this.orderBy};
    if(!force && this.lastEventArgs &&  eventArgs.ascending === this.lastEventArgs.ascending && eventArgs.limit === this.lastEventArgs.limit &&
      eventArgs.orderBy === this.lastEventArgs.orderBy && eventArgs.start === this.lastEventArgs.start
    ){
      return;
    }
    this.filterChanged.emit(eventArgs);
    this.lastEventArgs = eventArgs
  }

  productsLoaded(){
    if(this.loading === false){
      this.loadingInternal = false;
    }
  }

  changePage(pageNum: number){
    this.route.navigate([], {
      queryParamsHandling: 'merge',
      queryParams:{
        page:pageNum
      },
      preserveFragment: true
    });
  }
  get canGoNext(){
    return this.page+1 <= this.pageCount;
  }
  get canGoPrev(){
    return this.page-1 > 0;
  }
  nextPage(){
    if(!this.canGoNext) return;
    this.changePage((+this.page)+1);
  }
  prevPage(){
    if(!this.canGoPrev) return;
    this.changePage((+this.page)-1);
  }

  firstPage(){
    this.changePage(1);
  }

  lastPage(){
    this.changePage(this.pageCount);
  }

  getPageination(){
    var current = this.page,
        last = this.pageCount,
        delta = 3,
        left = current - delta,
        right = current + delta + 1,
        range = [],
        l;

    for (let i = 1; i <= last; i++) {
        if (i >= left && i < right) {
            range.push(i);
        }
    }
    return range;
  }
}
