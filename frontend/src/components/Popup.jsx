import React from "react";
import "./Popup.css";

export default function Popup({
  open,
  text,
  onClose,
  onConfirm,
  confirmMode = false,
  confirmText = "OK",
  cancelText = "Abbrechen",
}) {
  if (!open) return null;
  return (
    <div className="popup-overlay">
      <div className="popup-box">
        <div className="popup-text">{text}</div>
        <div className="popup-actions">
          {confirmMode ? (
            <>
              <button className="popup-btn" onClick={onConfirm}>
                {confirmText}
              </button>
              <button className="popup-btn" onClick={onClose}>
                {cancelText}
              </button>
            </>
          ) : (
            <button className="popup-btn" onClick={onClose}>
              OK
            </button>
          )}
        </div>
      </div>
    </div>
  );
}
