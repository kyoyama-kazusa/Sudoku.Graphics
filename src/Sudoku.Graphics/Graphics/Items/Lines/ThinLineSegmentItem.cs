namespace Sudoku.Graphics.Items.Lines;

/// <summary>
/// Represents a thin line segment item.
/// </summary>
public sealed record ThinLineSegmentItem() : LineSegmentItem(false)
{
	/// <inheritdoc/>
	public required override Absolute Cell1 { get; init; }

	/// <inheritdoc/>
	public required override Absolute Cell2 { get; init; }

	/// <inheritdoc/>
	public required override Scale LineWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor LineColor { get; init; }

	/// <inheritdoc/>
	public required override LineDashSequence LineDashSequence { get; init; }
}
