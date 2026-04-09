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
using Sudoku.Graphics.Directions;
using Sudoku.Graphics.Items.CellMarks;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Lines;
using Sudoku.Graphics.Templating.Templates;

//var options = new CanvasDrawingOptions();
var desktop = Environment.DesktopPath;
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 7, ColumnsCount = 7 }
};
using var canvas = new Canvas(
	new DefaultTemplate
	{
		Mapper = mapper,
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
		new CellFillItem { TemplateIndex = 0, Cell = 22, Color = SKColors.Black },
		new CellLoopSegmentLineMarkItem
		{
			TemplateIndex = 0,
			Cell = 16,
			OccupiedDirections = Direction4.Right | Direction4.Down,
			StrokeColor = SKColors.DimGray,
			StrokeWidthScale = 0.05M,
			CornerRadiusScale = 1M
		},
		new CellLoopSegmentLineMarkItem
		{
			TemplateIndex = 0,
			Cell = 19,
			OccupiedDirections = Direction4.Left | Direction4.Right,
			StrokeColor = SKColors.DimGray,
			StrokeWidthScale = 0.05M,
			CornerRadiusScale = 1M
		},
		new CellLoopSegmentLineMarkItem
		{
			TemplateIndex = 0,
			Cell = 39,
			OccupiedDirections = Direction4.Up | Direction4.Down,
			StrokeColor = SKColors.DimGray,
			StrokeWidthScale = 0.05M,
			CornerRadiusScale = 1M
		},
		new CellLoopSegmentLineMarkItem
		{
			TemplateIndex = 0,
			Cell = 44,
			OccupiedDirections = Direction4.Up | Direction4.Right,
			StrokeColor = SKColors.DimGray,
			StrokeWidthScale = 0.05M,
			CornerRadiusScale = 1M
		},
		new CellLoopSegmentLineMarkItem
		{
			TemplateIndex = 0,
			Cell = 46,
			OccupiedDirections = Direction4.Up | Direction4.Right,
			StrokeColor = SKColors.DimGray,
			StrokeWidthScale = 0.05M,
			CornerRadiusScale = 1M
		},
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
Console.WriteLine("Okay.");
