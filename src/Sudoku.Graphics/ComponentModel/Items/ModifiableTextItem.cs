namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents modifiable text.
/// </summary>
public sealed class ModifiableTextItem :
	Item,
	IItem_CellProperty,
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
	public override ItemType Type => ItemType.ModifiableText;

	/// <inheritdoc/>
	public SKFontStyleWeight FontWeight { get; init; } = SKFontStyleWeight.Normal;

	/// <inheritdoc/>
	public SKFontStyleWidth FontWidth { get; init; } = SKFontStyleWidth.Normal;

	/// <inheritdoc/>
	public SKFontStyleSlant FontSlant { get; init; } = SKFontStyleSlant.Upright;

	/// <inheritdoc/>
	public required Absolute Cell { get; init; }

	/// <inheritdoc/>
	public SerializableColor Color { get; init; }

	/// <inheritdoc/>
	public required Scale FontSizeScale { get; init; }

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(ModifiableTextItem);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Item? other)
		=> other is ModifiableTextItem comparer
		&& TemplateIndex == comparer.TemplateIndex && Text == comparer.Text
		&& Cell == comparer.Cell && Color == comparer.Color
		&& FontName == comparer.FontName && FontSizeScale == comparer.FontSizeScale
		&& FontWeight == comparer.FontWeight && FontWidth == comparer.FontWidth && FontSlant == comparer.FontSlant;

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		var hashCode = new HashCode();
		hashCode.Add(EqualityContract);
		hashCode.Add(TemplateIndex);
		hashCode.Add(Text);
		hashCode.Add(Cell);
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
		builder.Append($"{nameof(Cell)} = {Cell}, ");
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
		using var typeface = SKTypeface.FromFamilyName(FontName, FontWeight, FontWidth, FontSlant);
		var factSize = FontSizeScale.Measure(mapper.CellSize);
		using var textFont = new SKFont(typeface, factSize) { Subpixel = true };
		using var textPaint = new SKPaint { Color = Color };
		var offset = textFont.MeasureText(Text, textPaint);
		canvas.BackingCanvas.DrawText(
			Text,
			mapper.GetPoint(Cell, Alignment.Center)
				+ new SKPoint(0, offset / (2 * Text.Length)) // Offset adjustment
				+ new SKPoint(0, mapper.CellSize / 12), // Manual adjustment
			SKTextAlign.Center,
			textFont,
			textPaint
		);
	}
}
