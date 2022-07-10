import { Component, OnInit, ViewEncapsulation } from '@angular/core';
declare var $: any;  

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor() { }

  name = 'Jquery Integration With Angular!';  
  isJqueryWorking: any;  
  date: any;  
  ngOnInit()  
  {   
    
      $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active'); 
      });
      $('.dropdown-toggle').dropdown()
  }

    
}
