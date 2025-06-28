import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function dateOfBirthValidator(minAge: number): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) return null;

    const birthDate = new Date(control.value);
    const today = new Date();

    if (isNaN(birthDate.getTime())) {
      return { invalidDate: true };
    }

    birthDate.setHours(0, 0, 0, 0);
    today.setHours(0, 0, 0, 0);

    if (birthDate > today) {
      return { futureDate: true };
    }

    let age = today.getFullYear() - birthDate.getFullYear();
    const m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }

    if (age < minAge) {
      return { underAge: true };
    }

    return null;
  };
}
