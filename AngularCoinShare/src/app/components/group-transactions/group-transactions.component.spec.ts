import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupTransactionsComponent } from './group-transactions.component';

describe('GroupTransactionsComponent', () => {
  let component: GroupTransactionsComponent;
  let fixture: ComponentFixture<GroupTransactionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupTransactionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupTransactionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
