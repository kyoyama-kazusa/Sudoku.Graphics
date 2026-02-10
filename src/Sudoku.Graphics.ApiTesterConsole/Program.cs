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
using var canvas = new Canvas(120, 10, 9, 9);
canvas.FillBackground(Options);
canvas.DrawLines(Options);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
Console.WriteLine("Okay.");


/// <summary>
/// Provides entry point.
/// </summary>
file static partial class Program
{
	/// <summary>
	/// Represents default options.
	/// </summary>
	private static readonly CanvasDrawingOptions Options = new()
	{
		BackgroundColor = SKColors.White,
		ThickLineColor = SKColors.Black,
		GridLineTemplate = new DefaultGridLineTemplate(9, 9, 3, 3, DirectionVector.Zero)
	};
}
