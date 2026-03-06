namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents a text item that displays a given or a modifiable digit.
/// </summary>
public abstract record GivenOrModifiableTextItem :
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
	public override ItemType Type => ItemType.GivenText;

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
	protected internal sealed override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawTextToCell(
			Text,
			Cell,
			FontName,
			FontSizeScale,
			FontWeight,
			FontWidth,
			FontSlant,
			Color,
			0,
			Direction8.None,
			canvas.Templates[TemplateIndex].Mapper
		);
}
