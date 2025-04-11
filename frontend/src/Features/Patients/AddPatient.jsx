import Title from "../../Core/Components/comman/Title";
import { DropdownItem } from "../../Core/Components/dropdown/DropdownItem"
import Model from "../../Core/Components/Model/Model"
import AddEditPatientForm from "./AddEditPatientForm"

function AddPatient() {
  return (
    <Model>
      <Model.Open
        opens="AddPatient"
        render={(open) => (
          <DropdownItem onItemClick={() => open()}>
            Add Patient
          </DropdownItem>
        )}
      />
      <Model.Window name="AddPatient">
        <div>
            <Title>
                Add Patient
            </Title>
            <AddEditPatientForm onEnd={() => {
                Model.closeWindow();
            }}/>
        </div>
      </Model.Window>
    </Model>
  )
}

export default AddPatient
