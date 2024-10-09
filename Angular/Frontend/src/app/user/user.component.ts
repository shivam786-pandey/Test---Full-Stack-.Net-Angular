import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  user: any ;
  userForm = new FormGroup({
    userName: new FormControl(''),
    email: new FormControl('')
  });
  users: any[] = [];
  showPopup = false;
  successMessage: string | null = null;
  errorMessage: string | null = null;

constructor(private userService:UserService) {
  
}
ngOnInit(){
 
}

getAllUsers(){
  this.userService.getAllUsers().subscribe(data=>{
    this.users = data;
  });
}
createUser() {
  const user = this.userForm.value;
  if (!user.email || !user.userName) {
    this.errorMessage = "Email and username are required.";
    console.log("Empty")
    this.successMessage = null;
    setTimeout(() => {
      this.errorMessage = null;
    }, 2000);
    return; 
  }
  this.userService.createUser(user).subscribe({
    next: (response: any) => {
        this.successMessage = "User created successfully!";
        this.errorMessage = null;
        this.userForm.reset();
        setTimeout(() => {
          this.successMessage = null;
        }, 2000);
    },
    error: (error: any) => {
      this.errorMessage = error.error;
      this.successMessage = null;
      setTimeout(() => {
        this.errorMessage = null;
      }, 2000);
    }
  });
}
showUserDetails() {
  this.getAllUsers();
  this.showPopup = true;
}

closePopup() {
  this.showPopup = false;
}
}
