import { useState } from "react";

export default function usePopup() {
  const [popup, setPopup] = useState({
    open: false,
    text: "",
    confirmMode: false,
    resolve: null,
  });

  function showAlert(text) {
    return new Promise((resolve) => {
      setPopup({
        open: true,
        text,
        confirmMode: false,
        resolve: () => {
          setPopup((p) => ({ ...p, open: false }));
          resolve();
        },
      });
    });
  }

  function showConfirm(text) {
    return new Promise((resolve) => {
      setPopup({
        open: true,
        text,
        confirmMode: true,
        resolve,
      });
    });
  }

  function handleClose() {
    if (popup.resolve) popup.resolve(false);
    setPopup((p) => ({ ...p, open: false }));
  }

  function handleConfirm() {
    if (popup.resolve) popup.resolve(true);
    setPopup((p) => ({ ...p, open: false }));
  }

  return {
    popup,
    showAlert,
    showConfirm,
    handleClose,
    handleConfirm,
  };
}
