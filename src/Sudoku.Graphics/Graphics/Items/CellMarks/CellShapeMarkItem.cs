namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell shape item.
/// </summary>
public abstract record CellShapeMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public required sealed override Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public required sealed override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required sealed override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public sealed override SerializableColor FillColor { get; init; }
}
