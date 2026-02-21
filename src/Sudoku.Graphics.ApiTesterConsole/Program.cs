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
using Sudoku.ComponentModel.GridTemplates;
using Sudoku.ComponentModel.Items;
using Sudoku.Graphics;

var desktop = Environment.DesktopPath;
var defaultTemplateSize = new GridTemplateSize(9, 9);
var baseMapper = new PointMapper(120, 15, defaultTemplateSize);
using var canvas = new Canvas([new StandardGridTemplate { Mapper = baseMapper }]);
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = canvas.GetOptionValue(static options => options.BackgroundColor) },
		new TemplateLineStrokeItem
		{
			FillIntersectionCells = true,
			TemplateIntersectionCellsColor = canvas.GetOptionValue(static options => options.TemplateIntersectionColor)
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
