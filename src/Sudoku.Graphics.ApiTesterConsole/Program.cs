// This project only tests for APIs.
//
// I don't usually use NUnit or XUnit test-related packages,
// so I just use a console project to test for them.
//
// If you don't want to view for this, you can remove this project,
// or just close this project from solution explorer :) 

using System;
using System.IO;
using SkiaSharp;
using Sudoku.Graphics;

var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
using var canvas = new Canvas(
	cellSize: 120,
	margin: 10,
	rowsCount: 9,
	columnsCount: 9,
	vector: DirectionVector.Zero,
	drawingOptions: new()
	{
		BackgroundColor = SKColors.White,
		ThickLineColor = SKColors.Black,
		GridLineTemplate = new RectangularBlockLineTemplate(3, 3)
	},
	exportingOptions: new() { Quality = 100 }
);
canvas.FillBackground();
canvas.DrawLines();
canvas.Export(Path.Combine(desktop, "output.png"));
Console.WriteLine("Okay.");
