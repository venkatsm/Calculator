import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Client, MathOperationRequestDto } from '../service-clients/calculator-api-client'
import { Util } from '../utils/util';
import { MathOperationResponseDto } from './model/math-operation-response';
import { isGuid } from 'check-guid'
import { SignalrService } from '../signalr.service';
import { NotificationDto } from './model/notification';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  form: FormGroup;
  submitted = false;
  sessionId: string = "";
  operations: MathOperationResponseDto[] = [];
  displayRequestStatus: boolean = false;
  isRequestSuccessful: boolean = false;

  constructor(private formBuilder: FormBuilder,
    private client: Client,
    private util: Util,
    public signalRService: SignalrService
  ) {
    this.sessionId = util.getSessionId();
    this.form = this.formBuilder.group(
      {
        firstNumber: ['', Validators.required],
        operator: ['', Validators.required],
        secondNumber: ['', Validators.required],
      });
  }

  ngOnInit(): void {
    this.signalRService.addNotificationListener();

    this.signalRService.notificationObservable.subscribe((notification: NotificationDto) => {
      this.operations.forEach(x => {
        if (x.id === notification.id) {
          x.status = notification.status;
          x.result = notification.result;
        }
      });
    });
  }

  onSubmit(): void {
    this.displayRequestStatus = false;
    
    let request = {
      firstNumber: this.form.controls['firstNumber'].value,
      secondNumber: this.form.controls['secondNumber'].value,
      operator: this.form.controls['operator'].value,
      sessionId: this.sessionId
    } as MathOperationRequestDto;

    this.client.submitMathOperationRequest(request).subscribe(x => {
      if (x !== null && isGuid(x)) {
        let response = {
          id: x,
          sessionId: this.sessionId,
          firstNumber: request.firstNumber,
          operator: request.operator,
          secondNumber: request.secondNumber
        } as MathOperationResponseDto;

        this.displayRequestStatus = true;
        this.isRequestSuccessful = true;
        this.operations.unshift(response);
      } else {
        this.displayRequestStatus = true;
        this.isRequestSuccessful = false;
      }
    });
  }
}
