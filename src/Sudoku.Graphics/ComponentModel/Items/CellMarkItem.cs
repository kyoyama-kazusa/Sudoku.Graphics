namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a cell mark item.
/// </summary>
public abstract class CellMarkItem :
	Item,
	IItem_CellProperty,
	IItem_FontRelatedProperties,
	IItem_MarkRelatedProperties,
	IItem_TemplateIndexProperty
{
	/// <inheritdoc/>
	public required int TemplateIndex { get; init; }

	/// <summary>
	/// Indicates text font.
	/// </summary>
	public required string? TextFontName { get; init; }

	/// <inheritdoc/>
	public required Absolute Cell { get; init; }

	/// <inheritdoc/>
	public required Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates stroke width scale.
	/// </summary>
	public Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required SerializableColor FillColor { get; init; }

	/// <inheritdoc/>
	string IItem_FontRelatedProperties.FontName { get => TextFontName!; init => TextFontName = value; }

	/// <inheritdoc/>
	SKFontStyleWeight IItem_FontRelatedProperties.FontWeight { get; init; } = SKFontStyleWeight.Normal;

	/// <inheritdoc/>
	SKFontStyleWidth IItem_FontRelatedProperties.FontWidth { get; init; } = SKFontStyleWidth.Normal;

	/// <inheritdoc/>
	SKFontStyleSlant IItem_FontRelatedProperties.FontSlant { get; init; } = SKFontStyleSlant.Upright;

	/// <inheritdoc/>
	Scale IItem_FontRelatedProperties.FontSizeScale { get => SizeScale; init => SizeScale = value; }


	/// <inheritdoc/>
	public sealed override bool Equals([NotNullWhen(true)] Item? other)
		=> other is CellMarkItem comparer && EqualityContract == comparer.EqualityContract
		&& TemplateIndex == comparer.TemplateIndex && Cell == comparer.Cell && SizeScale == comparer.SizeScale
		&& TextFontName == comparer.TextFontName
		&& StrokeWidthScale == comparer.StrokeWidthScale
		&& StrokeColor == comparer.StrokeColor && FillColor == comparer.FillColor;

	/// <inheritdoc/>
	public sealed override int GetHashCode()
		=> HashCode.Combine(EqualityContract, TemplateIndex, Cell, SizeScale, TextFontName, StrokeWidthScale, StrokeColor, FillColor);

	/// <inheritdoc/>
	protected sealed override void PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(TemplateIndex)} = {TemplateIndex}, ");
		builder.Append($"{nameof(Cell)} = {Cell}, ");
		builder.Append($"{nameof(SizeScale)} = {SizeScale}, ");
		builder.Append($"{nameof(TextFontName)} = \"{TextFontName}\", ");
		builder.Append($"{nameof(StrokeWidthScale)} = {StrokeWidthScale}, ");
		builder.Append($"{nameof(StrokeColor)} = {StrokeColor}, ");
		builder.Append($"{nameof(FillColor)} = {FillColor}");
	}
}
