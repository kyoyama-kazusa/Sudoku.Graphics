namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents small text item.
/// </summary>
public abstract class SmallTextItem : BigSmallTextItem
{
	/// <summary>
	/// Indicates candidate position to be set.
	/// </summary>
	public required CandidatePosition CandidatePosition { get; init; }


	/// <inheritdoc/>
	public sealed override bool Equals([NotNullWhen(true)] Item? other)
		=> other is SmallTextItem comparer
		&& TemplateIndex == comparer.TemplateIndex && Text == comparer.Text
		&& CandidatePosition == comparer.CandidatePosition && Color == comparer.Color;

	/// <inheritdoc/>
	public sealed override int GetHashCode()
		=> HashCode.Combine(EqualityContract, TemplateIndex, Text, CandidatePosition, Color);

	/// <inheritdoc/>
	protected sealed override void PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(TemplateIndex)} = {TemplateIndex}, ");
		builder.Append($"{nameof(Text)} = {Text}, ");
		builder.Append($"{nameof(CandidatePosition)} = {CandidatePosition}");
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

		var (cell, subgridSize, innerIndex) = CandidatePosition;
		var cellTopLeft = mapper.GetPoint(cell, CellAlignment.TopLeft);
		var candidateSize = mapper.CellSize / subgridSize;
		var candidateRowIndex = innerIndex / subgridSize;
		var candidateColumnIndex = innerIndex % subgridSize;
		var factSize = options.SmallTextFontSizeScale.Resolve(options).Measure(mapper.CellSize) / subgridSize;
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
