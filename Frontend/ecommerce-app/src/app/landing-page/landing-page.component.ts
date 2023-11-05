import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  constructor(private api: ApiService) { }

  ngOnInit(): void {
    const res = localStorage.getItem('user');
    if (!!res) this.api.currentSession = JSON.parse((res as string));
  }
}
