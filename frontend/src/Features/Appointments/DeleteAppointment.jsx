import { DropdownItem } from "../../Core/Components/dropdown/DropdownItem";
import MessagePopup from "../../Core/Components/Model/MessagePopup";
import Model from "../../Core/Components/Model/Model";
import useDeleteAppointment from "./Hooks/useDeleteAppointment";

function DeleteAppointment({ id }) {
  const { deleteAppointment, isLoading } = useDeleteAppointment();
  return (
    <Model>
      <Model.Open
        opens="DeleteAppointment"
        render={(open) => (
          <DropdownItem onItemClick={() => open()}>
            Delete
          </DropdownItem>
        )}
      />
      <Model.Window name="DeleteAppointment">
        <MessagePopup
          title="Delete Appointment"
          buttonTitle="Delete"
          message="Are you sure you want to delete this appointment?"
          disabled={isLoading}
          onClose={() => Model.closeWindow()}
          onClick={() =>
            deleteAppointment(id, {
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

export default DeleteAppointment;
