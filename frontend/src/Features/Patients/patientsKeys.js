const patientDetailTypes = {
  ID: "id",
};

const patientsKeys = {
  all: ["patients"],
  lists: () => [...patientsKeys.all, "list"],
  list: (filter) => [...patientsKeys.lists(), { filter }],
  details: () => [...patientsKeys.all, "detail"],
  detail: (type, value) => [...patientsKeys.details(), { type, value }],
};

export { patientsKeys, patientDetailTypes };
