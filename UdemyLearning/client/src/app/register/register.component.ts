import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();


  model: any = {}
  constructor(private accountService: AccountService,
    private toastr: ToastrService) { }

  ngOnInit(): void {

  }

  register() {
    this.accountService.register(this.model).subscribe(
      Response => {
        console.log(Response);
        this.cancel();
      }, Error => {
        console.log(Error);
        this.toastr.error(Error.error);
      })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }


}
