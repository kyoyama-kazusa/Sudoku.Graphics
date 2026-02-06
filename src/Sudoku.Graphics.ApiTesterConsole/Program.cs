using System;
using System.IO;
using SkiaSharp;
using Sudoku.Graphics;

var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
using var canvas = new Canvas(120, 10, 9, 9);
canvas.Clear(SKColors.White);
canvas.Export(Path.Combine(desktop, "output.png"), new() { Quality = 100 });
Console.WriteLine("okay.");
