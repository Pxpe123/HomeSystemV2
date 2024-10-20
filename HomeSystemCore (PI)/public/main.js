const { app, BrowserWindow } = require("electron");
const path = require("path");

DevMode = true;

function createWindow() {
  const mainWindow = new BrowserWindow({
    width: 960,
    height: 540,
    show: false,
    webPreferences: {
      nodeIntegration: true,
      enableRemoteModule: true,
      contextIsolation: false,
      nodeIntegrationInWorker: true,
      nodeIntegrationInSubFrames: true,
    },
    fullscreen: false,
    alwaysOnTop: true,
    autoHideMenuBar: true,
    resizable: false,
    frame: DevMode, // Adjust based on your debugging needs
  });

  if (DevMode) {
    mainWindow.loadURL("http://localhost:3000");
    mainWindow.webContents.openDevTools();
  } else {
    mainWindow.loadFile(path.join(__dirname, "./build/index.html"));
  }

  mainWindow.once("ready-to-show", () => {
    mainWindow.show();
  });

  mainWindow.setPosition(0, 250);
}

app.on("ready", createWindow);

app.on("window-all-closed", () => {
  if (process.platform !== "darwin") {
    app.quit();
  }
});

app.on("activate", () => {
  if (BrowserWindow.getAllWindows().length === 0) {
    createWindow();
  }
});
