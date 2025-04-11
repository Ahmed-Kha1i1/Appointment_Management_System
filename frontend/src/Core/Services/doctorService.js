import { BASE_URL } from "../Settings/Constants";
import fetchData from "./Fetch";

const DOCTORS_BASE_URL = `${BASE_URL}/doctors`;

export async function getDoctor(doctorId) {
  const result = await fetchData(`${DOCTORS_BASE_URL}/${doctorId}`, {
    method: "GET",
  });

  return result.data;
}
export async function getDoctors({ searchQuery, orderDirection,specializationId, orderBy,pageNumber,pageSize }) {
  const result = await fetchData(`${DOCTORS_BASE_URL}/Search`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ searchQuery,  orderDirection,specializationId, orderBy,pageNumber,pageSize }),
  });

  return result.data;
}


export async function createDoctor({
  firstName,
  lastName,
  email,
  password,
  specializationId,
}) {
  const result = await fetchData(`${DOCTORS_BASE_URL}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      firstName,
      lastName,
      email,
      password,
      specializationId,
    }),
  });

  return result.data;
}

export async function updateDoctor({
  doctorId,
  firstName,
  lastName,
  email,
  specializationId,
}) {
  const result = await fetchData(`${DOCTORS_BASE_URL}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      doctorId,
      firstName,
      lastName,
      email,
      specializationId,
    }),
  });

  return result.data;
}

export async function deleteDoctor(doctorId) {
  const result = await fetchData(`${DOCTORS_BASE_URL}/${doctorId}`, {
    method: "DELETE",
  });

  return result.data;
}
