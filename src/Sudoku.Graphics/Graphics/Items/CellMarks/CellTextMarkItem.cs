namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents cell text mark item.
/// </summary>
public abstract class CellTextMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the direction. By default it's <see cref="Direction8.Up"/>.
	/// </summary>
	/// <seealso cref="Direction8.Up"/>
	public Direction8 Direction { get; init; }

	/// <inheritdoc/>
	public sealed override required string? TextFontName { get; init; }

	/// <summary>
	/// Indicates the printing text.
	/// </summary>
	protected abstract string PrintingText { get; }


	/// <inheritdoc/>
	protected internal sealed override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawOutlinedTextToCell(
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
			Direction.RotationDegrees,
			mapper
		);
	}
}
