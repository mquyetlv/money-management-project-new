import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WrapIonicon } from './wrap-ionicon';

describe('WrapIonicon', () => {
  let component: WrapIonicon;
  let fixture: ComponentFixture<WrapIonicon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WrapIonicon]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WrapIonicon);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
