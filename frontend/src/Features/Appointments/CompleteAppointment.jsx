import { DropdownItem } from "../../Core/Components/dropdown/DropdownItem";
import MessagePopup from "../../Core/Components/Model/MessagePopup";
import Model from "../../Core/Components/Model/Model";
import useCompleteAppointment from "./Hooks/useCompleteAppointment";

function CompleteAppointment({ id }) {
  const { complete, isLoading } = useCompleteAppointment();
  return (
    <Model>
      <Model.Open
        opens="CompleteAppointment"
        render={(open) => (
          <DropdownItem onItemClick={() => open()}>
            Complete
          </DropdownItem>
        )}
      />
      <Model.Window name="CompleteAppointment">
        <MessagePopup
          title="Complete Appointment"
          message="Are you sure you want to mark this appointment as complete?"
          disabled={isLoading}
          onClose={() => Model.closeWindow()}
          onClick={() =>
            complete(id, {
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

export default CompleteAppointment;
