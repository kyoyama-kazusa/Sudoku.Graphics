namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell circle mark item.
/// </summary>
public sealed record CellCircleMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Circle;

	/// <inheritdoc/>
	public required override Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public override SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawCircleTo(
			Cell,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			canvas.Templates[TemplateIndex].Mapper
		);
}
