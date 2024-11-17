import { Component } from '@angular/core';
import * as THREE from 'three';

@Component({
  selector: 'app-corridor',
  templateUrl: './corridor.component.html',
  styleUrls: ['./corridor.component.css']
})

export class CorridorComponent {
  public corridorGroup!: THREE.Group;
  private floorTexture: string = 'assets/floor.jpg';
  private width!: number;
  private wallThickness: number = 0.1;

  public createCorridor(width: number): void {
    this.width = width;
    this.corridorGroup = new THREE.Group();
    this.createCorridorFloor();
  }

  private createCorridorFloor(): void {
   // const texture = new THREE.TextureLoader().load(this.floorTexture);
    //texture.wrapS = THREE.RepeatWrapping;
    //texture.wrapT = THREE.RepeatWrapping;
    //texture.repeat.set(1, 1);

    const floorMaterial = new THREE.MeshStandardMaterial({ color: 0x00ff00, side: THREE.DoubleSide });
    const floorGeometry = new THREE.BoxGeometry(this.width, this.wallThickness, this.width);
    const floor = new THREE.Mesh(floorGeometry, floorMaterial);
    floor.position.set(0,0,0);
    floor.receiveShadow = true;
    floor.castShadow = false;
    floor.updateMatrix();

    this.corridorGroup.add(floor);
  }
}
