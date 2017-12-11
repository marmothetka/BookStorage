import { NgModule } from '@angular/core';
import { BooksComponent } from './books.component';

@NgModule({
    bootstrap: [ BooksComponent ],
    imports: [
        BooksComponent
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl }
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
