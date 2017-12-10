import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Book } from '.\books'

@Component({
    selector: 'books',
    templateUrl: './books.component.html'
})


export class AddData {
    public book: Book;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Books').subscribe(result => {
            this.book = result.json() as Book;
        }, error => console.error(error));
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