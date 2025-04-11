import { Fragment } from "react"
import Time from "./Time"
function Times() {
    return [...Array.from({ length: 12 }, (_, index) => (
    <Fragment key={`${index}-AM`}>
        <Time time={index === 0 ? "12AM" : `${index}AM`} />
        <div></div>
    </Fragment>
    )), ...Array.from({ length: 12 }, (_, index) => (
    <Fragment key={`${index}-PM`}>
        <Time time={index === 0 ? "12PM" : `${index}PM`} />
        <div></div>
    </Fragment>
    ))]
    
}

export default Times
