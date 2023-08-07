# Air Quality

A simple .NET 7 Web App that shows Air Quality data for the desired location. Data comes from the [OpenAQ](https://openaq.org/) API. Localization can be chosen from the [Google Places API autocomplete](https://developers.google.com/maps/documentation/places/web-service/autocomplete) dropdown.

![ezgif com-crop (1)](https://github.com/smutekDamian/AirQuality/assets/23149907/31e7dad4-7c75-4c47-adf0-cdfde6469e55)

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
5. Go to http://localhost:5041/ in the browser

![image](https://github.com/smutekDamian/AirQuality/assets/23149907/6dfb8638-ad23-48b0-81a8-679c5beac655)
