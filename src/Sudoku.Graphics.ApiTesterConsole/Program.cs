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
using Sudoku.Graphics.Items.CandidateMarks;
using Sudoku.Graphics.Items.CandidatePairMarks;
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
		new CandidateCircleMarkItem
		{
			TemplateIndex = 0,
			CandidatePosition = new(0, 3, 2),
			SizeScale = 0.8M,
			FillColor = new(63, 218, 101)
		},
		new CandidateCircleMarkItem
		{
			TemplateIndex = 0,
			CandidatePosition = new(15, 3, 5),
			SizeScale = 0.8M,
			FillColor = new(63, 218, 101)
		},
		new CandidateTextItem
		{
			TemplateIndex = 0,
			CandidatePosition = new(0, 3, 2),
			Text = "3",
			Color = SKColors.DimGray,
			FontName = "Arial",
			FontSizeScale = 0.8M
		},
		new CandidateTextItem
		{
			TemplateIndex = 0,
			CandidatePosition = new(15, 3, 5),
			Text = "6",
			Color = SKColors.DimGray,
			FontName = "Arial",
			FontSizeScale = 0.8M
		},
		new CandidatePairLinkMarkItem
		{
			TemplateIndex = 0,
			CandidatePosition1 = new(0, 3, 2),
			CandidatePosition2 = new(15, 3, 5),
			StrokeColor = SKColors.Red,
			StrokeWidthScale = 0.025M,
			ArrowCapLengthScale = 0.2M,
			HalfArrowCapRotationDegrees = 25,
			Candidate1SizeScale = 1.3M,
			Candidate2SizeScale = 1.3M,
			DashSequence = [5, 5]
		}
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
Console.WriteLine("Okay.");
