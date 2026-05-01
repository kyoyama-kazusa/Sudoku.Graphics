namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell mark item.
/// </summary>
public abstract record CellMarkItem :
	Item,
	IItem_CellProperty,
	IItem_FontRelatedProperties,
	IItem_MarkRelatedProperties
{
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

	/// <inheritdoc/>
	public required Absolute Cell { get; init; }

	/// <inheritdoc/>
	public virtual Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates stroke width scale.
	/// </summary>
	public virtual Scale StrokeWidthScale { get; init; }

	/// <summary>
	/// Indicates corner radius scale.
	/// </summary>
	public virtual Scale CornerRadiusScale { get; init; }

	/// <inheritdoc/>
	public virtual SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public virtual SerializableColor FillColor { get; init; }

	/// <inheritdoc/>
	string IItem_FontRelatedProperties.FontName { get => TextFontName!; init => TextFontName = value; }

	/// <inheritdoc/>
	Scale IItem_FontRelatedProperties.FontSizeScale { get => SizeScale; init => SizeScale = value; }
}
