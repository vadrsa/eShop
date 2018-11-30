import { Component, OnInit, Input } from '@angular/core';
import { SlideItem } from '../../Entities/slideItem';
import { BehaviorSubject } from '../../../../node_modules/rxjs';

@Component({
  selector: 'slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.css']
})
export class SliderComponent implements OnInit {


  private _slideItems = new BehaviorSubject<SlideItem[]>([]);
  @Input()
  set slideItems(value) { this._slideItems.next(value); }
  get slideItems() { return this._slideItems.getValue(); }

  private _slidesLoaded = new BehaviorSubject<boolean>(false);
  @Input()
  set slidesLoaded(value) {
      if(value === true)
        this.loading = value; 
      this._slidesLoaded.next(value);
    }
  get slidesLoaded() { return this._slidesLoaded.getValue(); }

  loading: boolean = true;
  
  constructor() { }

  ngOnInit() {
    this._slideItems.subscribe(
      (x) => {
        this.initSlideItems();
      }
    );
  }

  initSlideItems(){
    if(this.slideItems && this.slideItems.length > 0)
      this.slideItems[0].class = 'active';
  }

  imageLoaded(slideItem:SlideItem){
    slideItem.loaded = true;
    this.checkAllLoaded();
  }

  imageFailed(item: SlideItem){
    let newItems = [];
    this.slideItems.forEach(e => {
      if(e.id !== item.id)
        newItems.push(e);
    });
    this.slideItems = newItems;
    this.checkAllLoaded();

  }

  checkAllLoaded(){
    let allLoaded = true;
    this.slideItems.forEach(i =>{
      if(i.loaded === false || i.loaded === undefined) allLoaded = false;
    });
    if(allLoaded && this.slidesLoaded){
      this.loading = false;
    }
  }
}
