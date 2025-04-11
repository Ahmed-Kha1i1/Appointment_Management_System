
import { DropdownItem } from "../../Core/Components/dropdown/DropdownItem";
import MessagePopup from "../../Core/Components/Model/MessagePopup";
import Model from "../../Core/Components/Model/Model";
import useCancelAppointment from "./Hooks/useCancelAppointment";

function CancelAppointment({id}) {
  const {cancel, isLoading} = useCancelAppointment();
  return (
      <Model>
        <Model.Open
          opens="CancelAppointment"
          render={(open) => (
            <DropdownItem
              onItemClick={() => {open()}}
            >
              Cancel
            </DropdownItem>
          )}
        />
        <Model.Window name="CancelAppointment">
          <MessagePopup
            title="Cancel Application"
            message="Are you sure you want to cancel this application?"
            disabled={isLoading}
            onClose={() => Model.closeWindow()}
            onClick={() => cancel(id,{
              onSettled: () => {
                Model.closeWindow()
              },
            })} />

        </Model.Window>
      </Model>
  );
}

export default CancelAppointment;
