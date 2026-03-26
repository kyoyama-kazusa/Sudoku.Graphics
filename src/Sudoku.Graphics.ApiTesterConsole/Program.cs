
// This project only tests for APIs.
//
// I don't usually use NUnit or XUnit test-related packages,
// so I just use a console project to test for them.
//
// If you don't want to view for this, you can remove this project,
// or just close this project from solution explorer :)

using System;
using System.IO;
using System.Linq;
using SkiaSharp;
using Sudoku.Graphics;
using Sudoku.Graphics.Items.CellGroupMarks;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Lines;
using Sudoku.Graphics.Items.Texts;
using Sudoku.Graphics.Templates;

var desktop = Environment.DesktopPath;
var options = new CanvasDrawingOptions();
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 6, ColumnsCount = 6 }
};
using var canvas = new Canvas(
	new StandardTemplate(2, 3, mapper)
	{
		ThickLineWidth = options.ThickLineWidth.Resolve(options),
		ThinLineWidth = options.ThinLineWidth.Resolve(options),
		ThickLineColor = options.ThickLineColor.Resolve(options),
		ThinLineColor = options.ThinLineColor.Resolve(options)
	}
);

var rng = Random.Shared;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = options.BackgroundColor.Resolve(options) },
		new TemplateLineItem(),
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 18,
			Text = "1",
			Color = SKColors.Black,
			FontName = "Arial",
			FontSizeScale = .8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 24,
			Text = "2",
			Color = SKColors.Black,
			FontName = "Arial",
			FontSizeScale = .8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 30,
			Text = "3",
			Color = SKColors.Black,
			FontName = "Arial",
			FontSizeScale = .8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 31,
			Text = "4",
			Color = SKColors.Black,
			FontName = "Arial",
			FontSizeScale = .8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 32,
			Text = "5",
			Color = SKColors.Black,
			FontName = "Arial",
			FontSizeScale = .8M
		},
		new CellGroupTrailMarkItem
		{
			TemplateIndex = 0,
			SizeScale = .75M,
			Cells = [0, 1, 2, 8, 7, 6, 0],
			FillColor = SKColors.LightGray
		},
		new CellGroupTrailMarkItem
		{
			TemplateIndex = 0,
			SizeScale = .75M,
			Cells = [10, 11, 17],
			FillColor = SKColors.LightGray
		},
		new CellGroupTrailMarkItem
		{
			TemplateIndex = 0,
			SizeScale = .75M,
			Cells = [12, 13, 19],
			FillColor = SKColors.LightGray
		},
		new CellGroupTrailMarkItem
		{
			TemplateIndex = 0,
			SizeScale = .75M,
			Cells = [27, 28, 29, 35, 34, 33, 27],
			FillColor = SKColors.LightGray
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
