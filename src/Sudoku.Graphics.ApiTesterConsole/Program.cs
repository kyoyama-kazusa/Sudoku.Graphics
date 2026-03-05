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
using Sudoku.ComponentModel.Maths;
using Sudoku.ComponentModel.Templates;
using Sudoku.Graphics;
using Sudoku.Graphics.Items;
using Sudoku.Graphics.Items.CellMarks;

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

var operators1 = Enum.GetValues<ArithmeticOperator>()[1..];
var operators2 = Enum.GetValues<BitwiseOperator>()[1..];
var operators3 = Enum.GetValues<ComparisonOperator>()[1..];
var rng = Random.Shared;
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = options.BackgroundColor.Resolve(options) },
		new TemplateLineStrokeItem(),
		..
		from cell in SpanEnumerable.Range(0, 81)
		select rng.NextDouble() switch
		{
			< .33 => new CellArithmeticOperatorTextMarkItem
			{
				Cell = cell,
				Operator = operators1[rng.Next(0, operators1.Length)],
				TemplateIndex = 0,
				TextFontName = "Times New Roman",
				FillColor = SKColors.Gray,
				SizeScale = .75M
			},
			< .66 => new CellBitwiseOperatorTextMarkItem
			{
				Cell = cell,
				Operator = operators2[rng.Next(0, operators2.Length)],
				TemplateIndex = 0,
				TextFontName = "Times New Roman",
				FillColor = SKColors.Gray,
				SizeScale = .75M
			},
			<= 1 => new CellComparisonOperatorTextMarkItem
			{
				Cell = cell,
				Operator = operators3[rng.Next(0, operators3.Length)],
				TemplateIndex = 0,
				TextFontName = "Times New Roman",
				FillColor = SKColors.Gray,
				SizeScale = .75M
			},
			_ => default(CellTextMarkItem)
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
