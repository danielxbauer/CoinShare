import { Component, ContentChild, TemplateRef } from '@angular/core';

@Component({
    selector: 'app-card',
    templateUrl: './card.component.html'
})
export class CardComponent {
    @ContentChild('cardHeader', { static: false })
    headerTemplate: TemplateRef<any>;

    @ContentChild('cardBody', { static: false })
    bodyTemplate: TemplateRef<any>;
}
