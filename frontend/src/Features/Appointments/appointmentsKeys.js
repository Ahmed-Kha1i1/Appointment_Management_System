const appointmentDetailTypes = {
  ID: "id",
};

const appointmentsKeys = {
  all: ["appointments"],
  lists: () => [...appointmentsKeys.all, "list"],
  list: (filter) => [...appointmentsKeys.lists(), { filter }],
  details: () => [...appointmentsKeys.all, "detail"],
  detail: (type, value) => [...appointmentsKeys.details(), { type, value }],
};

export { appointmentsKeys, appointmentDetailTypes };
