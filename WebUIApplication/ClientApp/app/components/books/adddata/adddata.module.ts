import { NgModule } from '@angular/core';
import { AddDataComponent } from './adddata.component';

@NgModule({
    bootstrap: [ AddDataComponent ],
    imports: [
        AddDataComponent
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl }
    ]
})
export class AddDataModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
