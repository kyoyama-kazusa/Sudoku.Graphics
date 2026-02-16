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
using Sudoku.Concepts;
using Sudoku.Graphics;
using Sudoku.Graphics.LineTemplates;

var desktop = Environment.DesktopPath;
for (var i = 16; i <= 25; i++)
{
	var mapper = new PointMapper(cellSize: 120, margin: 10, rowsCount: i, columnsCount: i, vector: new(1));
	using var canvas = new Canvas(
		mapper: mapper,
		drawingOptions: new() { GridLineTemplate = new DefaultLineTemplate() },
		exportingOptions: new() { Quality = 100 }
	);
	canvas.FillBackground();
	canvas.DrawLines();
	var rawSize = (int)Math.Sqrt(i);
	var size = rawSize * rawSize == i ? rawSize : rawSize + 1;
	for (var digit = (Relative)0; digit < i; digit++)
	{
		canvas.DrawSmallText((digit + 1).ToString(), (Relative)0, digit, size, SKColors.Gray, SKFontStyleSlant.Upright);
	}
	canvas.Export(Path.Combine(desktop, $"output_size{i.ToString().PadLeft(2, '0')}.png"));
}

Console.WriteLine("Okay.");


/// <summary>
/// Provides main method as entry point of this program.
/// </summary>
file static partial class Program
{
	extension(Environment)
	{
		/// <summary>
		/// Represents desktop path.
		/// </summary>
		public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
	}
}
