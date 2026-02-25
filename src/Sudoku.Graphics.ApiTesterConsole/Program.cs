// This project only tests for APIs.
//
// I don't usually use NUnit or XUnit test-related packages,
// so I just use a console project to test for them.
//
// If you don't want to view for this, you can remove this project,
// or just close this project from solution explorer :)

using System;
using System.IO;
using Sudoku.ComponentModel.GridTemplates;
using Sudoku.ComponentModel.Items;
using Sudoku.ComponentModel.Items.CellMarks;
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
		new CellQuestionMarkItem
		{
			TemplateIndex = 0,
			Cell = 0,
			TextFontName = options.CellQuestionMarkFontName.Resolve(options),
			StrokeColor = options.CellQuestionMarkStrokeColor.Resolve(options),
			StrokeWidthScale = options.CellQuestionMarkStrokeWidthScale.Resolve(options),
			FillColor = options.CellQuestionMarkFillColor.Resolve(options),
			SizeScale = options.CellQuestionMarkSizeScale.Resolve(options)
		},
		new CellExclamationMarkItem
		{
			TemplateIndex = 0,
			Cell = 1,
			TextFontName = options.CellExclamationMarkFontName.Resolve(options),
			StrokeColor = options.CellExclamationMarkStrokeColor.Resolve(options),
			StrokeWidthScale = options.CellExclamationMarkStrokeWidthScale.Resolve(options),
			FillColor = options.CellExclamationMarkFillColor.Resolve(options),
			SizeScale = options.CellExclamationMarkSizeScale.Resolve(options)
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
