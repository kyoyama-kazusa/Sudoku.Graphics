namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents a cell group killer cage mark item.
/// </summary>
public sealed record CellGroupKillerCageMarkItem : CellGroupMarkItem
{
	/// <summary>
	/// Indicates the padding top of the boundary of text drawn.
	/// </summary>
	public float PaddingTop { get; init; }

	/// <summary>
	/// Indicates the padding bottom of the boundary of text drawn.
	/// </summary>
	public float PaddingBottom { get; init; }

	/// <summary>
	/// Indicates the padding left of the boundary of text drawn.
	/// </summary>
	public float PaddingLeft { get; init; }

	/// <summary>
	/// Indicates the padding right of the boundary of text drawn.
	/// </summary>
	public float PaddingRight { get; init; }

	/// <summary>
	/// The X value of offset to the text to be drawn.
	/// </summary>
	public float OffsetX { get; init; }

	/// <summary>
	/// The Y value of offset to the text to be drawn.
	/// </summary>
	public float OffsetY { get; init; }

	/// <summary>
	/// Indicates text to be drawn in killer cage.
	/// </summary>
	public string? Text { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellGroupMark_KillerCage;

	/// <summary>
	/// Indicates the text color.
	/// </summary>
	public SerializableColor TextColor { get; init; }

	/// <summary>
	/// Indicates text background color.
	/// </summary>
	public SerializableColor TextBackgroundColor { get; init; }

	/// <summary>
	/// Indicates the scale of size of each cells drawn, related to cell size.
	/// </summary>
	public required Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates the dash sequence.
	/// </summary>
	public LineDashSequence DashSequence { get; init; } = [];


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawKillerCage(
			Cells,
			SizeScale,
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
