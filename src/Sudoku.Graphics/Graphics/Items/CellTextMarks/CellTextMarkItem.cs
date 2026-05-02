namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents cell text mark item.
/// </summary>
public abstract record CellTextMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public required sealed override string? TextFontName { get; init; }

	/// <summary>
	/// Indicates the aligned direction.
	/// </summary>
	public virtual Direction8 AlignedDirection { get; init; }

	/// <summary>
	/// Indicates the rotation direction. By default it's <see cref="Direction8.Up"/> (upright, no rotation).
	/// </summary>
	/// <seealso cref="Direction8.Up"/>
	public Direction8 RotationDirection { get; init; } = Direction8.Up;

	/// <inheritdoc/>
	public override SKFontStyleWeight FontWeight { get; init; } = SKFontStyleWeight.Normal;

	/// <inheritdoc/>
	public override SKFontStyleWidth FontWidth { get; init; } = SKFontStyleWidth.Normal;

	/// <inheritdoc/>
	public override SKFontStyleSlant FontSlant { get; init; } = SKFontStyleSlant.Upright;

	/// <inheritdoc/>
	public sealed override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required sealed override SerializableColor FillColor { get; init; }

	/// <inheritdoc/>
	public required sealed override Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public sealed override Scale StrokeWidthScale { get; init; }

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
			FontWeight,
			FontWidth,
			FontSlant,
			StrokeColor,
			FillColor,
			RotationDirection.RotationDegree,
			AlignedDirection,
			canvas.Mapper
		);
}
