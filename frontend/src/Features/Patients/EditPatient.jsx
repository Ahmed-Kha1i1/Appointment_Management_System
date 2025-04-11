import Title from "../../Core/Components/comman/Title"
import { DropdownItem } from "../../Core/Components/dropdown/DropdownItem"
import Model from "../../Core/Components/Model/Model"
import AddEditPatientForm from "./AddEditPatientForm"

function EditPatient({id}) {

  return (
    <Model>
      <Model.Open
        opens="EditPatient"
        render={(open) => (
          <DropdownItem onItemClick={() => open()}>
            Details
          </DropdownItem>
        )}
      />
      <Model.Window name="EditPatient">
        <div>
            <Title>
                Edit Patient
            </Title>
            <AddEditPatientForm id={id}/>
        </div>
      </Model.Window>
    </Model>
  )
}

export default EditPatient
