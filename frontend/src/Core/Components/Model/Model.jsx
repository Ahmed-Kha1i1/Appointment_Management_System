
import {
  cloneElement,
  createContext,
  useContext,
  useRef,
  useState,
} from "react";
import { HiXMark } from "react-icons/hi2";

import Button from "./Button";
import Popup from "./Popup";

const ModelContext = createContext();
let ModelFunctions = {};
function Model({ children }) {
  const [ModelName, setModelName] = useState("");
  const close = () => setModelName("");
  const open = setModelName;
  ModelFunctions = { close, open };

  return (
    <ModelContext.Provider
      value={{
        ModelName,
        close,
        open,
      }}
    >
      {children}
    </ModelContext.Provider>
  );
}

function Open({ opens: opensWindowName, render }) {
  const { open } = useContext(ModelContext);

  return render(() => open(opensWindowName));
}

function Window({ children, name, className }) {
  const { close, ModelName } = useContext(ModelContext);
  const ref = useRef();

  function handleClose(e) {
    if (e.target == ref.current) {
      close();
    }
  }
  if (name !== ModelName) return null;

  return (
    <Popup referance={ref} className={className} onClick={handleClose}>
      <Button onClick={close} icon={<HiXMark />} styles="ml-auto text-black" width="fit" />
      <div>{cloneElement(children, { onCloseModel: close })}</div>
    </Popup>
  );
}

Model.closeWindow = () => ModelFunctions.close();
Model.openWindow = (name) => ModelFunctions.open(name);

Model.Open = Open;
Model.Window = Window;
export default Model;
