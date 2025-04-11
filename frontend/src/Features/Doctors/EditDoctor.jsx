import Title from "../../Core/Components/comman/Title"
import { DropdownItem } from "../../Core/Components/dropdown/DropdownItem"
import Model from "../../Core/Components/Model/Model"
import AddEditDoctorForm from "./AddEditDoctorForm"

function EditDoctor({id}) {
   
     return (
       <Model>
         <Model.Open
           opens="EditDoctor"
           render={(open) => (
             <DropdownItem onItemClick={() => open()}>
               Details
             </DropdownItem>
           )}
         />
         <Model.Window name="EditDoctor">
           <div>
               <Title>
                   Edit Doctor
               </Title>
               <AddEditDoctorForm id={id}/>
           </div>
         </Model.Window>
       </Model>
     )
   }

export default EditDoctor
