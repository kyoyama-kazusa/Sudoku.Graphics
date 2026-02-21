namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a text item that is bound with a big or small text.
/// Outside labels can also created here.
/// </summary>
public abstract class BigSmallTextItem : Item, IItem_ColorProperty, IItem_TemplateIndexProperty, IItem_TextProperty
{
	/// <inheritdoc/>
	public required int TemplateIndex { get; init; }

	/// <inheritdoc/>
	public required string Text { get; init; }

	/// <inheritdoc/>
	public SerializableColor Color { get; init; }
}
