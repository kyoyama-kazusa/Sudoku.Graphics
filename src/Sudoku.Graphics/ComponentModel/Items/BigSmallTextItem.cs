namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a text item that is bound with a big or small text.
/// </summary>
public abstract class BigSmallTextItem : TextItem
{
	/// <summary>
	/// Indicates the target template index.
	/// </summary>
	public required int TemplateIndex { get; init; }

	/// <summary>
	/// Indicates the text to be drawn.
	/// </summary>
	public required string Text { get; init; }

	/// <summary>
	/// Indicates the cell to be drawn.
	/// </summary>
	public required Absolute Cell { get; init; }

	/// <summary>
	/// Indicates the color to be used.
	/// </summary>
	public required SerializableColor Color { get; init; }
}
