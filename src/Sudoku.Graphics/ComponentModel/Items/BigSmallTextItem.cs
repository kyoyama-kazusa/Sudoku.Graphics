namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a text item that is bound with a big or small text.
/// Outside labels can also created here.
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
	/// Indicates the color to be used.
	/// </summary>
	public SerializableColor Color { get; init; }


	/// <summary>
	/// Unsafe accessor to field of <see cref="Color"/> property.
	/// </summary>
	/// <param name="instance">The instance.</param>
	/// <returns>The reference to the backing field.</returns>
	[UnsafeAccessor(UnsafeAccessorKind.Field, Name = $"<{nameof(Color)}>k__BackingField")]
	protected static extern ref SerializableColor GetColorFieldUnsafe(BigSmallTextItem instance);
}
