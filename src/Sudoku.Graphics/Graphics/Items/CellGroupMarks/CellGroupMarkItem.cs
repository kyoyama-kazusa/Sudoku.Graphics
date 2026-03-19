namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents cell group mark item.
/// </summary>
public abstract record CellGroupMarkItem : Item, IItem_FontRelatedProperties, IItem_MarkRelatedProperties, IItem_TemplateIndexProperty
{
	/// <inheritdoc/>
	public required int TemplateIndex { get; init; }

	/// <summary>
	/// Indicates text font.
	/// </summary>
	public virtual string? TextFontName { get; init; }

	/// <summary>
	/// Indicates the cells.
	/// </summary>
	public required Absolute[] Cells { get; init; }

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
	Scale IItem_FontRelatedProperties.FontSizeScale { get; init; }

	/// <inheritdoc/>
	Scale IItem_MarkRelatedProperties.SizeScale { get; init; }
}
