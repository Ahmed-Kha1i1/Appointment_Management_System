import { BASE_URL } from "../Settings/Constants";
import fetchData from "./Fetch";

const AUTH_BASE_URL = `${BASE_URL}/auth`;

export async function login({ email, password,roleId,recaptcha }) {
  const result = await fetchData(`${AUTH_BASE_URL}/Login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, password,roleId,recaptcha }),
  });

  return result.data;
}

export async function getDetails() {
  const result = await fetchData(`${AUTH_BASE_URL}`, {
    method: "Get",
    headers: {
      "Content-Type": "application/json",
    }
  });

  return result.data;
}


export async function revokeToken() {
  const result = await fetchData(`${AUTH_BASE_URL}/RevokeToken`, {
    method: "POST",
  });

  return result.data;
}
