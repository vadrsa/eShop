import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandBarComponent } from './brand-bar.component';

describe('BrandBarComponent', () => {
  let component: BrandBarComponent;
  let fixture: ComponentFixture<BrandBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BrandBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BrandBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
