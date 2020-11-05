import { Component, AfterViewInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as CanvasJS from '../../assets/canvasjs.min.js';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html'
})
export class WeatherComponent implements AfterViewInit {

  public http: HttpClient;
  public baseUrl: string;

  public weatherObservationsForOrigins: Map<string, number>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngAfterViewInit() {
    //Feature 5 - Weather observations for origins
    this.loadWeatherObservationsForOrigins();

    //Feature 6 - Temperature attributes for origins
    this.loadTemperatureAttributesForOrigins();

    //Feature 7 - Temperatures for JFK
    this.loadTemperaturesForJFK();

    //Feature 8 - Daily mean temperature for JFK
    this.loadDailyMeanTemperatureForJFK();

    //Feature 9 - Daily mean temperature for origins
    this.loadDailyMeanTemperatureForOrigins();
  }

  loadWeatherObservationsForOrigins() {
    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/WeatherObservationsForOrigins').subscribe(result => {
      this.weatherObservationsForOrigins = result;
    }, error => console.error(error));
  }

  loadTemperatureAttributesForOrigins() {
    let dataPointsJFK = [];
    let dataPointsEWR = [];
    let dataPointsLGA = [];

    let chart = new CanvasJS.Chart("chartContainer6", {
      animationEnabled: true,
      title: {
        text: "Temperature attributes for origins"
      },
      axisX: {
        title: "Date",
      },
      axisY: {
        title: "Temperature in Celsius"
      },
      data: [{
        type: "scatter",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      },
      {
        type: "scatter",
        legendText: "EWR",
        showInLegend: true,
        dataPoints: dataPointsEWR,
        color: "#C13B2E"
      },
      {
        type: "scatter",
        legendText: "LGA",
        showInLegend: true,
        dataPoints: dataPointsLGA,
        color: "#2EC146"
      },
      ]
    });

    chart.render();

    this.http.get<Map<Date, number>>(this.baseUrl + 'api/Nycflights/TemperatureInCelsiusForJFK').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFK.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<Date, number>>(this.baseUrl + 'api/Nycflights/TemperatureInCelsiusForEWR').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsEWR.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<Date, number>>(this.baseUrl + 'api/Nycflights/TemperatureInCelsiusForLGA').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsLGA.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error));
  }

  loadTemperaturesForJFK() {
    let dataPointsJFK = [];

    let chart = new CanvasJS.Chart("chartContainer7", {
      animationEnabled: true,
      title: {
        text: "Temperatures registered for JFK"
      },
      axisX: {
        title: "Date",
      },
      axisY: {
        title: "Temperature in Celsius"
      },
      data: [{
        type: "scatter",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      }]
    });

    chart.render();

    this.http.get<Map<Date, number>>(this.baseUrl + 'api/Nycflights/TemperatureInCelsiusForJFK').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFK.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error));
  }

  loadDailyMeanTemperatureForJFK() {
    let dataPointsJFK = [];

    let chart = new CanvasJS.Chart("chartContainer8", {
      animationEnabled: true,
      title: {
        text: "Daily mean temperature for JFK"
      },
      axisX: {
        title: "Date",
      },
      axisY: {
        title: "Mean temperature"
      },
      data: [{
        type: "scatter",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      }]
    });

    chart.render();

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/DailyMeanTempInCelsiusForJFK').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFK.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error));
  }

  loadDailyMeanTemperatureForOrigins() {
    let dataPointsJFK = [];
    let dataPointsEWR = [];
    let dataPointsLGA = [];

    let chart = new CanvasJS.Chart("chartContainer9", {
      animationEnabled: true,
      title: {
        text: "Daily mean temperature for origins"
      },
      axisX: {
        title: "Date",
      },
      axisY: {
        title: "Mean temperature"
      },
      data: [{
        type: "scatter",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      },
      {
        type: "scatter",
        legendText: "EWR",
        showInLegend: true,
        dataPoints: dataPointsEWR,
        color: "#C13B2E"
      },
      {
        type: "scatter",
        legendText: "LGA",
        showInLegend: true,
        dataPoints: dataPointsLGA,
        color: "#2EC146"
      },
      ]
    });

    chart.render();

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/DailyMeanTempInCelsiusForJFK').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFK.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/DailyMeanTempInCelsiusForEWR').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsEWR.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/DailyMeanTempInCelsiusForLGA').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsLGA.push({ label: key, y: result[key] })
      });

      chart.render();
    }, error => console.error(error));
  }
}

