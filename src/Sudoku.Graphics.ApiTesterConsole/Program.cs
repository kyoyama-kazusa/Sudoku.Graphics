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
using Sudoku.ComponentModel.GridTemplates;
using Sudoku.ComponentModel.Items;
using Sudoku.Graphics;

var desktop = Environment.DesktopPath;
var options = new CanvasDrawingOptions();
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 9, ColumnsCount = 9 }
};
using var canvas = new Canvas(
	new StandardGridTemplate(-1, -1, mapper)
	{
		ThickLineWidth = options.ThickLineWidth.Resolve(options),
		ThinLineWidth = options.ThinLineWidth.Resolve(options),
		ThickLineColor = options.ThickLineColor.Resolve(options),
		ThinLineColor = options.ThinLineColor.Resolve(options)
	}
);
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = options.BackgroundColor.Resolve(options) },
		new TemplateLineStrokeItem(),
		..
		from digit in Enumerable.Range(0, 9)
		select new CandidateTextItem
		{
			CandidatePosition = new(0, 3, digit),
			FontName = "Arial",
			Color = options.TextColorSet.Resolve(options)[^1],
			FontSizeScale = .8M,
			Text = (digit + 1).ToString(),
			TemplateIndex = 0
		}
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
	///// <summary>
	///// Represents options.
	///// </summary>
	//private static readonly JsonSerializerOptions Options = new()
	//{
	//	WriteIndented = true,
	//	IndentCharacter = ' ',
	//	IndentSize = 2,
	//	IgnoreReadOnlyProperties = true
	//};


	extension(Environment)
	{
		/// <summary>
		/// Represents desktop path.
		/// </summary>
		public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
	}
}
