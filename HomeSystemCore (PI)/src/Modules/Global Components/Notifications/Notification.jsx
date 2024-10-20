import React from "react";
import "./Notification.css";

import * as Globals from "./../../../globals";

const Notification = () => {
  const getSettings = Globals.getSettings;
  const settings = getSettings();
  let notifyPos = settings.settings.notifications.NotifyPos;

  if (notifyPos == "") {
    notifyPos = "TopRight";
    Globals.setNotificationsPos(notifyPos);
  }

  const positionClass = `Notify-${notifyPos}`;

  let DND = settings.notifications.DND;
  if (DND) {
    return;
  } else {
    return (
      <div className="notification-Container">
        <div className={`notification-Main ${positionClass}`}></div>
      </div>
    );
  }
};

export default Notification;
