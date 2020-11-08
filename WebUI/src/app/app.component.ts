import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

enum GuessResult {
  Hit = 0,
  Sink = 1,
  Miss = 2
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  private baseUri = 'https://localhost:44349/api/';

  public coordinates: FormControl;
  public result = '';

  public constructor(private http: HttpClient){}

  public ngOnInit(): void {
    this.coordinates = new FormControl('');
    this.restartGame();
  }

  public restartGame(): void {
    this.result = '';

    this.http.post(this.baseUri + 'initialise', {}, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Accept: 'application/json'
      })
    }).subscribe();
  }

  public submit(): void {
    console.log(this.coordinates.value);

    const body = JSON.stringify(this.coordinates.value);

    this.http.post<GuessResult>(this.baseUri + 'fire', body,
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          Accept: 'application/json'
        })
      }
    ).subscribe((data: GuessResult) => this.handleResult(data));
  }

  private handleResult(result: GuessResult) {
    this.showResult(result);
  }

  private showResult(result: GuessResult): void {
    if (result === GuessResult.Hit) {
      this.result = 'Hit!';
    } else if (result === GuessResult.Miss) {
      this.result = 'Missed';
    } else if (result === GuessResult.Sink) {
      this.result = 'Ship is sunk!';
    }
  }
}
