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
	/// Indicates corner radius ratio of rounded rectangle of border lines, relative to cell size. By default it's 0.12.
	/// </summary>
	public Ratio GridBorderRoundedRectangleCornerRadius { get; set; } = .12F;

	/// <summary>
	/// Indicates stroke thickness ratio of thin lines, relative to cell size. By default it's 0.015.
	/// </summary>
	public Ratio ThinLineWidth { get; set; } = .015F;

	/// <summary>
	/// Indicates stroke thickness ratio of thick lines, relative to cell size. By default it's 0.06.
	/// </summary>
	public Ratio ThickLineWidth { get; set; } = .06F;

	/// <summary>
	/// Represents background color. By default it's equivalent value of color <see cref="SKColors.White"/>.
	/// </summary>
	/// <seealso cref="SKColors.White"/>
	public SerializableColor BackgroundColor { get; set; } = SKColors.White;

	/// <summary>
	/// Represents thin line color. By default it's equivalent value of color <see cref="SKColors.Black"/>.
	/// </summary>
	/// <seealso cref="SKColors.Black"/>
	public SerializableColor ThinLineColor { get; set; } = SKColors.Black;

	/// <summary>
	/// Represents thick line color. By default it's equivalent value of color <see cref="SKColors.Black"/>.
	/// </summary>
	/// <seealso cref="SKColors.Black"/>
	public SerializableColor ThickLineColor { get; set; } = SKColors.Black;

	/// <summary>
	/// Represents grid line template to be drawn. By default it will be initialized as a <see cref="RectangularBlockLineTemplate"/>
	/// like this:
	/// <code>
	/// <c><see langword="new"/> <see cref="RectangularBlockLineTemplate"/>(3, 3)</c>
	/// </code>
	/// </summary>
	/// <seealso cref="RectangularBlockLineTemplate"/>
	public BlockLineTemplate GridLineTemplate { get; set; } = new RectangularBlockLineTemplate(3, 3);


	/// <inheritdoc/>
	static CanvasDrawingOptions IOptionsProvider<CanvasDrawingOptions>.DefaultInstance => Default;
}
