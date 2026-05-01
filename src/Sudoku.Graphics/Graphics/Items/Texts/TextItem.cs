namespace Sudoku.Graphics.Items.Texts;

/// <summary>
/// Represents a text item.
/// </summary>
public abstract record TextItem :
	Item,
	IItem_ColorProperty,
	IItem_FontRelatedProperties,
	IItem_TextProperty
{
	/// <inheritdoc/>
	public required string Text { get; init; }

	/// <inheritdoc/>
	public required string FontName { get; init; }

	/// <inheritdoc/>
	public SKFontStyleWeight FontWeight { get; init; } = SKFontStyleWeight.Normal;

	/// <inheritdoc/>
	public SKFontStyleWidth FontWidth { get; init; } = SKFontStyleWidth.Normal;

	/// <inheritdoc/>
	public SKFontStyleSlant FontSlant { get; init; } = SKFontStyleSlant.Upright;

	/// <inheritdoc/>
	public required SerializableColor Color { get; init; }

	/// <inheritdoc/>
	public required Scale FontSizeScale { get; init; }
}
