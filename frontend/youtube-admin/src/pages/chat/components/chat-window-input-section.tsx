import { useEffect, useState } from "react";

const ChatWindowInputSection = () => {
  const [messageText, setMessageText] = useState("");

  useEffect(() => {
    const messageInput = document.getElementById(
      "messageInput",
    ) as HTMLTextAreaElement;
    const sendButton = document.getElementById("sendButton") as HTMLDivElement;

    messageInput.addEventListener("input", () => {
      if (messageInput.value !== "" || messageText !== "") {
        sendButton.style.display = "flex";
      } else {
        sendButton.style.display = "none";
      }
    });
  }, [messageText]);

  return (
    <div className="input-section-layout">
      <textarea
        id="messageInput"
        value={messageText}
        onChange={(e) => setMessageText(e.target.value)}
        className="input-section-textarea"
        rows={4}
        maxLength={100}
        placeholder="Сообщение..."
      />
      <div
        className="input-section-send-message-button"
        id="sendButton"
        onClick={() => setMessageText("")}
      >
        <div className="input-section-attachment">
          <svg
            viewBox="0 0 24 24"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
            stroke="#000000"
          >
            <g id="SVGRepo_bgCarrier" strokeWidth="0" />
            <g
              id="SVGRepo_tracerCarrier"
              strokeLinecap="round"
              strokeLinejoin="round"
            />
            <g id="SVGRepo_iconCarrier">
              <path
                d="M12 5V19M12 5L6 11M12 5L18 11"
                stroke="lightgray"
                strokeWidth="1.2"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </g>
          </svg>
        </div>
      </div>
    </div>
  );
};

export default ChatWindowInputSection;