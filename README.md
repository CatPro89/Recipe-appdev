# RecipeApp

A Xamarin.Forms recipe app (currently only for Android) that uses Entity Framework Core to access an SQLite database.

[![License](https://img.shields.io/github/license/hocnat/RecipeApp)](https://img.shields.io/github/license/hocnat/RecipeApp)

## Motivation

The idea of this project was to experiment with Xamarin.Forms. At the same time it was a good opportunity to get to know Entity Framework Core better.

## Features

* Add all your favourite recipes
* Edit your recipes
* Search for a specific recipe in your list of recipes
* Let the app calculate ingredients for different numbers of servings
* Use in English or German

## Screenshots

...

## Getting started

### Clone

Open a command line.

```shell
git clone https://github.com/hocnat/RecipeApp.git
```

### Build

Open Solution in Visual Studio.

Build | Rebuild Solution

### Deploy

> Make sure the Android Emulator is [configured](https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/android-emulator/) correctly or connect an Android device.

Right click on RecipeApp.Android | Set as Startup Project

Build | Deploy Solution

### Start

Open app `Recipes`

## Built with

* [Xamarin.Forms](https://www.nuget.org/packages/Xamarin.Forms/4.7.0.1080) - Microsoft - [MIT](https://licenses.nuget.org/MIT)
* [Xamarin.Essentials](https://www.nuget.org/packages/Xamarin.Essentials/1.5.3.2) - Microsoft - [MIT](https://www.nuget.org/packages/Xamarin.Essentials/1.5.3.2/License)
* [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/3.1.5) - Microsoft - [Apache-2.0](https://licenses.nuget.org/Apache-2.0)
* [Xam.Media.Plugin](https://www.nuget.org/packages/Xam.Plugin.Media/5.0.1) - James Montemagno - [MIT](https://github.com/jamesmontemagno/MediaPlugin/blob/master/LICENSE)

## License

[MIT License](https://github.com/hocnat/RecipeApp/blob/master/LICENSE) Copyright 2020 © [hocnat](https://github.com/hocnat)
