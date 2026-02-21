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
using System.Text.Json;
using SkiaSharp;
using Sudoku.ComponentModel;
using Sudoku.ComponentModel.GridTemplates;
using Sudoku.ComponentModel.Items;
using Sudoku.Graphics;

var desktop = Environment.DesktopPath;
var defaultTemplateSize = new GridTemplateSize(9, 9);
var baseMapper = new PointMapper(120, 15, defaultTemplateSize);
using var canvas = new Canvas(
	[
		new StandardGridTemplate { Mapper = baseMapper },
		//new SpecifiedGridTemplate
		//{
		//	ThickLineSegments = [
		//		.. Piece.O.GetOutline(RotationType.None, baseMapper, 10, 0),
		//		.. Piece.T.GetOutline(RotationType.None, baseMapper, 10, 3),
		//		.. Piece.J.GetOutline(RotationType.None, baseMapper, 10, 6),
		//		.. Piece.L.GetOutline(RotationType.None, baseMapper, 13, 0),
		//		.. Piece.S.GetOutline(RotationType.None, baseMapper, 13, 3),
		//		.. Piece.Z.GetOutline(RotationType.None, baseMapper, 13, 6)
		//	],
		//	ThinLineSegments = [],
		//	Mapper = baseMapper
		//}
		//new JigsawGridTemplate
		//{
		//	CellIndexGroups = [
		//		[0, 1, 2, 9, 10, 11, 12, 21, 22],
		//		[3, 4, 5, 6, 7, 13, 14, 15, 16],
		//		[8, 17, 23, 24, 25, 26, 31, 32, 34],
		//		[18, 19, 20, 27, 36, 45, 54, 55, 56],
		//		[28, 29, 30, 37, 38, 39, 46, 47, 48],
		//		[33, 35, 40, 41, 42, 43, 44, 51, 53],
		//		[57, 58, 63, 64, 65, 66, 72, 73, 74],
		//		[49, 50, 52, 59, 60, 61, 62, 71, 80],
		//		[67, 68, 69, 70, 75, 76, 77, 78, 79]
		//	],
		//	Mapper = baseMapper,
		//	//AlsoFillGroups = true
		//}
		//new SujikenGridTemplate { Mapper = baseMapper }
		//new DefaultGridTemplate { Mapper = baseMapper }
	]
);

var gridTextItems = JsonSerializer.Deserialize<Item[]>(GridJsonString, Options)!;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = canvas.GetOptionValue(static options => options.BackgroundColor) },
		new TemplateLineStrokeItem
		{
			FillIntersectionCells = true,
			TemplateIntersectionCellsColor = canvas.GetOptionValue(static options => options.TemplateIntersectionColor)
		},
		.. gridTextItems,
		..
		from BigTextItem item in gridTextItems
		select new CellFillItem { TemplateIndex = 0, Cell = item.Cell, Color = SKColors.Yellow }
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });

//canvas.Options.WriteTo(Path.Combine(Environment.DesktopPath, "drawing-config.json"), Options);
Console.WriteLine("Okay.");


/// <summary>
/// Provides main method as entry point of this program.
/// </summary>
file static partial class Program
{
	/*lang=json*/
	public const string GridJsonString = """
		[
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "2",
				"Cell": 1,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "9",
				"Cell": 4,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "6",
				"Cell": 7,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "7",
				"Cell": 14,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "5",
				"Cell": 20,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "8",
				"Cell": 22,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "9",
				"Cell": 26,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "3",
				"Cell": 27,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "7",
				"Cell": 31,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "8",
				"Cell": 32,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "5",
				"Cell": 34,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "5",
				"Cell": 37,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "1",
				"Cell": 38,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "6",
				"Cell": 41,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "3",
				"Cell": 42,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "1",
				"Cell": 48,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "6",
				"Cell": 53,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "4",
				"Cell": 55,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "9",
				"Cell": 59,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "2",
				"Cell": 60,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "4",
				"Cell": 66,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "7",
				"Cell": 71,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "8",
				"Cell": 72,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "2",
				"Cell": 76,
				"Color": "#000000FF"
			},
			{
				"$type": "GivenTextItem",
				"TemplateIndex": 0,
				"Text": "1",
				"Cell": 79,
				"Color": "#000000FF"
			}
		]
		""";

	/// <summary>
	/// Represents options.
	/// </summary>
	private static readonly JsonSerializerOptions Options = new()
	{
		WriteIndented = true,
		IndentCharacter = ' ',
		IndentSize = 2,
		IgnoreReadOnlyProperties = true
	};


	extension(Environment)
	{
		/// <summary>
		/// Represents desktop path.
		/// </summary>
		public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
	}
}
