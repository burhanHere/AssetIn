import {
  AbstractControl,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';

export function ageGreaterThan18Validator(minimumAge: number): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) {
      return null; // Don't validate if no value is provided
    }

    const birthDate = new Date(control.value);
    const today = new Date();

    // Check if the date is valid
    if (isNaN(birthDate.getTime())) {
      return { invalidDate: true };
    }

    // Check if birth date is in the future
    if (birthDate > today) {
      return { underAge: true };
    }

    // Calculate age
    let age = today.getFullYear() - birthDate.getFullYear();
    const monthDiff = today.getMonth() - birthDate.getMonth();

    // Adjust age if birthday hasn't occurred this year
    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }

    // Check if age is less than minimumAge
    if (age <= minimumAge) {
      return { underAge: true };
    }

    return null; // Age is 18 or above
  };
}
