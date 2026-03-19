
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
using Sudoku.Graphics.Items.CellGroupMarks;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Lines;
using Sudoku.Graphics.Templates;

var desktop = Environment.DesktopPath;
var options = new CanvasDrawingOptions();
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 9, ColumnsCount = 9 }
};
using var canvas = new Canvas(
	new StandardTemplate(3, 3, mapper)
	{
		ThickLineWidth = options.ThickLineWidth.Resolve(options),
		ThinLineWidth = options.ThinLineWidth.Resolve(options),
		ThickLineColor = options.ThickLineColor.Resolve(options),
		ThinLineColor = options.ThinLineColor.Resolve(options)
	}
);

var rng = Random.Shared;
const decimal strokeWidthScale = .4M;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = options.BackgroundColor.Resolve(options) },
		new TemplateLineItem(),
		new CellGroupThermometerMarkItem
		{
			Cells = [8, 7, 6],
			TemplateIndex = 0,
			CircleScale = .8M,
			FillColor = SKColors.LightGray,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale,
		},
		new CellGroupThermometerMarkItem
		{
			Cells = [8, 17, 26],
			TemplateIndex = 0,
			CircleScale = .8M,
			FillColor = SKColors.LightGray,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale,
		},
		new CellGroupThermometerMarkItem
		{
			Cells = [31, 22, 21, 20],
			TemplateIndex = 0,
			CircleScale = .8M,
			FillColor = SKColors.LightGray,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale,
		},
		new CellGroupThermometerMarkItem
		{
			Cells = [31, 40, 39, 38, 47, 56, 57, 58],
			TemplateIndex = 0,
			CircleScale = .8M,
			FillColor = SKColors.LightGray,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale,
		},
		new CellGroupThermometerMarkItem
		{
			Cells = [46, 37, 28, 19, 10],
			TemplateIndex = 0,
			CircleScale = .8M,
			FillColor = SKColors.LightGray,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale,
		},
		new CellGroupThermometerMarkItem
		{
			Cells = [72, 63, 54],
			TemplateIndex = 0,
			CircleScale = .8M,
			FillColor = SKColors.LightGray,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale,
		},
		new CellGroupThermometerMarkItem
		{
			Cells = [72, 73, 74, 65],
			TemplateIndex = 0,
			CircleScale = .8M,
			FillColor = SKColors.LightGray,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale,
		},
		new CellGroupThermometerMarkItem
		{
			Cells = [70, 69, 68],
			TemplateIndex = 0,
			CircleScale = .8M,
			FillColor = SKColors.LightGray,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale,
		},
		new CellGroupThermometerMarkItem
		{
			Cells = [70, 61, 52, 51, 50],
			TemplateIndex = 0,
			CircleScale = .8M,
			FillColor = SKColors.LightGray,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale,
		},
		new CellGroupThermometerMarkItem
		{
			Cells = [70, 61, 52, 43, 34, 33, 32],
			TemplateIndex = 0,
			CircleScale = .8M,
			FillColor = SKColors.LightGray,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale,
		},
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
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
