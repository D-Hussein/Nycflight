import { Component, AfterViewInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as CanvasJS from '../../assets/canvasjs.min.js';
import { Pipe, PipeTransform } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './flights.component.html'
})

export class FlightsComponent implements AfterViewInit {

  public http: HttpClient;
  public baseUrl: string;

  public flightsToTopTenDestinations: Map<string, number>;
  public meanAirtimeForOrigins: Map<string, string>;
  public meanDepartureAndArrivalDelaysForJFK: Map<string, string>;
  public meanDepartureAndArrivalDelaysForEWR: Map<string, string>;
  public meanDepartureAndArrivalDelaysForLGA: Map<string, string>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngAfterViewInit() {

    //Feature 1 - Flights per month
    this.loadFlightsPerMonth();

    //Feature 2 - Flights per month from origins
    this.loadFlightsPerMonthFromOrigins();

    //Feature 3 - Flights to top 10 destinations
    this.loadFlightToTopTenDestinations();

    //Feature 4 - Mean airtime for origins
    this.loadMeanAirtimeForOrigins();

    //Feature 10 - Departure and arrival delays for origins
    this.loadDepartureAndArrivalDelaysForOrigins();
  }

  loadFlightsPerMonth() {
    let dataPoints = [];

    let chart = new CanvasJS.Chart("chartContainer1", {
      animationEnabled: true,
      title: {
        text: "Number of flights per month"
      },
      axisX: {
        title: "Month",
        interval: 1
      },
      axisY: {
        title: "Number of flights"
      },
      data: [{
        type: "column",
        dataPoints: dataPoints,
        color: "#2E86C1"
      }]
    });
    chart.render();

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/FlightsPerMonth').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPoints.push({ label: key, y: result[key] })
      });
      chart.render();
    }, error => console.error(error));
  }

  loadFlightsPerMonthFromOrigins() {
    let dataPointsJFK = [];
    let dataPointsEWR = [];
    let dataPointsLGA = [];

    let chartFrequency = new CanvasJS.Chart("chartContainer2Frequency", {
      animationEnabled: true,
      title: {
        text: "Number of flights per month - frequency"
      },
      axisX: {
        title: "Month",
        interval: 1
      },
      axisY: {
        title: "Number of flights"
      },
      data: [{
        type: "column",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      },
      {
        type: "column",
        legendText: "EWR",
        showInLegend: true,
        dataPoints: dataPointsEWR,
        color: "#C13B2E"
      },
      {
        type: "column",
        legendText: "LGA",
        showInLegend: true,
        dataPoints: dataPointsLGA,
        color: "#2EC146"
      },
      ]
    });

    let chartStacked = new CanvasJS.Chart("chartContainer2Stacked", {
      animationEnabled: true,
      title: {
        text: "Number of flights per month - stacked"
      },
      axisX: {
        title: "Month",
        interval: 1
      },
      axisY: {
        title: "Number of flights"
      },
      data: [{
        type: "stackedColumn",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      },
      {
        type: "stackedColumn",
        legendText: "EWR",
        showInLegend: true,
        dataPoints: dataPointsEWR,
        color: "#C13B2E"
      },
      {
        type: "stackedColumn",
        legendText: "LGA",
        showInLegend: true,
        dataPoints: dataPointsLGA,
        color: "#2EC146"
      },
      ]
    });

    let chartStackedPercentage = new CanvasJS.Chart("chartContainer2StackedPercentage", {
      animationEnabled: true,
      title: {
        text: "Number of flights per month - stacked percentage"
      },
      axisX: {
        title: "Month",
        interval: 1
      },
      axisY: {
        title: "Number of flights",
        suffix: "%"
      },
      data: [{
        type: "stackedColumn100",
        legendText: "JFK",
        toolTipContent: "{y} (#percent%)",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      },
      {
        type: "stackedColumn100",
        legendText: "EWR",
        toolTipContent: "{y} (#percent%)",
        showInLegend: true,
        dataPoints: dataPointsEWR,
        color: "#C13B2E"
      },
      {
        type: "stackedColumn100",
        legendText: "LGA",
        toolTipContent: "{y} (#percent%)",
        showInLegend: true,
        dataPoints: dataPointsLGA,
        color: "#2EC146"
      },
      ]
    });

    chartFrequency.render();
    chartStacked.render();
    chartStackedPercentage.render();

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/FlightsPerMonthForJFK').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFK.push({ label: key, y: result[key] })
      });

      chartFrequency.render();
      chartStacked.render();
      chartStackedPercentage.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/FlightsPerMonthForEWR').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsEWR.push({ label: key, y: result[key] })
      });

      chartFrequency.render();
      chartStacked.render();
      chartStackedPercentage.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/FlightsPerMonthForLGA').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsLGA.push({ label: key, y: result[key] })
      });

      chartFrequency.render();
      chartStacked.render();
      chartStackedPercentage.render();
    }, error => console.error(error));
    
  }

  loadFlightToTopTenDestinations() {

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/FlightsToTopTenDestinations').subscribe(result => {
      this.flightsToTopTenDestinations = result;
    }, error => console.error(error));

    let dataPointsJFKToDestinations = [];
    let dataPointsEWRToDestinations = [];
    let dataPointsLGAToDestinations = [];

    let chart = new CanvasJS.Chart("chartContainer3", {
      animationEnabled: true,
      title: {
        text: "Number of flights to top 10 destinations from origins"
      },
      axisX: {
        title: "Destination",
        interval: 1
      },
      axisY: {
        title: "Number of flights"
      },
      data: [{
        type: "column",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFKToDestinations,
        color: "#2E86C1"
      },
      {
        type: "column",
        legendText: "EWR",
        showInLegend: true,
        dataPoints: dataPointsEWRToDestinations,
        color: "#C13B2E"
      },
      {
        type: "column",
        legendText: "LGA",
        showInLegend: true,
        dataPoints: dataPointsLGAToDestinations,
        color: "#2EC146"
      },
      ]
    });
    chart.render();

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/FlightsToTopTenDestinationsFromJFK').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFKToDestinations.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/FlightsToTopTenDestinationsFromEWR').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsEWRToDestinations.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/FlightsToTopTenDestinationsFromLGA').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsLGAToDestinations.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error)); 
  }

  loadMeanAirtimeForOrigins() {
    this.http.get<Map<string, string>>(this.baseUrl + 'api/Nyc_flight/MeanAirtimeForOrigins').subscribe(result => {
      this.meanAirtimeForOrigins = result;
    }, error => console.error(error));
  }

  loadDepartureAndArrivalDelaysForOrigins() {
    this.http.get<Map<string, string>>(this.baseUrl + 'api/Nyc_flight/MeanDepartureAndArrivalDelayForJFK').subscribe(result => {
      this.meanDepartureAndArrivalDelaysForJFK = result;
    }, error => console.error(error));

    this.http.get<Map<string, string>>(this.baseUrl + 'api/Nyc_flight/MeanDepartureAndArrivalDelayForEWR').subscribe(result => {
      this.meanDepartureAndArrivalDelaysForEWR = result;
    }, error => console.error(error));

    this.http.get<Map<string, string>>(this.baseUrl + 'api/Nyc_flight/MeanDepartureAndArrivalDelayForLGA').subscribe(result => {
      this.meanDepartureAndArrivalDelaysForLGA = result;
    }, error => console.error(error));
  }

}

@Pipe({ name: 'getValues' })
export class GetValuesPipe implements PipeTransform {
  transform(map: Map<string, number>): any[] {
    let ret = [];

    Object.keys(map).forEach(function (key) {
      ret.push({ key: key, val: map[key] })
    });

    return ret;
  }
}


