import Title from "../../Core/Components/comman/Title";
import { DropdownItem } from "../../Core/Components/dropdown/DropdownItem";
import Model from "../../Core/Components/Model/Model";
import AddEditDoctorForm from "./AddEditDoctorForm";

function AddDoctor() {
    return (
        <Model>
          <Model.Open
            opens="AddDoctor"
            render={(open) => (
              <DropdownItem onItemClick={() => open()}>
                Add Doctor
              </DropdownItem>
            )}
          />
          <Model.Window name="AddDoctor">
            <div>
                <Title>
                    Add Doctor
                </Title>
                <AddEditDoctorForm onEnd={() => {
                    Model.closeWindow();
                }}/>
            </div>
          </Model.Window>
        </Model>
      )
    }
export default AddDoctor
