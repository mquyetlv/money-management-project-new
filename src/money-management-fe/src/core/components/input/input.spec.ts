import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonInout } from './input';

describe('MonInout', () => {
  let component: MonInout;
  let fixture: ComponentFixture<MonInout>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MonInout]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonInout);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
