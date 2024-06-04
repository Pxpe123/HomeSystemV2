const fs = require("fs");

const data = [
  {
    Key: "CFF452",
    Value: {
      TrainGUID: "CFF452",
      FrontCoupler: { AirCock: true },
      RearCoupler: { AirCock: false },
      CarType: "FlatbedEmpty",
      Cargo: { CargoType: "SteelBentPlates", CargoWeight: "1" },
    },
  },
  {
    Key: "CFF409",
    Value: {
      TrainGUID: "CFF409",
      FrontCoupler: { AirCock: true },
      RearCoupler: { AirCock: true },
      CarType: "FlatbedEmpty",
      Cargo: { CargoType: "SteelBentPlates", CargoWeight: "1" },
    },
  },
  {
    Key: "CFF100",
    Value: {
      TrainGUID: "CFF100",
      FrontCoupler: { AirCock: true },
      RearCoupler: { AirCock: true },
      CarType: "FlatbedEmpty",
      Cargo: { CargoType: "SteelBentPlates", CargoWeight: "1" },
    },
  },
  {
    Key: "L-007",
    Value: {
      TrainGUID: "L-007",
      FrontCoupler: { AirCock: true },
      RearCoupler: { AirCock: true },
      CarType: "LocoShunter",
      Cargo: { CargoType: "None", CargoWeight: "0" },
    },
  },
  {
    Key: "CFF398",
    Value: {
      TrainGUID: "CFF398",
      FrontCoupler: { AirCock: true },
      RearCoupler: { AirCock: true },
      CarType: "FlatbedEmpty",
      Cargo: { CargoType: "SteelSlabs", CargoWeight: "1" },
    },
  },
  {
    Key: "CFF983",
    Value: {
      TrainGUID: "CFF983",
      FrontCoupler: { AirCock: true },
      RearCoupler: { AirCock: true },
      CarType: "FlatbedEmpty",
      Cargo: { CargoType: "SteelRolls", CargoWeight: "1" },
    },
  },
  {
    Key: "CFF895",
    Value: {
      TrainGUID: "CFF895",
      FrontCoupler: { AirCock: false },
      RearCoupler: { AirCock: true },
      CarType: "FlatbedEmpty",
      Cargo: { CargoType: "SteelRolls", CargoWeight: "1" },
    },
  },
];

const divs = data
  .map((item) => `<div>${item.Value.TrainGUID}</div>`)
  .join(" + ");

const htmlContent = `
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Train Data</title>
<style>
  body {
    font-family: Arial, sans-serif;
    display: flex;
    flex-wrap: wrap;
    gap: 10px;
    justify-content: center;
    align-items: center;
    height: 100vh;
    margin: 0;
    background-color: #f0f0f0;
  }
  div {
    padding: 10px 20px;
    background-color: #fff;
    border: 1px solid #ddd;
    border-radius: 5px;
  }
</style>
</head>
<body>
${divs}
</body>
</html>
`;

fs.writeFileSync("index.html", htmlContent);
console.log("index.html file created successfully!");
