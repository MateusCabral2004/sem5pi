import { Component, Input } from '@angular/core';
import * as THREE from 'three';
import { CSG } from 'three-csg-ts';
import { GLTFLoader } from 'three-stdlib';

@Component({
  selector: 'app-surgery-room',
  templateUrl: './surgery-room.component.html',
  styleUrls: ['./surgery-room.component.css']
})
export class SurgeryRoomComponent {

  public roomGroup!: THREE.Group;
  public wallTexture: string = 'assets/wall.jpg';
  public floorTexture: string = 'assets/roomFloor.jpg';
  public doorTexture: string = 'assets/slidingDoors.jpg';

  private wallThickness: number = 0.1;
  private roomWidth!: number;
  private roomHeight!: number;
  private roomDepth!: number;

  public isOperating: boolean = false;

  public createRoom(isOperating: boolean = false,width: number,height:number): void {
    this.roomWidth = width;
    this.roomDepth = width;
    this.roomHeight = height;
    this.isOperating = isOperating;
    this.roomGroup = new THREE.Group();
    this.createRoomWalls();
    this.createRoomFloor();
    this.equipRoomWithSurgicalEquipment();

    if (this.isOperating) {
      this.startSurgery();
    }
  }

  private createRoomWalls(): void {
    const texture = new THREE.TextureLoader().load(this.wallTexture);
    texture.wrapS = THREE.RepeatWrapping;
    texture.wrapT = THREE.RepeatWrapping;
    texture.repeat.set(1, 1);

    const wallMaterial = new THREE.MeshStandardMaterial({ map: texture, side : THREE.DoubleSide });
    const whiteMaterial = new THREE.MeshStandardMaterial({ color: 0xffffff, side: THREE.DoubleSide });

    const frontBackWallGeometry = new THREE.BoxGeometry(this.roomWidth, this.roomHeight, this.wallThickness);
    const sideWallGeometry = new THREE.BoxGeometry(this.wallThickness, this.roomHeight, this.roomDepth);
    const doorGeometry = new THREE.BoxGeometry(2.5, 2.5, this.wallThickness);

    const frontWall = new THREE.Mesh(frontBackWallGeometry);
    frontWall.position.set(0, 0, this.roomDepth / 2 - this.wallThickness / 2);
    frontWall.receiveShadow = true;
    frontWall.castShadow = false;
    frontWall.updateMatrix();

    const doorTexture = new THREE.TextureLoader().load(this.doorTexture);
    const door = new THREE.Mesh(doorGeometry, new THREE.MeshStandardMaterial({ map: doorTexture, side: THREE.DoubleSide  }));
    door.position.set(0, -0.25, this.roomDepth / 2 - this.wallThickness / 2);
    door.castShadow = false;
    door.receiveShadow = true;
    door.updateMatrix();

    const frontWallCSG = CSG.fromMesh(frontWall);
    const doorCSG = CSG.fromMesh(door);
    const resultCSG = frontWallCSG.subtract(doorCSG);
    const resultMesh = CSG.toMesh(resultCSG, frontWall.matrix);
    resultMesh.material = [wallMaterial, wallMaterial, whiteMaterial, whiteMaterial, wallMaterial, wallMaterial];
    resultMesh.castShadow = false;
    resultMesh.receiveShadow = true;

    this.roomGroup.add(resultMesh);

    const backWall = new THREE.Mesh(frontBackWallGeometry, [wallMaterial, wallMaterial, whiteMaterial, whiteMaterial, wallMaterial, wallMaterial]);
    backWall.position.set(0, 0, -this.roomDepth / 2 + this.wallThickness / 2);
    backWall.receiveShadow = true;
    backWall.castShadow = false;
    this.roomGroup.add(backWall);

    const leftWall = new THREE.Mesh(sideWallGeometry, [wallMaterial, wallMaterial, whiteMaterial, whiteMaterial, wallMaterial, wallMaterial]);
    leftWall.position.set(-this.roomWidth / 2 + this.wallThickness / 2, 0, 0);
    leftWall.receiveShadow = true;
    leftWall.castShadow = false;
    this.roomGroup.add(leftWall);

    const rightWall = new THREE.Mesh(sideWallGeometry, [wallMaterial, wallMaterial, whiteMaterial, whiteMaterial, wallMaterial, wallMaterial]);
    rightWall.position.set(this.roomWidth / 2 - this.wallThickness / 2, 0, 0);
    rightWall.receiveShadow = true;
    rightWall.castShadow = false;
    this.roomGroup.add(rightWall);

    this.roomGroup.add(door);
  }

