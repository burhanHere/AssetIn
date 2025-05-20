import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function domainSuffixValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    // Check if value matches the domain pattern
    const domainRegex = /^@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$/;

    if (!value || domainRegex.test(value)) {
      return null; // valid
    }

    return { domainSuffixInvalid: true }; // invalid
  };
}
