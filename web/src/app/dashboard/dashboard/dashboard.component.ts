import { Component, OnChanges, OnInit, SimpleChanges, ViewEncapsulation } from '@angular/core';
declare var $: any;  

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit,OnChanges {

  ismaxwindow: boolean = true;

  constructor() { }


  ngOnChanges(changes: SimpleChanges): void {
    if($( window ).width() < 900){
      //this.ismaxwindow=false;
    }
  }

  name = 'Jquery Integration With Angular!';  
  isJqueryWorking: any;  
  date: any;  
  ngOnInit()  
  {   
    
      $('#sidebarCollapse').on('click', function () {
        console.log($( window ).width());
        if($( window ).width() < 900)
        $('#sidebar').toggleClass('active');
      });

      $('.dropdown-toggle').dropdown()
  }


    
}
