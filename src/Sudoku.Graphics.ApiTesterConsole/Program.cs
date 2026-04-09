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
using Sudoku.Graphics.Items.CellMarks;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Lines;
using Sudoku.Graphics.Items.Texts;
using Sudoku.Graphics.Templating.Templates;

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
		ThickLineWidth = 0.06M,
		ThinLineWidth = 0.0225M,
		ThickLineColor = SKColors.Black,
		ThinLineColor = SKColors.Black
	}
);

canvas.DrawItems(
	[
		new BackgroundFillItem { Color = SKColors.White },
		new TemplateLineItem(),
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 1,
			Color = SKColors.Black,
			Text = "2",
			FontName = "Arial",
			FontSizeScale = 0.8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 2,
			Color = SKColors.Black,
			Text = "3",
			FontName = "Arial",
			FontSizeScale = 0.8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 7,
			Color = SKColors.Black,
			Text = "4",
			FontName = "Arial",
			FontSizeScale = 0.8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 16,
			Color = SKColors.Black,
			Text = "5",
			FontName = "Arial",
			FontSizeScale = 0.8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 19,
			Color = SKColors.Black,
			Text = "6",
			FontName = "Arial",
			FontSizeScale = 0.8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 28,
			Color = SKColors.Black,
			Text = "6",
			FontName = "Arial",
			FontSizeScale = 0.8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 33,
			Color = SKColors.Black,
			Text = "1",
			FontName = "Arial",
			FontSizeScale = 0.8M
		},
		new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = 34,
			Color = SKColors.Black,
			Text = "3",
			FontName = "Arial",
			FontSizeScale = 0.8M
		},
		new CellLargeDiamondMarkItem
		{
			TemplateIndex = 0,
			Cell = 8,
			FillColor = SKColors.LightGray
		},
		new CellLargeDiamondMarkItem
		{
			TemplateIndex = 0,
			Cell = 16,
			FillColor = SKColors.LightGray
		},
		new CellLargeDiamondMarkItem
		{
			TemplateIndex = 0,
			Cell = 19,
			FillColor = SKColors.LightGray
		},
		new CellLargeDiamondMarkItem
		{
			TemplateIndex = 0,
			Cell = 27,
			FillColor = SKColors.LightGray
		},
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
Console.WriteLine("Okay.");
