namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents a cell group killer cage mark item.
/// </summary>
public sealed record CellGroupKillerCageMarkItem : CellGroupMarkItem
{
	/// <summary>
	/// Indicates the padding top of the boundary of text drawn.
	/// </summary>
	public float PaddingTop { get; init; } = 0;

	/// <summary>
	/// Indicates the padding bottom of the boundary of text drawn.
	/// </summary>
	public float PaddingBottom { get; init; } = 0;

	/// <summary>
	/// Indicates the padding left of the boundary of text drawn.
	/// </summary>
	public float PaddingLeft { get; init; } = 0;

	/// <summary>
	/// Indicates the padding right of the boundary of text drawn.
	/// </summary>
	public float PaddingRight { get; init; } = 0;

	/// <summary>
	/// The X value of offset to the text to be drawn.
	/// </summary>
	public float OffsetX { get; init; } = 0;

	/// <summary>
	/// The Y value of offset to the text to be drawn.
	/// </summary>
	public float OffsetY { get; init; } = 0;

	/// <summary>
	/// Indicates text to be drawn in killer cage.
	/// If a cage this instance represents doesn't provide a text, this property can be left <see langword="null"/>.
	/// </summary>
	public string? Text { get; init; }

	/// <inheritdoc/>
	public override string? TextFontName { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellGroup_KillerCage;

	/// <inheritdoc/>
	public override SKFontStyleWeight FontWeight { get; init; } = SKFontStyleWeight.Normal;

	/// <inheritdoc/>
	public override SKFontStyleWidth FontWidth { get; init; } = SKFontStyleWidth.Normal;

	/// <inheritdoc/>
	public override SKFontStyleSlant FontSlant { get; init; } = SKFontStyleSlant.Upright;

	/// <summary>
	/// Indicates the text color.
	/// </summary>
	public SerializableColor TextColor { get; init; }

	/// <summary>
	/// Indicates text background color.
	/// </summary>
	public SerializableColor TextBackgroundColor { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor FillColor { get; init; }

	/// <summary>
	/// Indicates the scale of size of each cells drawn, related to cell size.
	/// </summary>
	public required Scale ShortSideScale { get; init; }

	/// <inheritdoc/>
	public override required Scale FontSizeScale { get; init; }

	/// <inheritdoc/>
	public override required Scale CornerRadiusScale { get; init; }

	/// <inheritdoc/>
	public override required Scale StrokeWidthScale { get; init; }

	/// <summary>
	/// Indicates the dash sequence.
	/// </summary>
	public LineDashSequence DashSequence { get; init; } = [];


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawKillerCage(
			Cells,
			ShortSideScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			DashSequence,
			CornerRadiusScale,
			Text,
			TextFontName,
			FontSizeScale,
			FontWeight,
			FontWidth,
			FontSlant,
			TextColor,
			TextBackgroundColor,
			PaddingTop,
			PaddingBottom,
			PaddingLeft,
			PaddingRight,
			OffsetX,
			OffsetY,
			canvas.Templates[TemplateIndex].Mapper
		);
}
