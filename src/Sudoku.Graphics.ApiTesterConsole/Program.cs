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
using Sudoku.Graphics.Templating.Templates;

//var options = new CanvasDrawingOptions();
var desktop = Environment.DesktopPath;
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 9, ColumnsCount = 9 }
};
using var canvas = new Canvas(
	new StandardTemplate(3, 3, mapper)
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
		new CellGroupTrailedCapsuleSatelliteMarkItem
		{
			TemplateIndex = 0,
			Cells = [0],
			CapsuleSizeScale = 0.8M,
			StrokeColor = SKColors.DimGray,
			StrokeWidthScale = 0.03M,
			TrailCells = [0, 9],
		},
		new CellGroupTrailedCapsuleSatelliteMarkItem
		{
			TemplateIndex = 0,
			Cells = [10],
			CapsuleSizeScale = 0.8M,
			StrokeColor = SKColors.DimGray,
			StrokeWidthScale = 0.03M,
			TrailCells = [10, 9],
		},
		new CellGroupTrailedCapsuleSatelliteMarkItem
		{
			TemplateIndex = 0,
			Cells = [20],
			CapsuleSizeScale = 0.8M,
			StrokeColor = SKColors.DimGray,
			StrokeWidthScale = 0.03M,
			TrailCells = [20, 11, 2],
		}
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
Console.WriteLine("Okay.");
