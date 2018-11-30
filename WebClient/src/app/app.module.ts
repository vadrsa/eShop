import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BrandBarComponent } from './Components/brand-bar/brand-bar.component';
import { SearchBarComponent } from './Components/search-bar/search-bar.component';
import { FormsModule } from '@angular/forms';
import { SideMenuComponent } from './Components/side-menu/side-menu.component';
import { ProductComponent } from './Components/product/product.component';
import { ProductGridComponent } from './Components/product-grid/product-grid.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './Components/home/home.component';
import { ProductDisplayComponent } from './Components/product-display/product-display.component';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';
import { LoadingBarRouterModule } from '@ngx-loading-bar/router';
import { BrandComponent } from './Components/brand/brand.component';
import { CategoryComponent } from './Components/category/category.component';
import { ProductDetailsComponent } from './Components/product-details/product-details.component';
import { AlertComponent } from './Components/alert/alert.component';
import { SliderComponent } from './Components/slider/slider.component';
import { AlertService } from './Services/alert.service';
import { ApiService } from './Services/api.service';
import { SearchResultComponent } from './Components/search-result/search-result.component';
import { NotFoundComponent } from './Components/not-found/not-found.component';
@NgModule({
  declarations: [
    AppComponent,
    BrandBarComponent,
    SearchBarComponent,
    SideMenuComponent,
    ProductComponent,
    ProductGridComponent,
    HomeComponent,
    ProductDisplayComponent,
    BrandComponent,
    CategoryComponent,
    ProductDetailsComponent,
    AlertComponent,
    SliderComponent,
    SearchResultComponent,
    NotFoundComponent
  ],
  imports: [
    FormsModule,
    LoadingBarRouterModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule
  ],
  providers: [AlertService, ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
