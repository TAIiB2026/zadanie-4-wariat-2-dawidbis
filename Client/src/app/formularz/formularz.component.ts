import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription, Observable } from 'rxjs';
import { FORM_SUBMIT_TOKEN } from '../tokens/form-submit.token';
import { GET_DATA_TOKEN } from '../tokens/get-data.token';

@Component({
  selector: 'taiib2-formularz',
  standalone: false,
  templateUrl: './formularz.component.html',
  styles: ``
})
export class FormularzComponent implements OnInit, OnDestroy {
  private readonly router = inject(Router);
  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly getDataService = inject(GET_DATA_TOKEN);
  private readonly submitService = inject(FORM_SUBMIT_TOKEN);

  public tytul: string = '';
  public cena: number = 0;
  public data!: string;
  public id?: number;
  public wczytywanie = true;

  private sub: Subscription = new Subscription();

  constructor() { }

  ngOnInit(): void {
    this.sub = this.activatedRoute.params.subscribe(params => {
      const idParam = params['id'];
      
      if (idParam != null) {
        const idNumber = parseInt(idParam, 10); 
        
        if (!isNaN(idNumber)) {
          this.id = idNumber;

          this.getDataService.GetByID(idNumber).subscribe({
            next: res => {
                this.tytul = res.tytul;
                this.cena = res.cena;
                
                const dataWydania = new Date(res.dataWydania);
                this.ustawDate(dataWydania);
                
                this.wczytywanie = false;
              }, 
              error: (err) => {
                console.error(err);
                alert("Wystąpił błąd podczas pobierania obiektu.");
                this.wczytywanie = false;
              }
            });
        } else {
          this.ustawDate(new Date());
          this.wczytywanie = false;
        }
      } else {
        this.ustawDate(new Date());
        this.wczytywanie = false;
      }
    });
  }

  private ustawDate(data: Date): void {
    this.data = `${data.getFullYear()}-${
      String(data.getMonth() + 1).padStart(2, '0')
    }-${
      String(data.getDate()).padStart(2, '0')
    }`;
  }

  ngOnDestroy(): void {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }

  public onSubmit() {
    this.wczytywanie = true;
    let request: Observable<boolean>;

    const [year, month, day] = this.data.split('-').map(Number);
    const dataWydania: Date = new Date(year, month - 1, day);

    if (this.id != null && this.id > 0) {
      request = this.submitService.Put(this.id, this.tytul, this.cena, dataWydania);
    } else {
      request = this.submitService.Post(this.tytul, this.cena, dataWydania);
    }

    request.subscribe({
      next: (res) => {
        if (res) {
          this.router.navigateByUrl("/ksiazki");
        } else {
          alert("Wystąpił błąd podczas próby zapisu zmian.");
          this.wczytywanie = false;
        }
      }, 
      error: (err) => {
        console.error(err);
        alert("Wystąpił błąd komunikacji z serwerem podczas zapisu.");
        this.wczytywanie = false;
      }
    });
  }
}