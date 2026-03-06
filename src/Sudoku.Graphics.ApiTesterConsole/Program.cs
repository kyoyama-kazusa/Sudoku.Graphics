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
using Sudoku.ComponentModel.Directions;
using Sudoku.ComponentModel.Templates;
using Sudoku.Graphics;
using Sudoku.Graphics.Items;
using Sudoku.Graphics.Items.CellTextMarks;

var desktop = Environment.DesktopPath;
var options = new CanvasDrawingOptions();
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 9, ColumnsCount = 9 }
};
using var canvas = new Canvas(
	new StandardTemplate(3, 3, mapper)
	{
		ThickLineWidth = options.ThickLineWidth.Resolve(options),
		ThinLineWidth = options.ThinLineWidth.Resolve(options),
		ThickLineColor = options.ThickLineColor.Resolve(options),
		ThinLineColor = options.ThinLineColor.Resolve(options)
	}
);

var rng = Random.Shared;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = options.BackgroundColor.Resolve(options) },
		new TemplateLineStrokeItem(),
		..
		from cell in SpanEnumerable.Range(0, 81)
		select new CellBorderAlignedTextMarkItem
		{
			Cell = cell,
			TemplateIndex = 0,
			AlignedDirection = Enum.GetValues<Direction8>()[5..][rng.Next(0, 4)],
			//RotationDirection = Enum.GetValues<Direction8>()[rng.Next(0, 9)],
			SizeScale = .35M,
			Text = (rng.Next(0, 9) + 1).ToString(),
			TextFontName = "Arial",
			FillColor = SKColors.DimGray
		},
	]
);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
Console.WriteLine("Okay.");


/// <summary>
/// Provides main method as entry point of this program.
/// </summary>
file static partial class Program
{
	extension(Environment)
	{
		/// <summary>
		/// Represents desktop path.
		/// </summary>
		public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
	}
}
