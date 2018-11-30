import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../Services/alert.service';


@Component({
    selector: 'alert',
    templateUrl: 'alert.component.html',
    styleUrls: ['./alert.component.css']
})

export class AlertComponent {
    messages: string[] = [];
    constructor(private alertService: AlertService) { }

    ngOnInit() {
        this.alertService.getMessage().subscribe(message => { this.messages.push(message); });
    }
}