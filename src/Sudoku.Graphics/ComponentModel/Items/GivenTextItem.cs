namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents given text.
/// </summary>
public sealed class GivenTextItem :
	Item,
	IItem_CellProperty,
	IItem_ColorProperty,
	IItem_TemplateIndexProperty,
	IItem_TextProperty
{
	/// <inheritdoc/>
	public required int TemplateIndex { get; init; }

	/// <inheritdoc/>
	public required string Text { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.GivenText;

	/// <inheritdoc/>
	public required Absolute Cell { get; init; }

	/// <inheritdoc/>
	public SerializableColor Color { get; init; }

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(GivenTextItem);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Item? other)
		=> other is GivenTextItem comparer
		&& TemplateIndex == comparer.TemplateIndex && Text == comparer.Text
		&& Cell == comparer.Cell && Color == comparer.Color;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(EqualityContract, TemplateIndex, Text, Cell, Color);

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(TemplateIndex)} = {TemplateIndex}, ");
		builder.Append($"{nameof(Text)} = {Text}, ");
		builder.Append($"{nameof(Cell)} = {Cell}, ");
		builder.Append($"{nameof(Color)} = {Color}");
	}

	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var options = canvas.Options;
		var mapper = template.Mapper;
		using var typeface = SKTypeface.FromFamilyName(
			options.BigTextFontName.Resolve(options),
			options.BigTextFontWeight.Resolve(options),
			options.BigTextFontWidth.Resolve(options),
			options.BigTextFontSlant.Resolve(options)
		);

		var factSize = options.BigTextFontSizeScale.Resolve(options).Measure(mapper.CellSize);
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
