import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Book } from '.\books'

@Component({
    selector: 'books',
    templateUrl: './books.component.html'
})


export class AddData {
    public book: Book;
    private _http: Http;
    private _baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this._http = http;
        this._baseUrl = baseUrl;
    };

    public saveBook(body: Book) {
        this._http.post(this._baseUrl + 'api/Books', body).subscribe(result => {
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