import { Component, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-connect3d',
  templateUrl: './connect3d.component.html',
  styleUrls: ['./connect3d.component.css']
})
export class Connect3dComponent implements AfterViewInit {

  iframeUrl: string = 'http://localhost:4201';
  arrayData: any[] = [false,true,false,true,false,false];

  iframeElement!: HTMLIFrameElement;

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
