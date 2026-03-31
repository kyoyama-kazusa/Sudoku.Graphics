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
const float offsetX = 0, offsetY = 6, paddingLeft = 4, paddingTop = 0, paddingRight = 4, paddingBottom = 0;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = SKColors.White },
		new TemplateLineItem(),
		new CellGroupKillerCageMarkItem
		{
			TemplateIndex = 0,
			Cells = [0, 1, 7, 8, 14, 13],
			Text = "21",
			DashSequence = dashSequence,
			CornerRadiusScale = cornerRadiusScale,
			ShortSideScale = sizeScale,
			StrokeWidthScale = 0.04M,
			StrokeColor = SKColors.Black,
			TextFontName = "Arial",
			FontSizeScale = fontSizeScale,
			TextColor = SKColors.Red,
			TextBackgroundColor = SKColors.White,
			FillColor = SKColors.White,
			FontWeight = SKFontStyleWeight.Medium,
			Padding = new(paddingLeft, paddingTop, paddingRight, paddingBottom),
			Offset = new(offsetX, offsetY)
		},
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
Console.WriteLine("Okay.");
