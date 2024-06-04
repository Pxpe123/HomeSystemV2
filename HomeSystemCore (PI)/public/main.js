const { app, BrowserWindow } = require("electron");
const path = require("path");

let DevMode = true;

function createWindow() {
  const mainWindow = new BrowserWindow({
    width: 960,
    height: 540,
    show: false,
    webPreferences: {
      nodeIntegration: true,
      contextIsolation: false,
    },
    fullscreen: false,
    alwaysOnTop: true,
    autoHideMenuBar: true,
    resizable: false,
    frame: DevMode, // Allow frame in dev mode for debugging
  });

  if (DevMode) {
    mainWindow.loadURL("http://localhost:3000");
    mainWindow.webContents.openDevTools();
  } else {
    mainWindow.loadFile("./build/index.html");
  }

  mainWindow.once("ready-to-show", () => {
    mainWindow.show();
  });
  mainWindow.setPosition(0, 250);
}

app.whenReady().then(() => {
  createWindow();

  app.on("activate", function () {
    if (BrowserWindow.getAllWindows().length === 0) createWindow();
  });
});

app.on("window-all-closed", function () {
  if (process.platform !== "darwin") app.quit();
});
