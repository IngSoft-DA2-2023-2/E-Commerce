import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/api.service';

@Component({
  selector: 'app-signup-view',
  templateUrl: './signup-view.component.html',
  styleUrls: ['./signup-view.component.css']
})

export class SignupViewComponent {
  constructor(private api:ApiService,private router:Router) { }

  goBack() {
    this.router.navigate(['']);
}

signUpUser(name:HTMLInputElement,email:HTMLInputElement,address:HTMLInputElement,password:HTMLInputElement){
  console.log("Signing up")
  console.log(`Name: ${name.value} Email: ${email.value} Address: ${address.value} Password: ${password.value}`);
  this.api.postUser({ name: name.value, email: email.value, address: address.value, password: password.value }).subscribe({
    next: (response) => {
      // Función de devolución de llamada en caso de éxito
      console.log('Solicitud exitosa:', response);
      // Aquí puedes realizar acciones adicionales, como actualizar la interfaz de usuario.
    },
    error: (error) => {
      // Función de devolución de llamada en caso de error
      console.error('Error en la solicitud:', error);
      // Puedes manejar el error de acuerdo a tus necesidades, como mostrar un mensaje de error al usuario.
    },
    complete: () => {
      // Función de devolución de llamada cuando la solicitud se completa (opcional).
      console.log('Solicitud completada');
    }
  });
}
}  