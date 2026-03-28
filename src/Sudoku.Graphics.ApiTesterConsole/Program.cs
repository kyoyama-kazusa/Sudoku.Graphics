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
using Sudoku.Graphics;
using Sudoku.Graphics.Items.Fills;
using Sudoku.Graphics.Items.Lines;
using Sudoku.Graphics.Items.Texts;
using Sudoku.Graphics.Templates;

//var options = new CanvasDrawingOptions();
var desktop = Environment.DesktopPath;
var mapper = new PointMapper
{
	CellSize = 120,
	Margin = 15,
	TemplateSize = new() { RowsCount = 7, ColumnsCount = 7 }
};
using var canvas = new Canvas(
	new DefaultTemplate
	{
		Mapper = mapper,
		ThickLineWidth = .06M,
		ThinLineWidth = .0225M,
		ThickLineColor = SKColors.Black,
		ThinLineColor = SKColors.Black
	}
);

const string weekdays = "日一二三四五六";
canvas.DrawItems(
	[
		new BackgroundFillItem { Color = SKColors.White },
		new TemplateLineItem(),
		..
		from pair in weekdays.Index()
		let index = pair.Index
		let dayChar = pair.Item
		select new GivenTextItem
		{
			TemplateIndex = 0,
			Cell = index,
			Color = SKColors.Black,
			FontName = "黑体",
			FontSizeScale = .4M,
			Text = $"星期{dayChar}"
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
