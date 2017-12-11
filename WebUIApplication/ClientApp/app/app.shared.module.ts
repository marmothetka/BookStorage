import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { BooksComponent } from './components/books/books.component';
import { CounterComponent } from './components/counter/counter.component';
import { AddDataComponent } from './components/books/adddata/adddata.component'

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        BooksComponent,
        AddDataComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'books', component: BooksComponent },
            { path: 'add-book', component: AddDataComponent},
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
