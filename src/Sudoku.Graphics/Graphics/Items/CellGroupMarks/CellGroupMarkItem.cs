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

	/// <inheritdoc/>
	public virtual SKFontStyleWeight FontWeight { get; init; }

	/// <inheritdoc/>
	public virtual SKFontStyleWidth FontWidth { get; init; }

	/// <inheritdoc/>
	public virtual SKFontStyleSlant FontSlant { get; init; }

	/// <summary>
	/// Indicates the cells.
	/// </summary>
	public required Absolute[] Cells { get; init; }

	/// <summary>
	/// Indicates stroke width scale.
	/// </summary>
	public virtual Scale StrokeWidthScale { get; init; }

	/// <summary>
	/// Indicates corner radius scale.
	/// </summary>
	public virtual Scale CornerRadiusScale { get; init; }

	/// <inheritdoc/>
	public virtual Scale FontSizeScale { get; init; }

	/// <inheritdoc/>
	public virtual SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public virtual SerializableColor FillColor { get; init; }

	/// <inheritdoc/>
	string IItem_FontRelatedProperties.FontName { get => TextFontName!; init => TextFontName = value; }

	/// <inheritdoc/>
	Scale IItem_MarkRelatedProperties.SizeScale { get; init; }
}
