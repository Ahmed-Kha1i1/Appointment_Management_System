import { FaPlus } from "react-icons/fa"
import Model from "../../../Core/Components/Model/Model"
import AddEditAppointment from "../AddEditAppointment"

function AddAppointment() {
    return <Model>
          <Model.Open
            opens="AddAppointment"
            render={(open) => (
            <button 
                type="button" 
                onClick={() => open()}
                className="ml-6 rounded-md border border-transparent bg-indigo-600 py-2 px-4 text-sm font-medium text-white shadow-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
            >
                <FaPlus className="size-4"/>
            </button>
            )}
          />
          <Model.Window name="AddAppointment">
            <AddEditAppointment onEnd={()=> Model.closeWindow()}/>
          </Model.Window>
        </Model>
}

export default AddAppointment
