import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarMasterComponent } from './calendar-master.component';

describe('CalendarMasterComponent', () => {
  let component: CalendarMasterComponent;
  let fixture: ComponentFixture<CalendarMasterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarMasterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
