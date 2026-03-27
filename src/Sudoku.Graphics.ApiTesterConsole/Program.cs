
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
using Sudoku.ComponentModel;
using Sudoku.Graphics;
using Sudoku.Graphics.Items.CellGroupMarks;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Lines;
using Sudoku.Graphics.Templates;

var desktop = Environment.DesktopPath;
var options = new CanvasDrawingOptions();
var mapper = new PointMapper
{
	CellSize = 240,
	Margin = 30,
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
LineDashSequence dashSequence = [10, 10];
Scale cornerRadiusScale = 0M, sizeScale = .8M, fontSizeScale = .3M;
const float offsetX = 8, offsetY = 36, paddingLeft = 0, paddingTop = 0, paddingRight = 0, paddingBottom = 0;
const string fontName = "Arial";
const SKFontStyleWeight fontWeight = SKFontStyleWeight.Medium;
var textColor = SKColors.Black;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = options.BackgroundColor.Resolve(options) },
		new TemplateLineItem(),
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [0],
			Text = "1",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [1, 2, 8],
			Text = "12",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [6, 7],
			Text = "8",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [12, 18],
			Text = "9",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [13, 19],
			Text = "3",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [14, 20, 21],
			Text = "14",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [3, 9, 15],
			Text = "8",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [4, 10, 16],
			Text = "13",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [5, 11],
			Text = "7",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [17, 23, 22],
			Text = "9",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [24, 25, 26],
			Text = "11",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [30],
			Text = "3",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [31, 32],
			Text = "7",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [27, 33],
			Text = "8",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [28, 34],
			Text = "6",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [29, 35],
			Text = "7",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			SizeScale = sizeScale,
			StrokeWidthScale = options.ThinLineWidth.Resolve(options),
			StrokeColor = SKColors.Black,
			TextFontName = fontName,
			FontSizeScale = fontSizeScale,
			TextColor = textColor,
			TextBackgroundColor = SKColors.White,
			FontWeight = fontWeight,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
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
