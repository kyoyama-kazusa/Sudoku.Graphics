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
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawTextToCandidate(
			Text,
			CandidatePosition,
			FontName,
			FontSizeScale,
			FontWeight,
			FontWidth,
			FontSlant,
			Color,
			mapper
		);
	}
}
