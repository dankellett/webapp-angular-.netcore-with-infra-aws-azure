import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ChartDataSets, ChartType, ChartOptions } from 'chart.js';
import { Label, BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {

  @ViewChild(BaseChartDirective, { static: true }) chart: BaseChartDirective;

  private http: HttpClient;
  private baseUrl: string;

  constructor(_http: HttpClient, @Inject('BASE_URL') _baseUrl: string) {
    this.http = _http;
    this.baseUrl = _baseUrl;
  }

  ngOnInit() {
    this.getPoint();
  }

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

    this.updatePointOnChart(newX, newY);
    this.savePoint(newX, newY);
  }

  private updatePointOnChart(x:number, y:number){
    var newPoint: Chart.ChartPoint[] = [{ x: x, y: y }];

    this.scatterChartData = [
      {
        data: newPoint,
        label: 'Series A',
        pointRadius: 10,
      },
    ];

    this.chart.update();
  }

  private savePoint(x:number, y:number) : void {
    let alignmentEntryDto: AlignmentEntryDto = {x: x, y: y};
    let body = alignmentEntryDto;
    const headerOptions = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

    this.http.post(this.baseUrl + 'alignmententry', body, { headers: headerOptions }).subscribe(result => {
    }, error => console.error(error));
  }

  private getPoint(){
    this.http.get<AlignmentEntryDto>(this.baseUrl + 'alignmententry').subscribe(result => {
      this.updatePointOnChart(result.x, result.y);
    }, error => console.error(error));
  }
}

class AlignmentEntryDto {
  x: number;
  y: number;
}
