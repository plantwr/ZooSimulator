import { Component, OnDestroy } from '@angular/core';
import { SignalRService } from './services/signalr.service';
import { Subscription } from 'rxjs';
import { Simulator, ISimulator } from './simulator';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnDestroy {
  private signalRSubscription: Subscription;

  public simulator: ISimulator;

  constructor(private signalrService: SignalRService) {
    this.simulator = new Simulator();
    this.signalRSubscription = this.signalrService.getMessage().subscribe(
      (simulator) => {
        console.log(simulator);
        this.simulator = simulator;
    });
  }

  feed() {
    this.signalrService.feed();
  }

  ngOnDestroy(): void {
    this.signalrService.disconnect();
    this.signalRSubscription.unsubscribe();
  }
}