namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents cell group capsule with arrow line mark item (trailed capsule).
/// </summary>
public sealed record CellGroupTrailedCapsuleMarkItem : CellGroupMarkItem
{
	/// <summary>
	/// Indicates rotation degrees of arrow caps, in angle.
	/// </summary>
	public required float HalfArrowCapRotationDegrees { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellGroup_CapsuleWithArrowLine;

	/// <summary>
	/// Indicates scale of capsule, related to cell size.
	/// </summary>
	public required Scale CapsuleSizeScale { get; init; }

	/// <summary>
	/// Indicates scale of arrow cap length, related to cell size.
	/// </summary>
	public required Scale ArrowCapLengthScale { get; init; }

	/// <inheritdoc/>
	public override required Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor FillColor { get; init; }

	/// <summary>
	/// Indicates arrow line cells.
	/// </summary>
	public required Absolute[] TrailCells { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawCapsuleWithArrowLine(
			Cells,
			TrailCells,
			CapsuleSizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			ArrowCapLengthScale,
			HalfArrowCapRotationDegrees,
			canvas.Templates[TemplateIndex].Mapper
		);
}
