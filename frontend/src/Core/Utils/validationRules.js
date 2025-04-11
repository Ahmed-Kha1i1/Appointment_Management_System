import {
  isNumber,
  validateEmail,
  validateName,
  validatePassword,
} from "./validatorUtils";

function setMaxLength(max, field) {
  return {
    value: max,
    message: `The length of ${field} must be ${max} characters or fewer.`,
  };
}

function setMinLength(max, field) {
  return {
    value: max,
    message: `The length of ${field} must be ${max} characters or fewer.`,
  };
}

export function validateIdRule(field) {
  return {
    required: `${field} is required`,
    validate: (value) => isNumber(value) || `${field} must be number`,
    min: {
      value: 1,
      message: `${field} is inValid. ${field} must be greater than 0.`,
    },
  };
}

export function required(field) {
  const rule = {
    required: `${field} is required`,
  };
  return rule;
}

export function validateNameRule(field, required = true) {
  const rule = {
    maxLength: setMaxLength(50, `${field}`),
    validate: (value) =>
      !value ||
      validateName(value.trim()) ||
      `${field} can only contain letters, numbers, and hyphens`,
  };

  if (required) rule.required = `${field} is required`;
  return rule;
}

export function validatePasswordRule(field) {
  return {
    required: `${field} is required`,
    maxLength: setMaxLength(20, field),
    minLength: setMinLength(8, field),  
    validate: (value) =>
      validatePassword(value.trim()) ||
      `${field} must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character`,
  };
}


export function validateEmailRule() {
  return {
    required: "Email is required",
    validate: (value) => validateEmail(value.trim()) || "Email is not valid",
  };
}
