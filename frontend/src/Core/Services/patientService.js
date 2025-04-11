import { BASE_URL } from "../Settings/Constants";
import fetchData from "./Fetch";

const PATIENTS_BASE_URL = `${BASE_URL}/patients`;

export async function getPatient(patientId) {
  const result = await fetchData(`${PATIENTS_BASE_URL}/${patientId}`, {
    method: "GET",
  });

  return result.data;
}

export async function getPatients({ searchQuery, gender, orderDirection, orderBy,pageNumber,pageSize }) {
  const newgender = gender == null || gender == "All" ? null : Number(gender);
  const result = await fetchData(`${PATIENTS_BASE_URL}/Search`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ searchQuery, gender: newgender , orderDirection, orderBy,pageNumber,pageSize }),
  });

  return result.data;
}

export async function createPatient({ firstName, lastName, email, password,birthDate,gender }) {
  const result = await fetchData(`${PATIENTS_BASE_URL}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ firstName, lastName, email, password,birthDate,gender }),
  });

  return result.data;
}

export async function updatePatient({ patientId, firstName, lastName, email,birthDate,gender }) {
  const result = await fetchData(`${PATIENTS_BASE_URL}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ patientId, firstName, lastName, email,birthDate,gender }),
  });

  return result.data;
}

export async function deletePatient(patientId) {
  const result = await fetchData(`${PATIENTS_BASE_URL}/${patientId}`, {
    method: "DELETE",
  });

  return result.data;
}
