import {Component, ElementRef, forwardRef, HostListener, Input, OnInit} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from "@angular/forms";

@Component({
  selector: 'app-custom-select',
  templateUrl: './custom-select.component.html',
  styleUrls: ['./custom-select.component.sass'],
  providers: [
    {provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => CustomSelectComponent),
    multi: true
    }
  ]
})
export class CustomSelectComponent implements ControlValueAccessor, OnInit{
  @Input() public options: any[] = [];

  @Input() labelField: string = '';
  @Input() placeholder: string = 'Select';
  @Input() width = '100%';

  public isDropdownOpen = false;

  public selectedValue: any;

  // ControlValueAccessor methods
  onChange: any = () => {};
  onTouched: any = () => {};

  constructor(private elementRef: ElementRef) {}

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: Event): void {
    if (!this.elementRef.nativeElement.contains(event.target)) {
      this.isDropdownOpen = false;
    }
  }

  ngOnInit() {
    this.selectedValue = this.options && this.options.length > 0 ? this.options[0].value : null;
    this.onChange(this.selectedValue);
  }

  writeValue(value: any): void {
    this.selectedValue = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  onModelChange(value: any): void {
    this.selectedValue = value;
    this.onChange(value);
  }

  public toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  public isSelected(option: any): boolean {
    return this.selectedValue === option;
  }
}
