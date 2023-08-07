# Air Quality

A simple .NET 7 Web App that shows Air Quality data for the desired location. Data comes from the [OpenAQ](https://openaq.org/) API. Localization can be chosen from the [Google Places API autocomplete](https://developers.google.com/maps/documentation/places/web-service/autocomplete) dropdown.

![ezgif com-crop (1)](https://github.com/smutekDamian/AirQuality/assets/23149907/5a37fb24-f802-4146-8ec3-95397332bbbc)

# Getting started
## Prerequisites
- .NET 7 runtime
- Google Places API key - needs to be inserted into the [appsettings.json](https://github.com/smutekDamian/AirQuality/blob/master/SmutekDev.AirQuality.Web/appsettings.json) file, in the _GooglePlacesApiKey_ section.

## Installation

1. Clone or download the repository
2. Go to the Web project folder - _SmutekDev.AirQuality.Web_
3. Add Google Places API key to appsettings.json file
4. Open the terminal and execute the command:
   
    ```sh
    dotnet run
    ```
5. Go to http://localhost:5041/ or https://localhost:7079 in the browser

![image](https://github.com/smutekDamian/AirQuality/assets/23149907/02f22a78-cafc-42a8-9192-f17d7f67c966)

