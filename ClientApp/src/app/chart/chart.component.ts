import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartDataSets, ChartType, ChartOptions } from 'chart.js';
import { Label, BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {

  // scatter
  public scatterChartOptions: ChartOptions = {
    responsive: true,
    legend: {
      display: false
    },
    tooltips: {
      enabled: false
    },
    title: {
      display: false
    },
    animation:{
      duration: 0,
      easing: "linear"
    },
    scales: {
      yAxes: [{
        ticks: {
          suggestedMax: 10,
          suggestedMin: -10,
          display: false
        },
        scaleLabel: {
          labelString: 'Alignment Label 1',
          display: true
        }
      }],
      xAxes: [{
        ticks: {
          suggestedMax: 10,
          suggestedMin: -10,
          display: false
        },
        scaleLabel: {
          labelString: 'Alignment Label 2',
          display: true
        }
      }]
    }
  };
  public scatterChartLabels: Label[] = ['Eating', 'Drinking', 'Sleeping', 'Designing', 'Coding', 'Cycling', 'Running'];

  public scatterChartData: ChartDataSets[] = [
    {
      data: [
        // { x: 1, y: 1 },
        // { x: 2, y: 3 },
        // { x: 3, y: -2 },
        // { x: 4, y: 4 },
        // { x: 5, y: -3 },
      ],
      label: 'Series A',
      pointRadius: 10,
    },
  ];
  public scatterChartType: ChartType = 'scatter';

  @ViewChild(BaseChartDirective, { static: true }) chart: BaseChartDirective;

  constructor() { }

  ngOnInit() {
  }

  // events
  public chartClicked({ event, active }: { event: MouseEvent, active: {}[] }): void {
    
    var yTop = this.chart.chart.chartArea.top;
    var yBottom = this.chart.chart.chartArea.bottom;

    var yMin = -10; //this.chart.options.scales['y-axis-0'].min;
    var yMax = 10; //this.chart.options.scales['y-axis-0'].max;
    var newY = 0;

    if (event.offsetY <= yBottom && event.offsetY >= yTop) {
        newY = Math.abs((event.offsetY - yTop) / (yBottom - yTop));
        newY = (newY - 1) * -1;
        newY = newY * (Math.abs(yMax - yMin)) + yMin;
    };

    var xTop = this.chart.chart.chartArea.left;
    var xBottom = this.chart.chart.chartArea.right;
    var xMin = -10; //this.chart.options.scales['x-axis-0'].min;
    var xMax = 10; //this.chart.options.scales['x-axis-0'].max;
    var newX = 0;

    if (event.offsetX <= xBottom && event.offsetX >= xTop) {
        newX = Math.abs((event.offsetX - xTop) / (xBottom - xTop));
        newX = newX * (Math.abs(xMax - xMin)) + xMin;
    };
    var newPoint: Chart.ChartPoint[] = [{ x: newX, y: newY }];

    this.scatterChartData = [
      {
        data: newPoint,
        label: 'Series A',
        pointRadius: 10,
      },
    ];

    this.chart.update();
    
  }

}
