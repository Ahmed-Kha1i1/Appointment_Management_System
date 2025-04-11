const doctorDetailTypes = {
  ID: "id",
};

const doctorsKeys = {
  all: ["doctors"],
  lists: () => [...doctorsKeys.all, "list"],
  listSpecializations: () => [...doctorsKeys.lists(), { type: "specializations" }],
  listSpecializationsPref: () => [...doctorsKeys.lists(), { type: "specializationsPref" }],
  list: (filter) => [...doctorsKeys.lists(), { filter }],
  details: () => [...doctorsKeys.all, "detail"],
  detail: (type, value) => [...doctorsKeys.details(), { type, value }],
};

export { doctorsKeys, doctorDetailTypes };
