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
using Sudoku.ComponentModel;
using Sudoku.ComponentModel.Templates;
using Sudoku.Graphics;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Lines;

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
		new TemplateLineItem(),
		new VariantLineItem
		{
			StartCell = 0,
			StartCellAlignment = Alignment.TopLeft,
			AnchorCell = 1,
			AnchorCellAlignment = Alignment.BottomRight,
			Color = SKColors.Gray.WithAlpha(160),
			StrokeWidthScale = options.ThickLineWidth.Resolve(options),
			TemplateIndex = 0,
			WillExtendLine = true
		},
		new VariantLineItem
		{
			StartCell = 0,
			StartCellAlignment = Alignment.TopLeft,
			AnchorCell = 10,
			AnchorCellAlignment = Alignment.BottomRight,
			Color = SKColors.Gray.WithAlpha(160),
			StrokeWidthScale = options.ThickLineWidth.Resolve(options),
			TemplateIndex = 0,
			WillExtendLine = true
		},
		new VariantLineItem
		{
			StartCell = 0,
			StartCellAlignment = Alignment.TopLeft,
			AnchorCell = 19,
			AnchorCellAlignment = Alignment.BottomRight,
			Color = SKColors.Gray.WithAlpha(160),
			StrokeWidthScale = options.ThickLineWidth.Resolve(options),
			TemplateIndex = 0,
			WillExtendLine = true
		},
		new VariantLineItem
		{
			StartCell = 0,
			StartCellAlignment = Alignment.TopLeft,
			AnchorCell = 28,
			AnchorCellAlignment = Alignment.BottomRight,
			Color = SKColors.Gray.WithAlpha(160),
			StrokeWidthScale = options.ThickLineWidth.Resolve(options),
			TemplateIndex = 0,
			WillExtendLine = true
		}
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
