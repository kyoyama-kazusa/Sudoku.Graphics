using System;
using System.IO;
using SkiaSharp;
using Sudoku.Graphics;

using var canvas = new Canvas(40, 10, 9, 9);
canvas.Clear(SKColors.White);
canvas.Export(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "output.png"), new() { Quality = 100 });
Console.WriteLine("okay.");
