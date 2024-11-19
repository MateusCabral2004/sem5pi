import { Component, AfterViewInit } from '@angular/core';
import {SurgeryRoomService} from "../../../services/SurgeryRoomService/surgery-room.service";

@Component({
  selector: 'app-connect3d',
  templateUrl: './connect3d.component.html',
  styleUrls: ['./connect3d.component.css']
})
export class Connect3dComponent implements AfterViewInit {

  iframeUrl: string = 'http://localhost:4201';
  arrayData: any[] = [];
  iframeElement!: HTMLIFrameElement;

  constructor(private surgeryRoomService: SurgeryRoomService) {
  }

  ngOnInit() {
    this.surgeryRoomService.getSurgeryRooms().subscribe({
      next: (data) => {
        this.arrayData = data;
      },
      error: (err) => {
        console.error('Error fetching surgery rooms:', err);
      }
    });
  }

  ngAfterViewInit() {
    this.iframeElement = document.getElementById('iframe') as HTMLIFrameElement;
    this.sendDataToIframe();
  }

  sendDataToIframe() {
    if (this.iframeElement) {
      this.iframeElement.onload = () => {
        this.iframeElement.contentWindow?.postMessage(this.arrayData, this.iframeUrl + "/hospitalFloor");
      };
    }
  }
}
