import Model from "../../Core/Components/Model/Model";
import  useDeletePatient from "./Hooks/useDeletePatient";
import MessagePopup from "../../Core/Components/Model/MessagePopup";
import {DropdownItem} from "../../Core/Components/dropdown/DropdownItem"

function DeletePatient({id}) {
  const { isLoading,  deletePatient } = useDeletePatient();
  return (
    <Model>
      <Model.Open
        opens="DeletePatient"
        render={(open) => (
          <DropdownItem onItemClick={() => open()}>
            Delete
          </DropdownItem>
        )}
      />
      <Model.Window name="DeletePatient">
        <MessagePopup
          title="Delete Patient"
          buttonTitle="Delete"
          message="Are you sure you want to delete this patient?"
          disabled={isLoading}
          onClose={() => Model.closeWindow()}
          onClick={() =>
            deletePatient(id, {
              onSettled: () => {
                Model.closeWindow();
              },
            })
          }
        />
      </Model.Window>
    </Model>
  );
}

export default DeletePatient
