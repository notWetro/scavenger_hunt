import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScavengerHuntTileComponent } from './scavenger-hunt-tile.component';

describe('ScavengerHuntTileComponent', () => {
  let component: ScavengerHuntTileComponent;
  let fixture: ComponentFixture<ScavengerHuntTileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScavengerHuntTileComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ScavengerHuntTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
