import { Component, OnInit, OnDestroy } from '@angular/core';
import { Product } from '../../Entities';
import { ProductService } from '../../Services/product.service';
import { Observable, of, from, Subject } from 'rxjs';
import { takeUntil, debounce } from 'rxjs/operators';
import { UIService } from '../../Services/ui.service';
import { SlideItem } from '../../Entities/slideItem';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  slideItems:SlideItem[];
  slidesLoaded: boolean = false;
  destroy$ = new Subject();
  constructor(private productService: ProductService,
  private uiService: UIService) { }

  ngOnInit() {
    this.uiService.getHomeSlider().subscribe(
      (data) => {
        this.fillHomeSlider(data);
        this.slidesLoaded = true;
      }
    );
  }

  ngOnDestroy(){
    this.destroy$.next(true);
  }

  fillHomeSlider(data: SlideItem[]){
    let i = 0;
    data.forEach(item =>{
      item.orderID = i++;
    });
    this.slideItems = data;
  }

}
