import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'books',
    templateUrl: './books.component.html',
    moduleId: './books.module.ts'
})


export class BooksComponent {
    public books: Book[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Books').subscribe(result => {
            this.books = result.json() as Book[];
        }, error => console.error(error));
    };

    public editBook(id: number) {
        
    }

    public deleteBook(id: number){
        
    }
}

interface Book {
    id: number;
    name: string;
    authors: string;
    year: number;
    pages: number;
    publisher: string;
    isbn: string;
    image: ByteString;
}
