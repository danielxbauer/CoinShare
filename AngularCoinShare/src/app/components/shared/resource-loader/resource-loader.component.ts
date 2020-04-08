import { Component, Input, TemplateRef, ContentChild } from '@angular/core';
import { ApiResource, empty } from 'src/app/models';

@Component({
    selector: 'app-resource-loader',
    templateUrl: './resource-loader.component.html'
})
export class ResourceLoaderComponent<T> {
    @Input() resource: ApiResource<T> = empty();

    @ContentChild('data', { static: false })
    dataTemplate: TemplateRef<any>;

    get errorMessage() {
        return this.resource.kind === 'Error'
            ? this.resource.message
            : '';
    }

    get data() {
        return this.resource.kind === 'Data'
            ? this.resource.item
            : null;
    }
}
