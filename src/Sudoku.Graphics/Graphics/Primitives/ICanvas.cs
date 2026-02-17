namespace Sudoku.Graphics.Primitives;

/// <summary>
/// Represents a canvas object.
/// </summary>
/// <typeparam name="TDrawingOptions">Indicates the type of drawing options.</typeparam>
public interface ICanvas<out TDrawingOptions> :
	ICanvasDrawLines,
	ICanvasDrawBigText,
	ICanvasDrawSmallText,
	ICanvasExport,
	ICanvasFillBackground,
	IDisposable
	where TDrawingOptions : notnull, IOptionsProvider<TDrawingOptions>, new()
{
	/// <summary>
	/// Indicates drawing options.
	/// </summary>
	TDrawingOptions Options { get; }

	/// <summary>
	/// Indicates backing canvas.
	/// </summary>
	protected internal SKCanvas BackingCanvas { get; }
}
