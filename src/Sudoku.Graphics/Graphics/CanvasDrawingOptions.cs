namespace Sudoku.Graphics;

/// <summary>
/// Represents drawing options.
/// </summary>
public sealed class CanvasDrawingOptions : IOptionsProvider<CanvasDrawingOptions>
{
	/// <summary>
	/// Indicates the default instance.
	/// </summary>
	public static readonly CanvasDrawingOptions Default = new();


	/// <summary>
	/// Represents background color. By default it's equivalent value of color <see cref="SKColors.White"/>.
	/// </summary>
	/// <seealso cref="SKColors.White"/>
	public SerializableColor BackgroundColor { get; set; } = SKColors.White;


	/// <inheritdoc/>
	static CanvasDrawingOptions IOptionsProvider<CanvasDrawingOptions>.DefaultInstance => Default;
}
