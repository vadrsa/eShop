import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductService } from '../../Services/product.service';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';
import { map, takeUntil, first } from '../../../../node_modules/rxjs/operators';
import { Subject } from '../../../../node_modules/rxjs';
import { Product } from '../../Entities';
import Rxmq from '../../../../node_modules/rxmq';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit, OnDestroy {

  constructor(private productService: ProductService,
    private route: ActivatedRoute) { }

  productID$ = this.route.queryParams.pipe(map(r => r["id"]));
  productID:number;
  product: Product;
  prdoctLoaded:boolean = false;
  loading: boolean = true;
  destroy$ = new Subject();
  ngOnInit() {
    this.productID$
    .pipe(takeUntil(this.destroy$ ))
    .subscribe(data => {
      if(this.productID != data){
        this.productID = data;
        this.getProductDetails(this.productID);
      }
    });
  }

  ngOnDestroy(){
    this.destroy$.next(true);
    Rxmq.channel("product").subject("unselected").next();

  }

  getProductDetails(id:number){
    this.loading = true;
    this.productService.getByID(id)
    .pipe(first())
    .subscribe(data => {
      this.product = data;
      if(this.product && this.product.category) Rxmq.channel("product").subject("selected").next(this.product.category.id);
      this.prdoctLoaded = true;
      if(data == null)
        this.loading = false;
    },
    e => {
      this.product = null;
      this.loading = false;
    }
    );
  }

  imageLoaded(){
    if(this.prdoctLoaded === true)
      this.loading = false;
  }

  imageFailed(){
    if(this.prdoctLoaded === true)
      this.loading = false;
  }
}
