import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function dateOfBirthValidator(minAge: number): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) return null;

    const birthDate = new Date(control.value);
    const today = new Date();

    if (isNaN(birthDate.getTime())) {
      return { invalidDate: true };
    }

    // Normalize time to prevent timezone issues
    birthDate.setHours(0, 0, 0, 0);
    today.setHours(0, 0, 0, 0);

    // ✅ 1. Return if future
    if (birthDate > today) {
      return { futureDate: true };
    }

    // ✅ 2. Check underage only if not in future
    let age = today.getFullYear() - birthDate.getFullYear();
    const m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }

    if (age < minAge) {
      return { underAge: true };
    }

    return null; // ✅ Valid
  };
}
