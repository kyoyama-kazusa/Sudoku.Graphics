namespace Sudoku.Graphics.Items.Texts;

/// <summary>
/// Represents a text item that displays a given or a modifiable digit.
/// </summary>
public abstract record GivenOrModifiableTextItem :
	TextItem,
	IItem_CellProperty,
	IItem_ColorProperty,
	IItem_FontRelatedProperties,
	IItem_TemplateIndexProperty,
	IItem_TextProperty
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.GivenText;

	/// <inheritdoc/>
	public required Absolute Cell { get; init; }


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
