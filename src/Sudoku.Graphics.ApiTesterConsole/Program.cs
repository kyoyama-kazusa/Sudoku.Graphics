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
using SkiaSharp;
using Sudoku.Graphics;
using Sudoku.Graphics.LineTemplates;
using Sudoku.Serialization;

var desktop = Environment.DesktopPath;
using var canvas = Canvas.Create(
	cellSize: 120,
	margin: 10,
	rowsCount: 9,
	columnsCount: 9,
	vector: new(1),
	drawingOptions: new()
	{
		BackgroundColor = Inherited<SerializableColor>.FromValue(SKColors.White),
		ThickLineColor = Inherited<SerializableColor>.FromValue(SKColors.Black),
		ThinLineColor = Inherited<SerializableColor>.FromPropertyName(nameof(CanvasDrawingOptions.ThickLineColor)),
		//GridLineTemplate = new StandardLineTemplate(3)
		GridLineTemplate = new JigsawLineTemplate
		{
			CellIndexGroups = [
				[0, 1, 2, 9, 10, 11, 12, 21, 22],
				[3, 4, 5, 6, 7, 13, 14, 15, 16],
				[8, 17, 23, 24, 25, 26, 31, 32, 34],
				[18, 19, 20, 27, 36, 45, 54, 55, 56],
				[28, 29, 30, 37, 38, 39, 46, 47, 48],
				[33, 35, 40, 41, 42, 43, 44, 51, 53],
				[57, 58, 63, 64, 65, 66, 72, 73, 74],
				[49, 50, 52, 59, 60, 61, 62, 71, 80],
				[67, 68, 69, 70, 75, 76, 77, 78, 79]
			],
			AlsoFillGroups = true
		}
	},
	exportingOptions: new() { Quality = 100 }
);
canvas.FillBackground();
canvas.DrawLines();
canvas.Export(Path.Combine(desktop, "output.png"));

canvas.DrawingOptions.WriteTo(Path.Combine(Environment.DesktopPath, "output.json"), Options);
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
