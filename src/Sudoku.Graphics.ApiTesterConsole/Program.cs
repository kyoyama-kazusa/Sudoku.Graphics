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
using Sudoku.Graphics.IslandConnectors;
using Sudoku.Graphics.Items.CellPairMarks;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Lines;
using Sudoku.Graphics.Templating;
using Sudoku.Graphics.Templating.Templates;

//var options = new CanvasDrawingOptions();
var desktop = Environment.DesktopPath;
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 13, ColumnsCount = 13, Vector = new(2) }
};

Absolute[]
	block1 = [
		42, 43, 44, 45, 46,
		59, 60, 61, 62, 63,
		76, 77, 78, 79, 80,
		93, 94, 95, 96, 97
	],
	block2 = [
		70, 71, 72, 73,
		87, 88, 89, 90,
		104, 105, 106, 107,
		121, 122, 123, 124,
		138, 139, 140, 141
	],
	block3 = [
		147, 148, 149, 150,
		164, 165, 166, 167,
		181, 182, 183, 184,
		198, 199, 200, 201,
		215, 216, 217, 218
	],
	block4 = [
		191, 192, 193, 194, 195,
		208, 209, 210, 211, 212,
		225, 226, 227, 228, 229,
		242, 243, 244, 245, 246
	];

LineSegment[]
	lineSegments1 = LineSegmentFactory.GetOutline(block1, mapper),
	lineSegments2 = LineSegmentFactory.GetOutline(block2, mapper),
	lineSegments3 = LineSegmentFactory.GetOutline(block3, mapper),
	lineSegments4 = LineSegmentFactory.GetOutline(block4, mapper);

using var canvas = new Canvas(
	new SpecifiedTemplate(mapper)
	{
		ThickLineSegments = [
			.. lineSegments1,
			.. lineSegments2,
			.. lineSegments3,
			.. lineSegments4,
			new(8 * 17 + 8, Direction4.Up | Direction4.Down | Direction4.Left | Direction4.Right)
		],
		ThinLineSegments = [
			.. LineSegmentFactory.GetInline(block1, mapper),
			.. LineSegmentFactory.GetInline(block2, mapper),
			.. LineSegmentFactory.GetInline(block3, mapper),
			.. LineSegmentFactory.GetInline(block4, mapper)
		],
		ThickLineWidth = 0.08M,
		ThickLineColor = SKColors.Black,
		ThinLineWidth = 0.0225M,
		ThinLineColor = SKColors.Black
	}
);

canvas.DrawItems(
	[
		new BackgroundFillItem { Color = SKColors.White },
		new TemplateLineItem(),
		new CellPairIslandConnectorMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 43,
			Cell2 = 71,
			StrokeColor = SKColors.Gray.WithAlpha(128),
			StrokeWidthScale = 0.08M,
			CornerRadiusScale = 0.5M,
			IslandConnector = new DoubleCornerIslandConnector
			{
				StartConnectedDirection = Direction4.Up,
				EndConnectedDirection = Direction4.Up,
				Offset = 1
			}
		},
		new CellPairIslandConnectorMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 44,
			Cell2 = 70,
			StrokeColor = SKColors.Gray.WithAlpha(128),
			StrokeWidthScale = 0.08M,
			CornerRadiusScale = 0.5M,
			IslandConnector = new DoubleCornerIslandConnector
			{
				StartConnectedDirection = Direction4.Up,
				EndConnectedDirection = Direction4.Up,
				Offset = 2
			}
		},
		new CellPairIslandConnectorMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 218,
			Cell2 = 244,
			StrokeColor = SKColors.Gray.WithAlpha(128),
			StrokeWidthScale = 0.08M,
			CornerRadiusScale = 0.5M,
			IslandConnector = new DoubleCornerIslandConnector
			{
				StartConnectedDirection = Direction4.Down,
				EndConnectedDirection = Direction4.Down,
				Offset = 4
			}
		},
		new CellPairIslandConnectorMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 217,
			Cell2 = 245,
			StrokeColor = SKColors.Gray.WithAlpha(128),
			StrokeWidthScale = 0.08M,
			CornerRadiusScale = 0.5M,
			IslandConnector = new DoubleCornerIslandConnector
			{
				StartConnectedDirection = Direction4.Down,
				EndConnectedDirection = Direction4.Down,
				Offset = 3
			}
		},
		new CellPairIslandConnectorMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 104,
			Cell2 = 242,
			StrokeColor = SKColors.Gray.WithAlpha(128),
			StrokeWidthScale = 0.08M,
			CornerRadiusScale = 0.5M,
			IslandConnector = new DoubleCornerIslandConnector
			{
				StartConnectedDirection = Direction4.Left,
				EndConnectedDirection = Direction4.Left,
				Offset = 2
			}
		},
		new CellPairIslandConnectorMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 121,
			Cell2 = 225,
			StrokeColor = SKColors.Gray.WithAlpha(128),
			StrokeWidthScale = 0.08M,
			CornerRadiusScale = 0.5M,
			IslandConnector = new DoubleCornerIslandConnector
			{
				StartConnectedDirection = Direction4.Left,
				EndConnectedDirection = Direction4.Left,
				Offset = 1
			}
		},
		new CellPairIslandConnectorMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 141,
			Cell2 = 144,
			StrokeColor = SKColors.Gray.WithAlpha(128),
			StrokeWidthScale = 0.08M,
			CornerRadiusScale = 0.5M,
			IslandConnector = new DirectIslandConnector()
		},
		new CellPairIslandConnectorMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 144,
			Cell2 = 147,
			StrokeColor = SKColors.Gray.WithAlpha(128),
			StrokeWidthScale = 0.08M,
			CornerRadiusScale = 0.5M,
			IslandConnector = new DirectIslandConnector()
		},
		new CellPairIslandConnectorMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 93,
			Cell2 = 144,
			StrokeColor = SKColors.Gray.WithAlpha(128),
			StrokeWidthScale = 0.08M,
			CornerRadiusScale = 0.5M,
			IslandConnector = new DirectIslandConnector()
		},
		new CellPairIslandConnectorMarkItem
		{
			TemplateIndex = 0,
			Cell1 = 144,
			Cell2 = 195,
			StrokeColor = SKColors.Gray.WithAlpha(128),
			StrokeWidthScale = 0.08M,
			CornerRadiusScale = 0.5M,
			IslandConnector = new DirectIslandConnector()
		}
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
Console.WriteLine("Okay.");
