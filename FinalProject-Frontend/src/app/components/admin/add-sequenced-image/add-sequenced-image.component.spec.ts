import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSequencedImageComponent } from './add-sequenced-image.component';

describe('AddSequencedImageComponent', () => {
  let component: AddSequencedImageComponent;
  let fixture: ComponentFixture<AddSequencedImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddSequencedImageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddSequencedImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
