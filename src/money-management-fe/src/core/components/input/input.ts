import { Component, CUSTOM_ELEMENTS_SCHEMA, inject, Input } from '@angular/core';
import { ControlValueAccessor, FormBuilder, ReactiveFormsModule } from '@angular/forms'

@Component({
  selector: 'mon-input',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './input.html',
  styleUrl: './input.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class MonInout implements ControlValueAccessor {
  
  private readonly fb = inject(FormBuilder);

  @Input() maxlength?: number;
  @Input() icon?: string;
  @Input() placeholder = '';

  onChange?: () => void;
  onTouched?: () => void;

  control = this.fb.control<string | number>('');

  writeValue(value: string | number): void {
    this.control.setValue(value)
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled)
      this.control.disable();
    else 
      this.control.enable();
  }

}
