const axios = require("axios");

const url = "http://localhost:30151/Logging";
const data = {
  GetData: "DerailValley",
};

axios
  .put(url, data)
  .then((response) => {
    console.log("Response status:", response.status);
    console.log("Response data:", response.data);
  })
  .catch((error) => {
    if (error.response) {
      console.log("Error status:", error.response.status);
      console.log("Error data:", error.response.data);
    } else {
      console.log("Error:", error.message);
    }
  });
