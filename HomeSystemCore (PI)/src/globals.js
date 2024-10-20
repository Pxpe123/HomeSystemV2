const fs = window.require("fs");

let globalData = await loadSettings();

// Load / Save Settings
export async function loadSettings() {
  try {
    let settings = await fs.promises.readFile("./src/settings.json", "utf-8");
    return JSON.parse(settings);
  } catch (error) {
    console.error("Error reading settings:", error);
    throw error;
  }
}

export async function saveSettings() {
  try {
    await fs.promises.writeFile(
      "./src/settings.json",
      JSON.stringify(globalData)
    );
  } catch (error) {
    console.error("Error writing settings:", error);
    throw error;
  }
}

export function getSettings() {
  return globalData;
}

// Notification Settings
export function getNotifications() {
  return globalData.notifications;
}

export function setNotifications(notifications) {
  globalData.notifications = notifications;
}

export function setNotificationsPos(pos) {
  globalData.settings.notifications.NotifyPos = pos;
  saveSettings();
}

export function getNotificationsPos() {
  return globalData.notifications.NotifyPos;
}

export function setNotificationsDND(DND) {
  globalData.notifications.DND = DND;
  saveSettings();
}

export function getNotificationsDND() {
  return globalData.notifications.DND;
}

// Sub App Settings
export function getPrevApp() {
  return globalData.prevApp;
}

export function setPrevApp(prevApp) {
  globalData.prevApp = prevApp;
  saveSettings();
}

export function getCurrentActivity() {
  return globalData.currentActivity;
}

export function setCurrentActivity(currentActivity) {
  globalData.currentActivity = currentActivity;
  saveSettings();
}
