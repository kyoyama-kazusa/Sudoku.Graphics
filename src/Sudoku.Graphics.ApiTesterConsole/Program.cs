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
using Sudoku.Graphics.Items.CandidateMarks;
using Sudoku.Graphics.Items.CellGroupMarks;
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
		ThickLineWidth = .06M,
		ThinLineWidth = .0225M,
		ThickLineColor = SKColors.Black,
		ThinLineColor = SKColors.Black
	}
);

canvas.DrawItems(
	[
		new BackgroundFillItem { Color = SKColors.White },
		new TemplateLineItem(),
		..
		from digit in Enumerable.Range(0, 6)
		select new CandidateCircleMarkItem
		{
			TemplateIndex = 0,
			CandidatePosition = new(0, 3, digit),
			SizeScale = 0.75M,
			FillColor = SKColors.Green.WithAlpha(128)
		},
		..
		from digit in Enumerable.Range(0, 6)
		select new CandidateTextItem
		{
			TemplateIndex = 0,
			CandidatePosition = new(0, 3, digit),
			Text = (digit + 1).ToString(),
			FontName = "Arial",
			FontSizeScale = 0.75M,
			Color = SKColors.Black
		}
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
Console.WriteLine("Okay.");
