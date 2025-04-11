import { DropdownItem } from "../../Core/Components/dropdown/DropdownItem";
import Model from "../../Core/Components/Model/Model";
import AddEditAppointment from "./AddEditAppointment";

function AddEditAppointmentModel({id}) {
    // const isEdit = true;
    return (
    <Model>
      <Model.Open
        opens="AddEditAppointment"
        render={(open) => (
          <DropdownItem onItemClick={() => open()}>
            Details
          </DropdownItem>
        )}
      />
      <Model.Window name="AddEditAppointment">
        <AddEditAppointment id={id} />
      </Model.Window>
    </Model>
  );
}

export default AddEditAppointmentModel
