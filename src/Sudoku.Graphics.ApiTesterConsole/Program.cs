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
using Sudoku.ComponentModel;
using Sudoku.Graphics;
using Sudoku.Graphics.Items.CellPairMarks;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Texts;
using Sudoku.Graphics.Templates;

var desktop = Environment.DesktopPath;
var options = new CanvasDrawingOptions();
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 7, ColumnsCount = 7 }
};
using var canvas = new Canvas(
	new DefaultTemplate { Mapper = mapper }
);

var rng = Random.Shared;
var puzzleString = "3..4.3.......24....3..........2..3........4...3.3";
const float circleSize = .9F;
var strokeWidthScale = (Scale).06M;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = options.BackgroundColor.Resolve(options) },
		//new TemplateLineItem(),
		..
		from pair in puzzleString.Index()
		where pair.Item != '.'
		let cellIndex = pair.Index
		let digitString = pair.Item.ToString()
		select new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = cellIndex,
			Text = digitString,
			FontName = "Cascadia Code",
			FontSizeScale = options.BigTextFontSizeScale.Resolve(options),
			Color = SKColors.Black
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 0,
			Cell2 = 3,
			LinesCount = 2,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 3,
			Cell2 = 5,
			LinesCount = 2,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 0,
			Cell2 = 14,
			LinesCount = 1,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 14,
			Cell2 = 19,
			LinesCount = 1,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 5,
			Cell2 = 19,
			LinesCount = 1,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 19,
			Cell2 = 33,
			LinesCount = 1,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 30,
			Cell2 = 33,
			LinesCount = 2,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 14,
			Cell2 = 42,
			LinesCount = 2,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 42,
			Cell2 = 46,
			LinesCount = 2,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 46,
			Cell2 = 48,
			LinesCount = 1,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
		},
		new CellPairBridgeLineMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 13,
			Cell2 = 48,
			LinesCount = 2,
			CircleScale = circleSize,
			StrokeColor = SKColors.Black,
			LinesMaxGapScale = .2M,
			StrokeWidthScale = strokeWidthScale
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
