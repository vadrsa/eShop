import { Component, OnInit, Input, OnDestroy, Output, EventEmitter } from '@angular/core';
import { Product } from '../../Entities';
import { Observable, BehaviorSubject, from } from 'rxjs';
import { takeWhile } from 'rxjs/operators';
import { takeUntil } from 'rxjs/operators';
@Component({
  selector: 'product-grid',
  templateUrl: './product-grid.component.html',
  styleUrls: ['./product-grid.component.css']
})
export class ProductGridComponent implements OnInit , OnDestroy{
  private _products = new BehaviorSubject<Product[]>([]);
  @Input()
  set products(value) { this._products.next(value); }
  get products() { return this._products.getValue(); }

  private _isGridStyle = new BehaviorSubject<boolean>(true);
  @Input()
  set isGridStyle(value) { this._isGridStyle.next(value); }
  get isGridStyle() { return this._isGridStyle.getValue(); }

  dproducts:Product[][];
  constructor() { }

  @Output() loaded = new EventEmitter<boolean>();

  ngOnInit() {
    this._products
      .subscribe(x => {
        if(this.products == undefined || this.products.length == 0)
          this.loaded.emit(true);
        if(this.products != undefined){
          this.fillProducts(this.products);
        }
      });
  }

  ngOnDestroy(){
    this._products.unsubscribe();
  }

  fillProducts(data: Product[]){
    var prods: Product[][] = [];
    var j = 0;
    for(var i = 0; i*3 + j < data.length; i++){
      prods[i] = [];
      for(j = 0; j < 3 ; j++){
        prods[i][j] = data[i*3+j];
      }
      j = 0;
    }
    this.dproducts = prods;
  }

  onImageLoaded(prod: Product){
    prod.loaded = true;
    this.checkForLoaded();
  }

  checkForLoaded(){
    if(this.products === undefined || this.products.length == 0){
      this.loaded.emit(true);
      return;
    }
    let isAllLoaded = true;
    this.products.forEach(p=> {
      if(!p.loaded) isAllLoaded = false;
    });
    if(isAllLoaded){
      this.loaded.emit(true);
    }
  }

}
