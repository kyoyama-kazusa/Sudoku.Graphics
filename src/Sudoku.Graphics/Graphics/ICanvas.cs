namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object.
/// </summary>
/// <typeparam name="TDrawingOptions">Indicates the type of drawing options.</typeparam>
/// <typeparam name="TExportingOptions">Indicates the type of exporting options.</typeparam>
public interface ICanvas<out TDrawingOptions, out TExportingOptions> :
	ICanvasDrawLines,
	ICanvasExport,
	ICanvasFillBackground,
	IDisposable
	where TDrawingOptions : notnull, IOptionsProvider<TDrawingOptions>, new()
	where TExportingOptions : notnull, IOptionsProvider<TExportingOptions>, new()
{
	/// <summary>
	/// Indicates drawing options.
	/// </summary>
	TDrawingOptions DrawingOptions { get; }

	/// <summary>
	/// Indicates exporting options.
	/// </summary>
	TExportingOptions ExportingOptions { get; }

	/// <summary>
	/// Indicates backing canvas.
	/// </summary>
	protected internal SKCanvas BackingCanvas { get; }
}
