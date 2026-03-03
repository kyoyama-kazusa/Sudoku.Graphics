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
	public virtual string? TextFontName { get; init; }

	/// <inheritdoc/>
	public required Absolute Cell { get; init; }

	/// <inheritdoc/>
	public Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates stroke width scale.
	/// </summary>
	public Scale StrokeWidthScale { get; init; }

	/// <summary>
	/// Indicates corner radius scale.
	/// </summary>
	public Scale CornerRadiusScale { get; init; }

	/// <inheritdoc/>
	public SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public SerializableColor FillColor { get; init; }

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
		&& StrokeWidthScale == comparer.StrokeWidthScale && CornerRadiusScale == comparer.CornerRadiusScale
		&& StrokeColor == comparer.StrokeColor && FillColor == comparer.FillColor;

	/// <inheritdoc/>
	public sealed override int GetHashCode()
	{
		var hashCode = new HashCode();
		hashCode.Add(EqualityContract);
		hashCode.Add(TemplateIndex);
		hashCode.Add(Cell);
		hashCode.Add(SizeScale);
		hashCode.Add(TextFontName);
		hashCode.Add(StrokeWidthScale);
		hashCode.Add(CornerRadiusScale);
		hashCode.Add(StrokeColor);
		hashCode.Add(FillColor);
		return hashCode.ToHashCode();
	}

	/// <inheritdoc/>
	protected sealed override void PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(TemplateIndex)} = {TemplateIndex}, ");
		builder.Append($"{nameof(Cell)} = {Cell}, ");
		builder.Append($"{nameof(SizeScale)} = {SizeScale}, ");
		builder.Append($"{nameof(TextFontName)} = \"{TextFontName}\", ");
		builder.Append($"{nameof(StrokeWidthScale)} = {StrokeWidthScale}, ");
		builder.Append($"{nameof(StrokeColor)} = {StrokeColor}, ");
		builder.Append($"{nameof(CornerRadiusScale)} = {CornerRadiusScale}, ");
		builder.Append($"{nameof(FillColor)} = {FillColor}");
	}
}
