Aleksander Kashchonok
# Musement
City Weather forecast CLI application

Application works with 2 endpoints:  http://api.weatherapi.com and  https://api.musement.com
Application works only with one command "cities".
Application gets the list of the cities from TUI Musement's API and provide weather forecast for every city in the catalog for two days

#Note Before start to use application
Create your own account in the weatherapi.com to get a key and fill it in to AppSettings.json
Application has two AppSettings config files: 
1. for application, 
2. for UnitTests 

#Architecture
Application has two services
1. ICityService gets list of cities from https://api.musement.com
2. IWeatherForecastService get weather forecast by city

ICommandModule - registration of CLI commands and actions.
IComponentBuilder build components use for gets all information from the services
IComponentResultBuilder build results from components
IPrintResult print results in console