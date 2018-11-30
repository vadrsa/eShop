import { Component, OnInit, OnDestroy } from '@angular/core';
import { CategoryService } from '../../Services/category.service';
import { Category } from '../../Entities';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Router } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit, OnDestroy {

  categories:Category[];
  selectedCategory:Category;
  categoriesLoaded:Boolean = false;
  destroy$ = new Subject();
  queryString: string = "";
  constructor(private categoryService: CategoryService, private router:Router ) { }

  ngOnInit() {
    this.categoryService.getCategoryTree()
    .pipe(takeUntil(this.destroy$))
    .subscribe(
      (data) => {
        this.loadCategories(data);
        this.categoriesLoaded = true;
      }
    );
  }

  ngOnDestroy(){
    this.destroy$.next(true);
  }

  loadCategories(data : Category[]){
    this.categories = data;
    this.selectCategory(null);
  }

  selectCategory(category: Category){
    if(category == null)
      this.selectedCategory = <Category>{id: 0, name: "All Categories", children: null};
    else
      this.selectedCategory = category;
  }

  search(){
    if(this.queryString){
      this.router.navigateByUrl('search?query='+this.queryString+"&categoryID="+this.selectedCategory.id);
    }
  }
}
