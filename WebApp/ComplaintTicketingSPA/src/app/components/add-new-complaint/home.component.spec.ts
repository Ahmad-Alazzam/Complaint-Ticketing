import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewComplaintComponent } from './add-new-complaint';

describe('AddNewComplaintsComponent', () => {
  let component: AddNewComplaintComponent;
  let fixture: ComponentFixture<AddNewComplaintComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddNewComplaintComponent]
    });
    fixture = TestBed.createComponent(AddNewComplaintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
