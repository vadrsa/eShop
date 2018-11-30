import { Component, OnInit, OnDestroy } from '@angular/core';
import { Category } from '../../Entities';
import { CategoryService } from '../../Services/category.service';
import { takeUntil } from 'rxjs/operators';
import { Subject, Observable } from 'rxjs';
import Rxmq from 'rxmq';
import { CategorySelectedEventArgs } from '../../EventArgs';
import { forkJoin, of, interval } from 'rxjs';
@Component({
  selector: 'side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.css']
})
export class SideMenuComponent implements OnInit, OnDestroy {

  destroy$ = new Subject();

  categories: Category[];
  selectedCategoryID: number;
  loading = true;
  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService.getCategoryTree()
    .pipe(takeUntil(this.destroy$))
    .subscribe(
      (data) => {
        this.fillCategories(data);
        this.loading = false;
      }
    );
    Rxmq.channel('categories').observe('selected')
    .pipe(takeUntil(this.destroy$))    
    .subscribe(
      (data) => {
        this.selectedCategoryID = (<CategorySelectedEventArgs>data).selectedID;
        this.expandForCatgeoryID(this.selectedCategoryID)
      }
    )
    Rxmq.channel('categories').observe('unselected')
    .pipe(takeUntil(this.destroy$))
    .subscribe(
      (data) => {
        this.collapseAll();
        this.selectedCategoryID = 0;
      }
    )
    Rxmq.channel("product").subject("selected")
    .pipe(takeUntil(this.destroy$))
    .subscribe(
      (data) => {
        this.selectedCategoryID = <number>data;
        this.expandForCatgeoryID(this.selectedCategoryID)
      }
    )
    Rxmq.channel('product').observe('unselected')
    .pipe(takeUntil(this.destroy$))
    .subscribe(
      (data) => {
        this.collapseAll();
        this.selectedCategoryID = 0;
      }
    )
  }

  ngOnDestroy(){
    this.destroy$.next(true);
  }

  fillCategories(data: Category[]){
    this.categories = data;
    if(this.selectedCategoryID != undefined){
      this.expandForCatgeoryID(this.selectedCategoryID)
    }
  }

  rootClicked(category : Category){
    //category.isExpanded = !category.isExpanded;
  }

  expandForCatgeoryID(id: number) {
    if(!this.categories) return;
    let topLevel = this.findCategoryByID(this.categories, id);
    let res:Category = null;
    this.collapseAll();

    if(topLevel == null){
      this.categories.forEach(cat => {
        res = this.findCategoryByID(cat.children, id);
        if(res != null){
          cat.isExpanded = true;
        }
      });
    }
    else{
      this.collapseAll();

      topLevel.isExpanded = true;
    }
  }

  toggleExpand(category: Category){
    category.isExpanded = !category.isExpanded;
  }

  private collapseAll(){
    this.categories.forEach(el => {
      el.isExpanded = false;
    });
  }

  private findCategoryByID(list: Category[], id: number) : Category{
    let ret:Category = null;
    if(!list) return null;
    list.forEach(cat => {
      if(cat.id == id){
        ret = cat;
        return;
      }
    });
    return ret;
  }

}
