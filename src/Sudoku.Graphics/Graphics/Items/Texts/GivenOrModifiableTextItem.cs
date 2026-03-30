namespace Sudoku.Graphics.Items.Texts;

/// <summary>
/// Represents a text item that displays a given or a modifiable digit.
/// </summary>
public abstract record GivenOrModifiableTextItem : TextItem, IItem_CellProperty
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Text_Given;

	/// <inheritdoc/>
	public required Absolute Cell { get; init; }


	/// <inheritdoc/>
	protected internal sealed override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawOutlinedTextToCell(
			Text,
			Cell,
			FontName,
			FontSizeScale,
			0M,
			FontWeight,
			FontWidth,
			FontSlant,
			SKColors.Transparent,
			Color,
			0,
			Direction8.None,
			canvas.Templates[TemplateIndex].Mapper
		);
}
