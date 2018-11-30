import { HomeComponent } from './Components/home/home.component';
import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrandComponent } from './Components/brand/brand.component';
import { CategoryComponent } from './Components/category/category.component';
import { SearchResultComponent } from './Components/search-result/search-result.component';
import { ProductDetailsComponent } from './Components/product-details/product-details.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path : 'brand', component : BrandComponent},
  { path : 'category', component : CategoryComponent},
  { path : 'search', component : SearchResultComponent},
  { path : 'product', component : ProductDetailsComponent}
];@

NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}