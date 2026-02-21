namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents candidate text.
/// </summary>
public sealed class CandidateTextItem :
	Item,
	IItem_CandidatePositionProperty,
	IItem_ColorProperty,
	IItem_FontRelatedProperties,
	IItem_TemplateIndexProperty,
	IItem_TextProperty
{
	/// <inheritdoc/>
	public required int TemplateIndex { get; init; }

	/// <inheritdoc/>
	public required string Text { get; init; }

	/// <inheritdoc/>
	public required string FontName { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CandidateText;

	/// <inheritdoc/>
	public SKFontStyleWeight FontWeight { get; init; } = SKFontStyleWeight.Normal;

	/// <inheritdoc/>
	public SKFontStyleWidth FontWidth { get; init; } = SKFontStyleWidth.Normal;

	/// <inheritdoc/>
	public SKFontStyleSlant FontSlant { get; init; } = SKFontStyleSlant.Upright;

	/// <inheritdoc/>
	public required Scale FontSizeScale { get; init; }

	/// <inheritdoc/>
	public SerializableColor Color { get; init; }

	/// <inheritdoc/>
	public required CandidatePosition CandidatePosition { get; init; }

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CandidateTextItem);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Item? other)
		=> other is CandidateTextItem comparer
		&& TemplateIndex == comparer.TemplateIndex && Text == comparer.Text
		&& CandidatePosition == comparer.CandidatePosition && Color == comparer.Color
		&& FontName == comparer.FontName && FontSizeScale == comparer.FontSizeScale
		&& FontWeight == comparer.FontWeight && FontWidth == comparer.FontWidth && FontSlant == comparer.FontSlant;

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		var hashCode = new HashCode();
		hashCode.Add(EqualityContract);
		hashCode.Add(TemplateIndex);
		hashCode.Add(Text);
		hashCode.Add(CandidatePosition);
		hashCode.Add(Color);
		hashCode.Add(FontName);
		hashCode.Add(FontSizeScale);
		hashCode.Add(FontWeight);
		hashCode.Add(FontWidth);
		hashCode.Add(FontSlant);
		return hashCode.ToHashCode();
	}

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(TemplateIndex)} = {TemplateIndex}, ");
		builder.Append($"{nameof(Text)} = {Text}, ");
		builder.Append($"{nameof(CandidatePosition)} = {CandidatePosition}, ");
		builder.Append($"{nameof(Color)} = {Color}, ");
		builder.Append($"{nameof(FontName)} = \"{FontName}\", ");
		builder.Append($"{nameof(FontSizeScale)} = {FontSizeScale}, ");
		builder.Append($"{nameof(FontWeight)} = {FontWeight}, ");
		builder.Append($"{nameof(FontWidth)} = {FontWidth}, ");
		builder.Append($"{nameof(FontSlant)} = {FontSlant}");
	}

	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		// The main idea on drawing candidates is to find for the number of rows and columns in a cell should be drawn,
		// accommodating all possible candidate values.
		// The general way is to divide a cell into <c>n * n</c> subcells, in order to fill with each candidate value.
		// Here variable <c>subgridSize</c> represents the variable <c>n</c> (for <c>n * n</c> subcells).

		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		using var typeface = SKTypeface.FromFamilyName(FontName, FontWeight, FontWidth, FontSlant);
		var (_, subgridSize, _) = CandidatePosition;
		var candidateSize = mapper.CellSize / subgridSize;
		var candidateCenterPoint = mapper.GetPoint(CandidatePosition, Alignment.Center);
		var factSize = FontSizeScale.Measure(mapper.CellSize) / subgridSize;
		using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
		using var textPaint = new SKPaint { Color = Color };
		var offset = textFont.MeasureText(Text, textPaint);
		canvas.BackingCanvas.DrawText(
			Text,
			candidateCenterPoint
				+ new SKPoint(0, offset / (2 * Text.Length)) // Offset adjustment
				+ new SKPoint(0, candidateSize / 12), // Manual adjustment
			SKTextAlign.Center,
			textFont,
			textPaint
		);
	}
}
