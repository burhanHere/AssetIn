import {
  AbstractControl,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';

export function formControlValueMatch(targetComponent: AbstractControl): ValidatorFn {
  return (SourceComponent: AbstractControl): ValidationErrors | null => {
    if (!targetComponent || !SourceComponent) {
      return null; // nothing to validate as one or both component are not present
    }
    if (targetComponent.value === SourceComponent.value) {
      return null; // all good
    }
    return { valueMismatch: true }; // values are not a match
  };
}
