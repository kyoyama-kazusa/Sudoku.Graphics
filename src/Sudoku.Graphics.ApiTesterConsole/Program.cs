// This project only tests for APIs.
//
// I don't usually use NUnit or XUnit test-related packages,
// so I just use a console project to test for them.
//
// If you don't want to view for this, you can remove this project,
// or just close this project from solution explorer :)

using System;
using System.IO;
using System.Text.Json;
using Sudoku.ComponentModel;
using Sudoku.ComponentModel.Items;
using Sudoku.Graphics;

var desktop = Environment.DesktopPath;
var defaultTemplateSize = new GridTemplateSize(9, 9);
var baseMapper = new PointMapper(120, 15, defaultTemplateSize);
using var canvas = new Canvas(
	[
		.. OverlappingGridTemplates.Gattai3(3, 3, baseMapper),
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
canvas.DrawItems(
	[
		new BackgroundFillItem(),
		new TemplateLineStrokeItem(),
		//..
		//from digit in Enumerable.Range(0, 9)
		//select new BigTextItem
		//{
		//	TemplateIndex = 1,
		//	Text = (digit + 1).ToString(),
		//	Cell = canvas.Templates[1].Mapper.GetAbsoluteIndex(digit * 10),
		//	Color = SKColors.Black
		//}
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });

canvas.Options.WriteTo(Path.Combine(Environment.DesktopPath, "drawing-config.json"), Options);
Console.WriteLine("Okay.");


/// <summary>
/// Provides main method as entry point of this program.
/// </summary>
file static partial class Program
{
	/// <summary>
	/// Represents options.
	/// </summary>
	private static readonly JsonSerializerOptions Options = new()
	{
		WriteIndented = true,
		IndentCharacter = ' ',
		IndentSize = 2
	};


	extension(Environment)
	{
		/// <summary>
		/// Represents desktop path.
		/// </summary>
		public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
	}
}
