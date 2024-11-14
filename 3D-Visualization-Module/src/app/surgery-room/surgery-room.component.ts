import {Component} from '@angular/core';
import * as THREE from 'three';
import {CSG} from 'three-csg-ts';
import {GLTFLoader} from 'three-stdlib';

@Component({
  selector: 'app-surgery-room',
  templateUrl: './surgery-room.component.html',
  styleUrls: ['./surgery-room.component.css']
})
export class SurgeryRoomComponent {

  public roomGroup!: THREE.Group;
  public wallTexture: string = 'assets/wall.jpg';
  public floorTexture: string = 'assets/wall.jpg';
  public doorTexture: string = 'assets/slidingDoors.jpg';

  private wallThickness: number = 0.1;
  private roomWidth: number = 8;
  private roomHeight: number = 3;
  private roomDepth: number = 8;

  private beingUsed: boolean = true;

  constructor() {
    this.createRoom();
  }

  public createRoom(): void {
    this.roomGroup = new THREE.Group();
    this.createRoomWalls();
    this.createRoomFloor();
    this.equipRoomWithSurgicalEquipment();
    if (this.beingUsed) {
      this.startSurgery();
    }

    const ambientLight = new THREE.AmbientLight(0xffffff, 0.6);
    this.roomGroup.add(ambientLight);
  }

  private createRoomWalls(): void {
    const texture = new THREE.TextureLoader().load(this.wallTexture);
    texture.wrapS = THREE.RepeatWrapping;
    texture.wrapT = THREE.RepeatWrapping;
    texture.repeat.set(1, 1);

    const wallMaterial = new THREE.MeshBasicMaterial({map: texture});
    const whiteMaterial = new THREE.MeshBasicMaterial({color: 0xffffff});

    const frontBackWallGeometry = new THREE.BoxGeometry(this.roomWidth, this.roomHeight, this.wallThickness);
    const sideWallGeometry = new THREE.BoxGeometry(this.wallThickness, this.roomHeight, this.roomDepth);
    const doorGeometry = new THREE.BoxGeometry(2.5, 2.5, this.wallThickness);

    const frontWall = new THREE.Mesh(frontBackWallGeometry);
    frontWall.position.set(0, 0, this.roomDepth / 2 - this.wallThickness / 2);
    frontWall.updateMatrix();

    const door = new THREE.Mesh(doorGeometry, new THREE.MeshBasicMaterial({map: new THREE.TextureLoader().load(this.doorTexture)}));
    door.position.set(0, -0.25, this.roomDepth / 2 - this.wallThickness / 2);
    door.updateMatrix();

    const frontWallCSG = CSG.fromMesh(frontWall);
    const doorCSG = CSG.fromMesh(door);
    const resultCSG = frontWallCSG.subtract(doorCSG);
    const resultMesh = CSG.toMesh(resultCSG, frontWall.matrix);

    resultMesh.material = [wallMaterial, wallMaterial, whiteMaterial, whiteMaterial, wallMaterial, wallMaterial];

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

    const floorMaterial = new THREE.MeshBasicMaterial({color: 0xf0f0f0});
    const floorGeometry = new THREE.BoxGeometry(this.roomWidth, this.wallThickness, this.roomDepth);
    const floor = new THREE.Mesh(floorGeometry, floorMaterial);
    floor.position.set(0, -this.roomHeight / 2, 0);
    this.roomGroup.add(floor);
  }

  private equipRoomWithSurgicalEquipment(): void {
    this.createMedicalTable();
  }

  public startSurgery(): void {
    this.beingUsed = true;
    this.createPatient();
    this.createOperatingStaff();
  }

  public endSurgery(): void {
    this.beingUsed = false;
  }

  private createPatient(): void {
    const loader = new GLTFLoader();
    loader.load('assets/Patient/patient.glb', (gltf) => {
      const patientModel = gltf.scene;
      patientModel.scale.set(0.0115, 0.0115, 0.0115);
      patientModel.position.set(0, -0.3, 0.5);
     // patientModel.rotation.y = Math.PI / 2;
      patientModel.traverseVisible(function (child) {
        child.castShadow = true;
        child.receiveShadow = true;
      });
      this.roomGroup.add(patientModel);

      const directionalLight = new THREE.DirectionalLight(0xffffff, 0.5);
      directionalLight.position.set(0, 1, 0);
      patientModel.add(directionalLight);
    }, undefined, (error) => {
      console.error('Error loading patient model:', error);
    });
  }

  private createOperatingStaff(): void {
    this.createDoctor();
    this.createNurse();
  }

  private createDoctor(): void {
    const loader = new GLTFLoader();
    loader.load('assets/Doctor/doctor.glb', (gltf) => {
      const doctorModel = gltf.scene;
      doctorModel.scale.set(0.013, 0.013, 0.013);
      doctorModel.position.set(1.0, -1.4, 0.5);
      doctorModel.rotation.y = -Math.PI / 2;
      doctorModel.traverseVisible(function (child) {
        child.castShadow = true;
        child.receiveShadow = true;
      });
      this.roomGroup.add(doctorModel);

      const directionalLight = new THREE.DirectionalLight(0xffffff, 0.5);
      directionalLight.position.set(0, 1, 0);
      doctorModel.add(directionalLight);
    }, undefined, (error) => {
      console.error('Error loading doctor model:', error);
    });
  }

  private createNurse(): void {
    const loader = new GLTFLoader();
    loader.load('assets/Nurse/nurse.glb', (gltf) => {
      const nurseModel = gltf.scene;
      nurseModel.scale.set(0.0095, 0.0095, 0.0095);
      nurseModel.position.set(-1.0, -1.37, 0.5);
      nurseModel.rotation.y = Math.PI / 2;
      nurseModel.traverseVisible(function (child) {
        child.castShadow = true;
        child.receiveShadow = true;
      });
      this.roomGroup.add(nurseModel);

      const directionalLight = new THREE.DirectionalLight(0xffffff, 0.5);
      directionalLight.position.set(0, 1, 0);
      nurseModel.add(directionalLight);
    }, undefined, (error) => {
      console.error('Error loading nurse model:', error);
    });
  }

  private createMedicalTable(): void {
    const loader = new GLTFLoader();
    loader.load('assets/MedicalTable/table.glb', (gltf) => {
      const medicalTableModel = gltf.scene;
      medicalTableModel.scale.set(9, 9, 9);
      medicalTableModel.position.set(-0.0, 0.8, -3.0);
      medicalTableModel.traverseVisible(function (child) {
        child.castShadow = true;
        child.receiveShadow = true;
      });

      this.roomGroup.add(medicalTableModel);

      const directionalLight = new THREE.DirectionalLight(0xffffff, 0.5);
      directionalLight.position.set(0, 1, 0);
      medicalTableModel.add(directionalLight);
    }, undefined, (error) => {
      console.error('Error loading medical table model:', error);
    });
  }

}
