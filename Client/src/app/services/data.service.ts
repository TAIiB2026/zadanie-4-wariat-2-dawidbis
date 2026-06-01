import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl = 'http://localhost:5106/api/data'; 

  constructor(private http: HttpClient) { }

  public Get(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  public GetByID(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
  
  public Post(tytul: string, cena: number, data: Date): Observable<boolean> {
    const payload = { 
      tytul: tytul, 
      cena: cena, 
      dataWydania: data 
    };

    return this.http.post(this.apiUrl, payload).pipe(
      map(() => true),             
      catchError(() => of(false))  
    );
  }

  public Put(id: number, tytul: string, cena: number, data: Date): Observable<boolean> {
    const payload = { 
      id: id, 
      tytul: tytul, 
      cena: cena, 
      dataWydania: data 
    };

    return this.http.put(this.apiUrl, payload).pipe(
      map(() => true),
      catchError(() => of(false))
    );
  }
}