  private createRoomFloor(): void {
    const texture = new THREE.TextureLoader().load(this.floorTexture);
    texture.wrapS = THREE.RepeatWrapping;
    texture.wrapT = THREE.RepeatWrapping;
    texture.repeat.set(1, 1);

    const floorMaterial = new THREE.MeshStandardMaterial({ map: texture });
    const floorGeometry = new THREE.BoxGeometry(this.roomWidth, this.wallThickness, this.roomDepth);
    const floor = new THREE.Mesh(floorGeometry, floorMaterial);
    floor.position.set(0, -this.roomHeight / 2, 0);
    floor.receiveShadow = true;
    floor.castShadow = false;
    floor.updateMorphTargets();
    this.roomGroup.add(floor);
  }

  private equipRoomWithSurgicalEquipment(): void {
    this.createMedicalTable();
    this.createMedicalLamp();
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
    }, undefined, (error) => {
      console.error('Error loading medical table model:', error);
    });
  }

  public startSurgery(): void {
    this.createPatient();
    this.createOperatingStaff();
    this.createLampLight();
  }

  public stopSurgery(): void {
    this.removePatient();
    this.removeOperatingStaff();
    this.removeLampLight();
  }

  private removePatient(): void {
    const patient = this.roomGroup.getObjectByName('patient');
    if (patient) {
      this.roomGroup.remove(patient);
      console.log('Patient removed from the room');
    }
  }

  private removeOperatingStaff(): void {
    const doctor = this.roomGroup.getObjectByName('doctor');
    if (doctor) {
      this.roomGroup.remove(doctor);
      console.log('Doctor removed from the room');
    }

    const nurse = this.roomGroup.getObjectByName('nurse');
    if (nurse) {
      this.roomGroup.remove(nurse);
      console.log('Nurse removed from the room');
    }
  }

  private removeLampLight(): void {
    const lampLight = this.roomGroup.getObjectByName('lampLight');
    if (lampLight) {
      this.roomGroup.remove(lampLight);
      console.log('Lamp light removed from the room');
    }
  }

  private createPatient(): void {
    const loader = new GLTFLoader();
    loader.load('assets/Patient/patient.glb', (gltf) => {
      const patientModel = gltf.scene;
      patientModel.scale.set(0.0115, 0.0115, 0.0115);
      patientModel.position.set(0, -0.3, 0.5);
      patientModel.traverseVisible(function (child) {
        child.castShadow = true;
        child.receiveShadow = true;
      });
      patientModel.name = 'patient';
      this.roomGroup.add(patientModel);
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
      doctorModel.name = 'doctor';
      this.roomGroup.add(doctorModel);
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
      nurseModel.name = 'nurse';
      this.roomGroup.add(nurseModel);
    }, undefined, (error) => {
      console.error('Error loading nurse model:', error);
    });
  }

  private createMedicalLamp(): void {
    const loader = new GLTFLoader();
    loader.load('assets/SurgicalLight/floorLight.glb', (gltf) => {
      const medicalLampModel = gltf.scene;
      medicalLampModel.scale.set(1.2, 1.2, 1.6);
      medicalLampModel.rotation.x = -Math.PI / 2;
      medicalLampModel.position.set(-1.4, -1.35, -0.2);
      medicalLampModel.traverseVisible(function (child) {
        child.castShadow = true;
        child.receiveShadow = true;
      });
      this.roomGroup.add(medicalLampModel);
    }, undefined, (error) => {
      console.error('Error loading medical lamp model:', error);
    });
  }

  private createLampLight(): void {
    const lampLight = new THREE.SpotLight(0xffffff, 40, 3.4, Math.PI/4); // Increased intensity and smaller cone angle

    lampLight.position.set(0, 1.8, 0.5);
    lampLight.target.position.set(0, 0, 0.5);

    lampLight.castShadow = true;
    lampLight.shadow.mapSize.width = 2048;
    lampLight.shadow.mapSize.height = 2048;

    lampLight.shadow.camera.near = 0.1;
    lampLight.shadow.camera.far = 20;

    lampLight.name = 'lampLight';
    lampLight.target.name = 'lampLightTarget';

    this.roomGroup.add(lampLight);
    this.roomGroup.add(lampLight.target);
  }

}
