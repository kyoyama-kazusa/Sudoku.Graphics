namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents cell text mark item.
/// </summary>
public abstract record CellTextMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the aligned direction.
	/// </summary>
	public virtual Direction8 AlignedDirection { get; init; }

	/// <summary>
	/// Indicates the rotation direction. By default it's <see cref="Direction8.Up"/> (upright, no rotation).
	/// </summary>
	/// <seealso cref="Direction8.Up"/>
	public Direction8 RotationDirection { get; init; }

	/// <inheritdoc/>
	public sealed override required string? TextFontName { get; init; }

	/// <summary>
	/// Indicates the printing text.
	/// </summary>
	protected abstract string PrintingText { get; }


	/// <inheritdoc/>
	protected internal sealed override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawOutlinedTextToCell(
			PrintingText,
			Cell,
			TextFontName ?? throw new InvalidOperationException("Expected a valid text font name."),
			SizeScale,
			StrokeWidthScale,
			((IItem_FontRelatedProperties)this).FontWeight,
			((IItem_FontRelatedProperties)this).FontWidth,
			((IItem_FontRelatedProperties)this).FontSlant,
			StrokeColor,
			FillColor,
			RotationDirection.RotationDegrees,
			AlignedDirection,
			canvas.Templates[TemplateIndex].Mapper
		);
}
