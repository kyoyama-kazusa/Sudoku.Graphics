namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object.
/// </summary>
public interface ICanavs :
	ICanvasDrawLinesMethods,
	ICanvasFillBackgroundMethods,
	ICanvasExportMethods,
	IDisposable;
