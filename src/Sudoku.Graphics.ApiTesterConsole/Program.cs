
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
const string puzzleString = "..5...8...4.....7.1.......5....5.......3.2.......1....5.......4.6.....8...1...6..";
const float arrowCapRotationDegrees = 25;
const decimal capsuleSizeScale = .75M;
const decimal arrowCapLengthScale = .25M;
const decimal strokeWidthScale = .08M;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = options.BackgroundColor.Resolve(options) },
		new TemplateLineItem(),
		..
		from pair in puzzleString.Index()
		let digitChar = pair.Item
		let cell = pair.Index
		where digitChar != '.'
		select new GivenTextItem
		{
			Cell = cell,
			Text = digitChar.ToString(),
			Color = SKColors.Black,
			FontName = "Cascadia Code",
			FontSizeScale = .8M,
			TemplateIndex = 0
		},
		new CellGroupTrailedCapsuleMarkItem
		{
			TemplateIndex = 0,
			Cells = [4],
			TrailCells = [4, 12, 20, 28],
			HalfArrowCapRotationDegrees = arrowCapRotationDegrees,
			CapsuleSizeScale = capsuleSizeScale,
			ArrowCapLengthScale = arrowCapLengthScale,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale
		},
		new CellGroupTrailedCapsuleMarkItem
		{
			TemplateIndex = 0,
			Cells = [22],
			TrailCells = [22, 30],
			HalfArrowCapRotationDegrees = arrowCapRotationDegrees,
			CapsuleSizeScale = capsuleSizeScale,
			ArrowCapLengthScale = arrowCapLengthScale,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale
		},
		new CellGroupTrailedCapsuleMarkItem
		{
			TemplateIndex = 0,
			Cells = [38],
			TrailCells = [38, 48],
			HalfArrowCapRotationDegrees = arrowCapRotationDegrees,
			CapsuleSizeScale = capsuleSizeScale,
			ArrowCapLengthScale = arrowCapLengthScale,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale
		},
		new CellGroupTrailedCapsuleMarkItem
		{
			TemplateIndex = 0,
			Cells = [42],
			TrailCells = [42, 32],
			HalfArrowCapRotationDegrees = arrowCapRotationDegrees,
			CapsuleSizeScale = capsuleSizeScale,
			ArrowCapLengthScale = arrowCapLengthScale,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale
		},
		new CellGroupTrailedCapsuleMarkItem
		{
			TemplateIndex = 0,
			Cells = [36],
			TrailCells = [36, 46, 56, 66],
			HalfArrowCapRotationDegrees = arrowCapRotationDegrees,
			CapsuleSizeScale = capsuleSizeScale,
			ArrowCapLengthScale = arrowCapLengthScale,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale
		},
		new CellGroupTrailedCapsuleMarkItem
		{
			TemplateIndex = 0,
			Cells = [58],
			TrailCells = [58, 50],
			HalfArrowCapRotationDegrees = arrowCapRotationDegrees,
			CapsuleSizeScale = capsuleSizeScale,
			ArrowCapLengthScale = arrowCapLengthScale,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale
		},
		new CellGroupTrailedCapsuleMarkItem
		{
			TemplateIndex = 0,
			Cells = [76],
			TrailCells = [76, 68, 60, 52],
			HalfArrowCapRotationDegrees = arrowCapRotationDegrees,
			CapsuleSizeScale = capsuleSizeScale,
			ArrowCapLengthScale = arrowCapLengthScale,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale
		},
		new CellGroupTrailedCapsuleMarkItem
		{
			TemplateIndex = 0,
			Cells = [44],
			TrailCells = [44, 34, 24, 14],
			HalfArrowCapRotationDegrees = arrowCapRotationDegrees,
			CapsuleSizeScale = capsuleSizeScale,
			ArrowCapLengthScale = arrowCapLengthScale,
			StrokeColor = SKColors.LightGray,
			StrokeWidthScale = strokeWidthScale
		}
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
