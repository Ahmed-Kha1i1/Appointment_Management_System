import { DropdownItem } from "../../Core/Components/dropdown/DropdownItem";
import MessagePopup from "../../Core/Components/Model/MessagePopup";
import Model from "../../Core/Components/Model/Model";
import useConfirmAppointment from "./Hooks/useConfirmAppointment";

function ConfirmAppointment({ id }) {
  const { confirm, isLoading } = useConfirmAppointment();
    const messageContent = (
    <div className="space-y-2">
      <p>Are you sure you want to confirm this appointment?</p>
      <p className="text-sm text-gray-500 italic">
        Note: Assuming you have contacted the patient.
      </p>
    </div>
  );

  return (
    <Model>
      <Model.Open
        opens="ConfirmAppointment"
        render={(open) => (
          <DropdownItem onItemClick={() => open()}>
            Confirm
          </DropdownItem>
        )}
      />
      <Model.Window name="ConfirmAppointment">
        <MessagePopup
          title="Confirm Appointment"
          message= {messageContent  }
          disabled={isLoading}
          onClose={() => Model.closeWindow()}
          onClick={() =>
            confirm(id, {
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

export default ConfirmAppointment;
