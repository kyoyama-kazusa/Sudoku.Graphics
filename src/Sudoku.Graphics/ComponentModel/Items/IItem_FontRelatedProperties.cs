namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a type that includes font-related properties.
/// </summary>
public interface IItem_FontRelatedProperties
{
	/// <summary>
	/// Indicates the font name.
	/// </summary>
	string FontName { get; init; }

	/// <summary>
	/// Indicates font size scale.
	/// </summary>
	Scale FontSizeScale { get; init; }

	/// <summary>
	/// Indicates the font weight. By default it's <see cref="SKFontStyleWeight.Normal"/>.
	/// </summary>
	/// <seealso cref="SKFontStyleWeight.Normal"/>
	SKFontStyleWeight FontWeight { get; init; }

	/// <summary>
	/// Indicates the font width. By default it's <see cref="SKFontStyleWidth.Normal"/>.
	/// </summary>
	/// <seealso cref="SKFontStyleWidth.Normal"/>
	SKFontStyleWidth FontWidth { get; init; }

	/// <summary>
	/// Indicates the font slant. By default it's <see cref="SKFontStyleSlant.Upright"/>.
	/// </summary>
	/// <seealso cref="SKFontStyleSlant.Upright"/>
	SKFontStyleSlant FontSlant { get; init; }
}
