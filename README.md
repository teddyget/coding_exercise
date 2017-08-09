# code_practice

## Description

C# console application to perform stock analysis based on data retrieved from Quandl's historical stock data api.
-  Data being analyzed
    -- Start Date: 2017-01-01
    -- End Date: 2017-06-30
    -- Stocks: [COF, GOOGL, MSFT]
-  Types of analysis
    -- Monthly Average Open and Close price
    -- Max Profit date for each stock if bought at low and sold at highest price

##  How to run application
- Provide your Quandl api_key in "apiKey" string variable found in "AnalysisApp.cs" class
- Open    __code_practice/StockAnalysis/StockAnalysis.sln__   file using VisualStudio
- build application (might require to modify proxy or source to get nuget packages)
- Once successfully compiled the application, you can start the app throught VisualStudio or navigate
to   __code_practice\StockAnalysis\StockAnalysis\bin\Debug__ and run the __"StockAnalysis.exe"__ 
- When the console up starts up, choose from the options to view analysis result

##  Sample Results
#### Monthly Average Open and Close Price
```sh
{
  "COF": [
    {
      "month": "2017-01",
      "average_open": 88.84,
      "average_cLose": 89.58
    }
  ],
  "MSFT": [
    {
      "month": "2017-01",
      "average_open": 62.64,
      "average_cLose": 62.44
    }
  ]
}
```

#### Max profit date
```sh
{
  "COF": {
    "Date": "2017-01-03",
    "low": 87.79,
    "high": 89.6,
    "max_profit": 1.81
  },
  "MSFT": {
    "Date": "2017-01-03",
    "low": 62.125,
    "high": 62.84,
    "max_profit": 0.72
  }
}
```
## Environment
Built and tested on VisualStuio 2015
