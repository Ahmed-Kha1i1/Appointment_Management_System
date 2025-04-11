import Model from "../../Core/Components/Model/Model";
import useDeleteDoctor  from "./Hooks/useDeleteDoctor"
import MessagePopup from "../../Core/Components/Model/MessagePopup";
import {DropdownItem} from "../../Core/Components/dropdown/DropdownItem"
function DeleteDoctor({id}) {
  const { isLoading,  deleteDoctor } = useDeleteDoctor();
  return (
    <Model>
      <Model.Open
        opens="DeleteDoctor"
        render={(open) => (
          <DropdownItem onItemClick={() => open()}>
            Delete
          </DropdownItem>
        )}
      />
      <Model.Window name="DeleteDoctor">
        <MessagePopup
          title="Delete Doctor"
          buttonTitle="Delete"
          message="Are you sure you want to delete this doctor?"
          disabled={isLoading}
          onClose= {() => Model.closeWindow()}
          onClick={() =>
            deleteDoctor(id, {
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

export default DeleteDoctor
