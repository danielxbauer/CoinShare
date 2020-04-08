import { Directive, DoCheck, ElementRef } from '@angular/core';
import { NgModel } from '@angular/forms';

@Directive({
    selector: '[ngModel][appInputValidation]',
})
export class InputValidationDirective implements DoCheck {
    constructor(
        private element: ElementRef,
        private model: NgModel
    ) {
        this.element.nativeElement.classList.add('form-control');
        this.element.nativeElement.classList.add('border-2');
    }

    ngDoCheck(): void {
        const invalid = this.model.invalid && (this.model.dirty || this.model.touched);

        if (invalid) {
            this.element.nativeElement.classList.add('invalid');
        } else {
            this.element.nativeElement.classList.remove('invalid');
        }
    }
}
