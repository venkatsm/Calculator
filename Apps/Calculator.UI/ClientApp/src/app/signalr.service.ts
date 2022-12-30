import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { NotificationDto } from './home/model/notification';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  hubUrl: string;
  connection: any;
  notificationObservable: BehaviorSubject<NotificationDto>;

  constructor() {
    this.hubUrl = 'https://notification-api-ms.azurewebsites.net/notify';
    this.notificationObservable = new BehaviorSubject<NotificationDto>(new NotificationDto());
  }

  public async initiateSignalrConnection(): Promise<void> {
    try {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(this.hubUrl)
        .withAutomaticReconnect()
        .build();

      await this.connection.start();

      console.log(`SignalR connection success! connectionId: ${this.connection.connectionId}`);
    }
    catch (error) {
      console.log(`SignalR connection error: ${error}`);
    }
  }

  public addNotificationListener(): void {
    this.connection.on('PublishNotification', (data: NotificationDto) => {
      console.log(data);
      this.notificationObservable.next(data);
    });
  }
}
