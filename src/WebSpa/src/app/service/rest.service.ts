import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable()
export class RestService {
  constructor(private http: HttpClient) {}

  // Generic send request method
  public sendRequest<T>(
    verb: string,
    url: string,
    body?: any,
    headers?: HttpHeaders
  ): Observable<T> {
    return this.http
      .request<T>(verb, url, {
        body: body,
        headers: headers
      })
      .pipe(
        catchError((error: Response) => {
          return throwError(`Network Error: ${error.statusText} (${error.status})`);
        })
      );
  }
}
