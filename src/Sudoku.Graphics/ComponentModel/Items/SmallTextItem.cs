namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents small text item.
/// </summary>
public abstract class SmallTextItem : BigSmallTextItem
{
	/// <summary>
	/// Indicates the size that a cell will be divided into. Like 3 for 3 * 3 subgrid in a cell,
	/// which means a cell will be filled with at most 9 small text strings.
	/// </summary>
	public required Relative CellSplitSize { get; init; }

	/// <summary>
	/// Indicates the inner position of the cell having been divided by property <see cref="CellSplitSize"/>.
	/// </summary>
	/// <seealso cref="CellSplitSize"/>
	public required Absolute CellInnerPosition { get; init; }


	/// <inheritdoc/>
	public sealed override bool Equals([NotNullWhen(true)] Item? other)
		=> other is SmallTextItem comparer
		&& TemplateIndex == comparer.TemplateIndex && Text == comparer.Text
		&& Cell == comparer.Cell && Color == comparer.Color
		&& CellSplitSize == comparer.CellSplitSize && CellInnerPosition == comparer.CellInnerPosition;

	/// <inheritdoc/>
	public sealed override int GetHashCode()
		=> HashCode.Combine(EqualityContract, TemplateIndex, Text, Cell, CellSplitSize, CellInnerPosition, Color);

	/// <inheritdoc/>
	protected sealed override void PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(TemplateIndex)} = {TemplateIndex}, ");
		builder.Append($"{nameof(Text)} = {Text}, ");
		builder.Append($"{nameof(Cell)} = {Cell}, ");
		builder.Append($"{nameof(CellSplitSize)} = {CellSplitSize}, ");
		builder.Append($"{nameof(CellInnerPosition)} = {CellInnerPosition}, ");
		builder.Append($"{nameof(Color)} = {Color}");
	}

	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		// The main idea on drawing candidates is to find for the number of rows and columns in a cell should be drawn,
		// accommodating all possible candidate values.
		// The general way is to divide a cell into <c>n * n</c> subcells, in order to fill with each candidate value.
		// Here variable <c>splitSize</c> represents the variable <c>n</c> (for <c>n * n</c> subcells).

		var options = canvas.Options;
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		using var typeface = SKTypeface.FromFamilyName(
			options.SmallTextFontName.Resolve(options),
			options.SmallTextFontWeight.Resolve(options),
			options.SmallTextFontWidth.Resolve(options),
			options.SmallTextFontSlant.Resolve(options)
		);
		var cellTopLeft = mapper.GetPoint(Cell, CellAlignment.TopLeft);
		var candidateSize = mapper.CellSize / CellSplitSize;
		var candidateRowIndex = CellInnerPosition / CellSplitSize;
		var candidateColumnIndex = CellInnerPosition % CellSplitSize;
		var factSize = options.SmallTextFontSizeScale.Resolve(options).Measure(mapper.CellSize) / CellSplitSize;
		using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
		using var textPaint = new SKPaint { Color = Color };
		var offset = textFont.MeasureText(Text, textPaint);
		canvas.BackingCanvas.DrawText(
			Text,
			cellTopLeft
				+ new SKPoint(candidateColumnIndex * candidateSize, candidateRowIndex * candidateSize) // Adjust to candidate position
				+ new SKPoint(candidateSize / 2, candidateSize / 2) // Adjust to candidate center
				+ new SKPoint(0, offset / (2 * Text.Length)) // Offset adjustment
				+ new SKPoint(0, candidateSize / 12), // Manual adjustment
			SKTextAlign.Center,
			textFont,
			textPaint
		);
	}
}
