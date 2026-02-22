// This project only tests for APIs.
//
// I don't usually use NUnit or XUnit test-related packages,
// so I just use a console project to test for them.
//
// If you don't want to view for this, you can remove this project,
// or just close this project from solution explorer :)

using System;
using System.IO;
using Sudoku.ComponentModel;
using Sudoku.ComponentModel.Crossmath;
using Sudoku.ComponentModel.GridTemplates;
using Sudoku.ComponentModel.Items;
using Sudoku.Graphics;

var desktop = Environment.DesktopPath;
var options = new CanvasDrawingOptions();
var formulae = (CrossmathFormula[])[
	new()
	{
		Cell = 0,
		ExpandingDirection = Direction.Right,
		ValueSequence = ["3", "+", null, "=", "14"]
	},
	new()
	{
		Cell = 0,
		ExpandingDirection = Direction.Down,
		ValueSequence = ["3", "+", "1", "=", null]
	},
	new()
	{
		Cell = 52,
		ExpandingDirection = Direction.Right,
		ValueSequence = [null, "x", "4", "=", null]
	},
	new()
	{
		Cell = 4,
		ExpandingDirection = Direction.Down,
		ValueSequence = ["14", "+", "2", "=", null]
	},
	new()
	{
		Cell = 30,
		ExpandingDirection = Direction.Right,
		ValueSequence = ["2", "x", "2", "=", null]
	},
	new()
	{
		Cell = 8,
		ExpandingDirection = Direction.Right,
		ValueSequence = ["5", "+", "10", "=", null]
	},
	new()
	{
		Cell = 8,
		ExpandingDirection = Direction.Down,
		ValueSequence = ["5", "+", null, "=", null]
	},
	new()
	{
		Cell = 12,
		ExpandingDirection = Direction.Down,
		ValueSequence = [null, "x", null, "=", null]
	},
	new()
	{
		Cell = 60,
		ExpandingDirection = Direction.Right,
		ValueSequence = [null, "x", null, "=", "45"]
	},
	new()
	{
		Cell = 54,
		ExpandingDirection = Direction.Down,
		ValueSequence = ["4", "x", null, "=", null]
	},
	new()
	{
		Cell = 62,
		ExpandingDirection = Direction.Down,
		ValueSequence = [null, "-", "3", "=", null]
	},
	new()
	{
		Cell = 104,
		ExpandingDirection = Direction.Right,
		ValueSequence = ["3", "x", null, "=", "12"]
	},
	new()
	{
		Cell = 104,
		ExpandingDirection = Direction.Down,
		ValueSequence = ["3", "+", "17", "=", null]
	},
	new()
	{
		Cell = 108,
		ExpandingDirection = Direction.Down,
		ValueSequence = ["12", "-", "8", "=", null]
	},
	new()
	{
		Cell = 156,
		ExpandingDirection = Direction.Right,
		ValueSequence = [null, "÷", null, "=", null]
	},
	new()
	{
		Cell = 134,
		ExpandingDirection = Direction.Right,
		ValueSequence = ["8", "+", null, "=", null]
	},
	new()
	{
		Cell = 112,
		ExpandingDirection = Direction.Right,
		ValueSequence = ["6", "÷", null, "=", null]
	},
	new()
	{
		Cell = 112,
		ExpandingDirection = Direction.Down,
		ValueSequence = ["6", "+", null, "=", null]
	},
	new()
	{
		Cell = 116,
		ExpandingDirection = Direction.Down,
		ValueSequence = [null, "x", "5", "=", "15"]
	},
	new()
	{
		Cell = 164,
		ExpandingDirection = Direction.Right,
		ValueSequence = [null, "x", null, "=", "15"]
	}
];
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 13, ColumnsCount = 13 }
};
using var canvas = new Canvas(
	new CrossmathGridTemplate
	{
		Mapper = mapper,
		Formulae = formulae,
		ThickLineWidth = options.ThickLineWidth.Resolve(options),
		ThinLineWidth = options.ThinLineWidth.Resolve(options),
		ThickLineColor = options.ThickLineColor.Resolve(options),
		ThinLineColor = options.ThinLineColor.Resolve(options)
	}
);
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = options.BackgroundColor.Resolve(options) },
		new TemplateLineStrokeItem
		{
			FillIntersectionCells = true,
			TemplateIntersectionCellsColor = options.TemplateIntersectionColor.Resolve(options)
		},
		..
		CrossmathPuzzleCellTextItemFactory.Create(
			formulae,
			mapper,
			options.BigTextFontName.Resolve(options),
			options.BigTextFontSizeScale.Resolve(options),
			options.TextColorSet.Resolve(options)[0]
		)
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
