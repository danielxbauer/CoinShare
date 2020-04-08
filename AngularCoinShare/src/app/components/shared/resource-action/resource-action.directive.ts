import { Component, ComponentFactoryResolver, Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { ApiResource, Error } from 'src/app/models';
import { LoadingComponent } from '../loading/loading.component';

@Component({
    selector: 'app-resource-error',
    template: `
        <ng-container *ngTemplateOutlet="dataTemplate">
        </ng-container>
        <p class="text-center text-danger pt-2 m-0">{{ resource.message }}</p>
    `
})
export class ResourceErrorComponent {
    @Input() resource: Error;
    dataTemplate: TemplateRef<any>;
}

@Directive({
    selector: '[appResourceAction]',
})
export class ResourceActionDirective<TData> {
    constructor(
        private componentFactoryResolver: ComponentFactoryResolver,
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef
    ) { }

    @Input() set appResourceAction(resource: ApiResource<TData>) {
        this.viewContainer.clear();

        if (resource.kind === 'Data' || resource.kind === 'Empty') {
            this.viewContainer.createEmbeddedView(this.templateRef);
        }

        if (resource.kind === 'Loading') {
            const component = this.componentFactoryResolver.resolveComponentFactory(LoadingComponent);
            this.viewContainer.createComponent(component);
        }

        if (resource.kind === 'Error') {
            const component = this.componentFactoryResolver.resolveComponentFactory(ResourceErrorComponent);

            const componentRef = this.viewContainer.createComponent(component);
            componentRef.instance.resource = resource;
            componentRef.instance.dataTemplate = this.templateRef;
        }
    }
}
