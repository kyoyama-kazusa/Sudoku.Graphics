
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
using Sudoku.Graphics.Items.CellPairMarks;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Lines;
using Sudoku.Graphics.Items.Texts;
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

var rng = Random.Shared;
LineDashSequence dashSequence = [10, 10];
Scale strokeSizeScale = .7M, strokeWidthScale = .04M;
SerializableColor strokeColor = SKColors.Black;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = SKColors.White },
		new TemplateLineItem(),
		new GivenTextItem
		{
			Cell = 0,
			Text = "3",
			Color = SKColors.Black,
			FontName = "Arial",
			FontSizeScale = .8M,
			TemplateIndex = 0
		},
		new GivenTextItem
		{
			Cell = 35,
			Text = "6",
			Color = SKColors.Black,
			FontName = "Arial",
			FontSizeScale = .8M,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 6,
			Cell2 = 13,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 7,
			Cell2 = 12,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 8,
			Cell2 = 13,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 9,
			Cell2 = 16,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 10,
			Cell2 = 15,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 10,
			Cell2 = 17,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 11,
			Cell2 = 16,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 14,
			Cell2 = 21,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 16,
			Cell2 = 21,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 19,
			Cell2 = 24,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 20,
			Cell2 = 27,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 23,
			Cell2 = 28,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 26,
			Cell2 = 31,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 28,
			Cell2 = 33,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
		},
		new CellPairConnectionLineMarkItem
		{
			Cell1 = 27,
			Cell2 = 34,
			SizeScale = strokeSizeScale,
			StrokeWidthScale = strokeWidthScale,
			StrokeColor = strokeColor,
			TemplateIndex = 0
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
