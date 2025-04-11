import { BASE_URL } from "../Settings/Constants";
import fetchData from "./Fetch";

const SPECIALIZATIONS_BASE_URL = `${BASE_URL}/specializations`;

export async function getAllSpecializations() {
  const result = await fetchData(`${SPECIALIZATIONS_BASE_URL}/All`, {
    method: "GET",
  });

  return result.data;
}


export async function getAllDetailsSpecializations() {
  const result = await fetchData(`${SPECIALIZATIONS_BASE_URL}/AllDetails`, {
    method: "GET",
  });

  return result.data;
}
