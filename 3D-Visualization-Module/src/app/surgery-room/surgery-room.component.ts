import { Component } from '@angular/core';
import * as THREE from 'three';
import { CSG } from 'three-csg-ts';

@Component({
  selector: 'app-surgery-room',
  templateUrl: './surgery-room.component.html',
  styleUrls: ['./surgery-room.component.css']
})
export class SurgeryRoomComponent {

  public roomGroup!: THREE.Group;
  public wallTexture: string = 'assets/wall.jpg';
  public floorTexture: string = 'assets/floor.jpg';
  public doorTexture: string = 'assets/slidingDoors.jpg';

  private wallThickness: number = 0.1;
  private roomWidth: number = 6;
  private roomHeight: number = 3;
  private roomDepth: number = 6;

  constructor() {
    this.createRoom();
  }

  public createRoom(): void {
    this.roomGroup = new THREE.Group();
    this.createRoomWalls();
    this.createRoomFloor();
  }

  private createRoomWalls(): void {
    const texture = new THREE.TextureLoader().load(this.wallTexture);
    texture.wrapS = THREE.RepeatWrapping;
    texture.wrapT = THREE.RepeatWrapping;
    texture.repeat.set(1, 1);

    const wallMaterial = new THREE.MeshBasicMaterial({ map: texture });
    const whiteMaterial = new THREE.MeshBasicMaterial({ color: 0xffffff });

    const frontBackWallGeometry = new THREE.BoxGeometry(this.roomWidth, this.roomHeight, this.wallThickness);
    const sideWallGeometry = new THREE.BoxGeometry(this.wallThickness, this.roomHeight, this.roomDepth);
    const doorGeometry = new THREE.BoxGeometry(2.5, 2.5, this.wallThickness);

    // Create front wall geometry
    const frontWall = new THREE.Mesh(frontBackWallGeometry, [wallMaterial, wallMaterial, whiteMaterial, whiteMaterial, wallMaterial, wallMaterial]);
    frontWall.position.set(0, 0, this.roomDepth / 2 - this.wallThickness / 2);
    frontWall.updateMatrix();

    const door = new THREE.Mesh(doorGeometry, new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load(this.doorTexture) }));
    door.position.set(0, -0.25, this.roomDepth / 2 - this.wallThickness / 2); // At the front wall
    door.updateMatrix();

    const frontWallCSG = CSG.fromMesh(frontWall);
    const doorCSG = CSG.fromMesh(door);
    const resultCSG = frontWallCSG.subtract(doorCSG);
    const resultMesh = CSG.toMesh(resultCSG, frontWall.matrix);

    resultMesh.material = wallMaterial;

    this.roomGroup.add(resultMesh);

    const backWall = new THREE.Mesh(frontBackWallGeometry, [wallMaterial, wallMaterial, whiteMaterial, whiteMaterial, wallMaterial, wallMaterial]);
    backWall.position.set(0, 0, -this.roomDepth / 2 + this.wallThickness / 2);
    this.roomGroup.add(backWall);

    const leftWall = new THREE.Mesh(sideWallGeometry, [wallMaterial, wallMaterial, whiteMaterial, whiteMaterial, wallMaterial, wallMaterial]);
    leftWall.position.set(-this.roomWidth / 2 + this.wallThickness / 2, 0, 0);
    this.roomGroup.add(leftWall);

    const rightWall = new THREE.Mesh(sideWallGeometry, [wallMaterial, wallMaterial, whiteMaterial, whiteMaterial, wallMaterial, wallMaterial]);
    rightWall.position.set(this.roomWidth / 2 - this.wallThickness / 2, 0, 0);
    this.roomGroup.add(rightWall);

    this.roomGroup.add(door);
  }

  private createRoomFloor(): void {
    const texture = new THREE.TextureLoader().load(this.floorTexture);
    texture.wrapS = THREE.RepeatWrapping;
    texture.wrapT = THREE.RepeatWrapping;
    texture.repeat.set(10, 10);

    const floorMaterial = new THREE.MeshBasicMaterial({ color: 0xf0f0f0 });
    const floorGeometry = new THREE.BoxGeometry(this.roomWidth, this.wallThickness, this.roomDepth);
    const floor = new THREE.Mesh(floorGeometry, floorMaterial);
    floor.position.set(0, -this.roomHeight / 2, 0);
    this.roomGroup.add(floor);
  }
}
