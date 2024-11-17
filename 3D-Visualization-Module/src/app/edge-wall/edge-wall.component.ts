import { Component } from '@angular/core';
import * as THREE from 'three';
import { CSG } from 'three-csg-ts';

@Component({
  selector: 'app-edge-wall',
  templateUrl: './edge-wall.component.html',
  styleUrls: ['./edge-wall.component.css']
})
export class EdgeWallComponent {

  public edgeWallGroup!: THREE.Group;
  private wallTexture: string = 'assets/wall.jpg';
  private doorTexture: string = 'assets/slidingDoors.jpg';

  private wallThickness: number = 0.1;
  private wallWidth!: number;
  private wallHeight!: number;
  private wallDepth!: number;

  public createEdgeWalls(walls: {
    left: boolean;
    back: boolean;
    front: boolean;
    right: boolean
  }, number: number, height: number): void {

    this.wallWidth = number;
    this.wallDepth = number;
    this.wallHeight = height;
    this.edgeWallGroup = new THREE.Group();
    this.createWalls(walls);
  }

  private createWalls(walls: {
    left: boolean;
    back: boolean;
    front: boolean;
    right: boolean;
  }): void {
    for (let wall in walls) {
      if (walls[wall as keyof typeof walls]) {
        const newWall = this.createWall();
        switch (wall) {
          case 'left':
            newWall.position.set(-this.wallWidth / 2, 0, 0);
            newWall.rotation.y = Math.PI / 2;
            break;
          case 'right':
            newWall.position.set(this.wallWidth / 2, 0, 0);
            newWall.rotation.y = -Math.PI / 2;
            break;
          case 'back':
            newWall.position.set(0, 0, -this.wallDepth / 2);
            newWall.rotation.y = Math.PI;
            break;
          case 'front':
            newWall.position.set(0, 0, this.wallDepth / 2);
            newWall.rotation.y = 0;
            break;
        }
        this.edgeWallGroup.add(newWall);
      }
    }
  }

  private createWall() {
    const wallTexture = new THREE.TextureLoader().load(this.wallTexture);
    wallTexture.wrapS = THREE.RepeatWrapping;
    wallTexture.wrapT = THREE.RepeatWrapping;
    wallTexture.repeat.set(1, 1);

    const wallMaterial = new THREE.MeshStandardMaterial({ map: wallTexture, side: THREE.DoubleSide });
    const geometry = new THREE.BoxGeometry(this.wallWidth, this.wallHeight, this.wallThickness);
    const wall = new THREE.Mesh(geometry, wallMaterial);
    wall.receiveShadow = true;
    wall.castShadow = false;

    // Create door
    const doorTexture = new THREE.TextureLoader().load(this.doorTexture);
    const door = new THREE.Mesh(new THREE.BoxGeometry(2.5, 2.5, this.wallThickness), new THREE.MeshStandardMaterial({ map: doorTexture, side: THREE.DoubleSide }));

    // Position the door
    door.position.set(0, 0, 0);

    // Set door shadow properties
    door.castShadow = false;
    door.receiveShadow = true;

    // Perform CSG subtraction: subtract door from the wall
    const wallCSG = CSG.fromMesh(wall);
    const doorCSG = CSG.fromMesh(door);
    const resultCSG = wallCSG.subtract(doorCSG);
    const resultMesh = CSG.toMesh(resultCSG, wall.matrix);

    // Apply material and shadow properties to the result mesh
    resultMesh.material = [wallMaterial, wallMaterial, wallMaterial, wallMaterial, wallMaterial, wallMaterial];
    resultMesh.castShadow = false;
    resultMesh.receiveShadow = true;

    // Add the door as a child of the resultMesh
    resultMesh.add(door);

    // Add the resulting mesh (with door as a child) to the group
    this.edgeWallGroup.add(resultMesh);

    return resultMesh;
  }

}
