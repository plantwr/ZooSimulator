import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Subject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ISimulator } from '../simulator';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private message$: Subject<ISimulator>;
  private connection: signalR.HubConnection;

  constructor() {
    this.message$ = new Subject<ISimulator>();
    this.connection = new signalR.HubConnectionBuilder()
    .withUrl(environment.hubUrl)
    .build();

    this.connect();
  }

  private connect() {
    this.connection.start().then(() => this.connection.send("UpdateSimulator")).catch(err => console.log(err));

    this.connection.on('UpdateSimulator', (simulator) => {
      this.message$.next(simulator);
    });
  }

  public getMessage(): Observable<ISimulator> {
    return this.message$;
  }

  public feed() {
    this.connection.send("Feed");
  }

  public disconnect() {
    this.connection.stop();
  }
}