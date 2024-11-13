import { Component, ViewChild, AfterViewInit } from '@angular/core';
import * as THREE from 'three';
import { SurgeryRoomComponent } from '../surgery-room/surgery-room.component';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';

@Component({
  selector: 'app-operation-floor',
  templateUrl: './operation-floor.component.html',
  styleUrls: ['./operation-floor.component.css']
})
export class OperationFloorComponent implements AfterViewInit {
  @ViewChild(SurgeryRoomComponent) surgeryRoomComponent!: SurgeryRoomComponent;

  private renderer!: THREE.WebGLRenderer;
  private scene!: THREE.Scene;
  private camera!: THREE.PerspectiveCamera;
  private controls!: OrbitControls;

  ngOnInit(): void {
    this.initialize();
  }

  private initialize(): void {
    this.initScene();
    this.initRenderer();
    this.initCamera();
  }

  private initScene(): void {
    this.scene = new THREE.Scene();
    //this.scene.background = new THREE.Color(0xdddddd);
  }

  private initRenderer(): void {
    this.renderer = new THREE.WebGLRenderer();
    this.renderer.setSize(window.innerWidth, window.innerHeight);
    document.body.appendChild(this.renderer.domElement);
  }

  private initCamera(): void {
    this.camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
    this.camera.position.set(0, 0, 15);  // Position the camera away from the room
  }

  ngAfterViewInit(): void {
    this.createRooms();
    this.setupCameraControls();
    this.render();
  }

  private createRooms(): void {
    this.surgeryRoomComponent.createRoom();
    this.scene.add(this.surgeryRoomComponent.roomGroup);
  }

  private setupCameraControls(): void {
    this.controls = new OrbitControls(this.camera, this.renderer.domElement);
    this.controls.enableZoom = true;
    this.controls.enableRotate = true;
    this.controls.enablePan = false;

    this.controls.mouseButtons.LEFT = THREE.MOUSE.PAN; // the pan is false so that means that the left button is disabled
    this.controls.mouseButtons.RIGHT = THREE.MOUSE.ROTATE;
  }

  private render = () => {
    this.controls.update();
    this.renderer.render(this.scene, this.camera);
    requestAnimationFrame(this.render);
  };
}
