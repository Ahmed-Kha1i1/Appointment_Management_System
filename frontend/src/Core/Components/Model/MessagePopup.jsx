function MessagePopup({title, message, onClick,buttonTitle = "Confirm",onClose, disabled}) {
    return (
        <div>
            <div className="p-4">
            <h1 className="text-xl font-semibold mb-2">{title}</h1>
            <p className="text-lg text-gray-500 " >
                {message}
            </p>
            <div className="flex justify-end mt-4 gap-2">
                <button
                    onClick={onClose}
                    className="bg-gray-500 text-white px-4 py-2 rounded text-lg hover:opacity-80 transition duration-200"
                    disabled={disabled}
                    >
                    Cancel
                </button>
                <button
                    onClick={onClick}
                    className="bg-blue-500 text-white px-4 py-2 rounded text-lg hover:opacity-80 transition duration-200"
                    disabled={disabled}
                    >
                    {buttonTitle}
                </button>
            </div>
        </div>
        </div>
    )
}

export default MessagePopup
