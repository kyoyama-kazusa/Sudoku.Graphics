namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a type that includes mark-related properties.
/// </summary>
public interface IItem_MarkRelatedProperties
{
	/// <summary>
	/// Indicates size scale of mark.
	/// </summary>
	Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates the stroke color.
	/// </summary>
	SerializableColor StrokeColor { get; init; }

	/// <summary>
	/// Indicates the fill color.
	/// </summary>
	SerializableColor FillColor { get; init; }
}
