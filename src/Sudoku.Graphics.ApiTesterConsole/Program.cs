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
using Sudoku.Graphics.Items.CellPairTextMarks;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Lines;
using Sudoku.Graphics.Templates;

//var options = new CanvasDrawingOptions();
var desktop = Environment.DesktopPath;
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 6, ColumnsCount = 6 }
};
using var canvas = new Canvas(
	new StandardTemplate(2, 3, mapper)
	{
		ThickLineWidth = .06M,
		ThinLineWidth = .0225M,
		ThickLineColor = SKColors.Black,
		ThinLineColor = SKColors.Black
	}
);

LineDashSequence dashSequence = [10, 10];
Scale cornerRadiusScale = 0M, sizeScale = .8M, fontSizeScale = .25M;
const float offsetX = 0, offsetY = 8, paddingLeft = 8, paddingTop = 8, paddingRight = 8, paddingBottom = 8;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = SKColors.White },
		new TemplateLineItem(),
		new CellPairRomanNumeralTextMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 0,
			Cell2 = 1,
			Value = 5,
			FontName = "Arial",
			FontSizeScale = fontSizeScale,
			FontColor = SKColors.Black,
			FillColor =	SKColors.White,
			StrokeColor = SKColors.Black,
			StrokeWidthScale = .025M,
			OffsetX = offsetX,
			OffsetY = offsetY,
			PaddingLeft = paddingLeft,
			PaddingTop = paddingTop,
			PaddingRight = paddingRight,
			PaddingBottom = paddingBottom
		},
		new CellPairRomanNumeralTextMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 1,
			Cell2 = 7,
			Value = 10,
			FontName = "Arial",
			FontSizeScale = fontSizeScale,
			FontColor = SKColors.Black,
			FillColor = SKColors.White,
			StrokeColor = SKColors.Black,
			StrokeWidthScale = .025M,
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
