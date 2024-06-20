let globalData = {
  notifications: {},
  prevApp: null,
  currentActivity: null,
  settings: {
    notifications: {
      DND: false,
      NotifyPos: "TopRight",
    },
  },
};

export function getNotifications() {
  return globalData.notifications;
}

export function setNotifications(notifications) {
  globalData.notifications = notifications;
}

export function setNotificationsPos(pos) {
  globalData.notifications.NotifyPos = pos;
}

export function getNotificationsPos() {
  return globalData.notifications.NotifyPos;
}

export function setNotificationsDND(DND) {
  globalData.notifications.DND = DND;
}

export function getNotificationsDND() {
  return globalData.notifications.DND;
}

export function getPrevApp() {
  return globalData.prevApp;
}

export function setPrevApp(prevApp) {
  globalData.prevApp = prevApp;
}

export function getCurrentActivity() {
  return globalData.currentActivity;
}

export function setCurrentActivity(activity) {
  globalData.currentActivity = activity;
}

export function getSettings() {
  return globalData.settings;
}

export function updateSettings(newSettings) {
  globalData.settings = { ...globalData.settings, ...newSettings };
}
