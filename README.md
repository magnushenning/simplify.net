# Simplify.NET

`Simplify.NET` is a high-performance polyline simplification library written in C# for .NET.

## Demo
Visit http://mourner.github.io/simplify-js/ 

## Usage

    using Simplify.NET;
    
    double tolerance    = 5;
    bool highestQuality = false;
    var bigList         = new List<Point>() { new Point(0, 500), new Point(1, 1000), ... };

    var simplifiedList  = SimplifyNet.Simplify(bigList, tolerance, highestQuality);

## Nuget
https://www.nuget.org/packages/Simplify.NET/

## API
 **Namespace**: `Simplify.Net`
 
| Method | Argument 1 | Argument 2 | Argument 3 |
|--|--|--|--|
| SimplifyNet.**Simplify** | **List\<Point\>** points | **double** tolerance *(default: 1)* | **bool** highestQuality *(default: false)* |
|SimplifyNet.**Point** | **double** x | **double** y||


Based on [Simplify.js](https://github.com/mourner/simplify-js) by Vladimir Agafonki