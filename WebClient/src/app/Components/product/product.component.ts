import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Product } from '../../Entities';
import { BehaviorSubject } from '../../../../node_modules/rxjs';

@Component({
  selector: 'product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  @Input() product: Product;

  @Output() loaded = new EventEmitter<boolean>();

  constructor() { }

  ngOnInit() {
  }

  imageLoadedOrFailed(id){
    
    if(id === this.product.id)
      this.loaded.emit(true);
  }

}
