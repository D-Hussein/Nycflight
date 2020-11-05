import { Component, AfterViewInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-planes-component',
  templateUrl: './planes.component.html'
})

export class PlanesComponent implements AfterViewInit {

  public http: HttpClient;
  public baseUrl: string;

  public manufacturersMoreThanTwoHundredPlanes: Map<string, number>;
  public flightsForManufacturersMoreThanTwoHundredPlanes: Map<string, number>;
  public planesForEachAirbusModel: Map<string, number>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngAfterViewInit() {
    //Feature 11 - Manufacturers with more than 200 planes
    this.loadManufacturersMoreThanTwoHundredPlanes();

    //Feature 12 - Flights for manufacturers with more than 200 planes
    this.loadFlightsForManufacturersWithMoreThanTwoHundredPlanes();

    //Feature 13 - Planes for each Airbus model
    this.loadPlanesForEachAirbusModel();
  }

  loadManufacturersMoreThanTwoHundredPlanes() {
    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/ManufacturersMoreThanTwoHundredPlanes').subscribe(result => {
      this.manufacturersMoreThanTwoHundredPlanes = result;
    }, error => console.error(error));
  }

  loadFlightsForManufacturersWithMoreThanTwoHundredPlanes() {
    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/FlightsManufacturersMoreThanTwoHundredPlanes').subscribe(result => {
      this.flightsForManufacturersMoreThanTwoHundredPlanes = result;
    }, error => console.error(error));
  }

  loadPlanesForEachAirbusModel() {
    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nyc_flight/PlanesforAirbus').subscribe(result => {
      this.planesForEachAirbusModel = result;
    }, error => console.error(error));
  }
}

