import { Component, OnInit, OnDestroy } from '@angular/core';
import { BrandService } from '../../Services/brand.service';
import { Brand } from '../../Entities';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'brand-bar',
  templateUrl: './brand-bar.component.html',
  styleUrls: ['./brand-bar.component.css']
})
export class BrandBarComponent implements OnInit, OnDestroy {
  barItems: Brand[];
  itemsLoaded: Boolean = false;
  loading: Boolean = true;
  destroy$ = new Subject();
  constructor(private brandService : BrandService) { }

  ngOnInit() {
    this.brandService.getBar()
    .pipe(takeUntil(this.destroy$))
    .subscribe(
      (data) => {
        this.fillBarData(data);
        this.itemsLoaded = true;
      }
    );
  }

  ngOnDestroy(){
    this.destroy$.next(true);
  }

  fillBarData(data: Brand[]): void {
    this.barItems = data;

  }

  imageLoadedorFailed(brand: Brand){
    brand.loaded = true;
    this.checkAllLoaded();
  }

  checkAllLoaded(){
    let allLoaded = true;
    this.barItems.forEach(i =>{
      if(i.loaded === false || i.loaded === undefined) {
        allLoaded = false;
      }
    });
    if(allLoaded && this.itemsLoaded){
      this.loading = false;
    }
  }

}
