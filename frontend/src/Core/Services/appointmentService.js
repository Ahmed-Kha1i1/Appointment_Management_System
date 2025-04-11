import { BASE_URL } from "../Settings/Constants";
import fetchData from "./Fetch";

const APPOINTMENTS_BASE_URL = `${BASE_URL}/appointments`;

export async function getAppointment(appointmentId) {
  const result = await fetchData(`${APPOINTMENTS_BASE_URL}/${appointmentId}`, {
    method: "GET",
  });

  return result.data;
}

export async function getAppointments({startDate, endDate}) {
  const result = await fetchData(`${APPOINTMENTS_BASE_URL}/Search`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ startDate, endDate }),
  });

  return result.data;
}

export async function createAppointment({
  doctorId,
  patientId,
  appointmentDate,
  startTime
}) {
  const result = await fetchData(`${APPOINTMENTS_BASE_URL}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ doctorId, patientId, appointmentDate,startTime }),
  });

  return result.data;
}

export async function createGuestAppointment({
  doctorId,
  emailAddress,
  appointmentDate,
  startTime
}) {
  const result = await fetchData(`${APPOINTMENTS_BASE_URL}/BookAsGuest`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ doctorId, emailAddress, appointmentDate,startTime }),
  });

  return result.data;
}

export async function updateAppointment({
  appointmentId,
  doctorId,
  patientId,
  appointmentDate,
  startTime
}) {
  const result = await fetchData(`${APPOINTMENTS_BASE_URL}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      appointmentId,
      doctorId,
      patientId,
      appointmentDate,
      startTime
    }),
  });

 

  return result.data;
}

export async function deleteAppointment(appointmentId) {
  const result = await fetchData(`${APPOINTMENTS_BASE_URL}/${appointmentId}`, {
    method: "DELETE",
  });

  return result.data;
}

export async function cancelAppointment(appointmentId) {
  const result = await fetchData(
    `${APPOINTMENTS_BASE_URL}/${appointmentId}/cancel`,
    {
      method: "POST",
    }
  );

  return result.data;
}

export async function completeAppointment(appointmentId) {
  const result = await fetchData(
    `${APPOINTMENTS_BASE_URL}/${appointmentId}/complete`,
    {
      method: "POST",
    }
  );

  return result.data;
}

export async function confirmAppointment(appointmentId) {
  const result = await fetchData(
    `${APPOINTMENTS_BASE_URL}/${appointmentId}/confirm`,
    {
      method: "POST",
    }
  );

  return result.data;
}

export async function checkOverlappingAppointment({
  doctorId,
  appointmentDate,
  timeSlotDuration,
}) {
  const result = await fetchData(`${APPOINTMENTS_BASE_URL}/CheckOverlap`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ doctorId, appointmentDate, timeSlotDuration }),
  });

  return result.data;
}
