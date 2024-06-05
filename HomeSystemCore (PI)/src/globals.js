let globalData = {
  notifications: {},
  prevApp: null,
  currentActivity: null,
  settings: {},
};

export function getNotifications() {
  return globalData.notifications;
}

export function setNotifications(notifications) {
  globalData.notifications = notifications;
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
