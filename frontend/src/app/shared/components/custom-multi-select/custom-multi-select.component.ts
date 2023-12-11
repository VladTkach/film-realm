import {Component, ElementRef, forwardRef, HostListener, Input, Self, ViewChild} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from "@angular/forms";

@Component({
  selector: 'app-custom-multi-select',
  templateUrl: './custom-multi-select.component.html',
  styleUrls: ['./custom-multi-select.component.sass'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CustomMultiSelectComponent),
      multi: true,
    }
  ]
})
export class CustomMultiSelectComponent implements ControlValueAccessor{
  selectedItems: any[] = [];

  @Input() options: any[] = [];
  @Input() labelField: string = '';
  @Input() placeholder: string = 'Select';
  @Input() width = '100%';

  public isDropdownOpen = false;

  constructor(private elementRef: ElementRef) {}

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: Event): void {
    if (!this.elementRef.nativeElement.contains(event.target)) {
      this.isDropdownOpen = false;
    }
  }

  public onChange: any = () => {};
  public onTouched: any = () => {};

  public writeValue(value: any): void {
    if (value) {
      this.selectedItems = value;
    }
  }

  public registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  public toggleSelection(option: any): void {
    if (this.isSelected(option)) {
      this.selectedItems = this.selectedItems.filter((item) => item !== option);
    } else {
      this.selectedItems = [...this.selectedItems, option];
    }
    this.onChange(this.selectedItems);
    this.onTouched();
  }

  public isSelected(option: any): boolean {
    return this.selectedItems.includes(option);
  }

  public toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
}
