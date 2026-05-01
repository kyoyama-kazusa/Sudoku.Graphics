namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair mark item.
/// </summary>
public abstract record CellPairMarkItem : Item, IItem_FontRelatedProperties, IItem_MarkRelatedProperties
{
	/// <summary>
	/// Indicates text font.
	/// </summary>
	public virtual string? TextFontName { get; init; }

	/// <summary>
	/// Indicates the first cell.
	/// </summary>
	public required Absolute Cell1 { get; init; }

	/// <summary>
	/// Indicates the second cell.
	/// </summary>
	public required Absolute Cell2 { get; init; }

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
