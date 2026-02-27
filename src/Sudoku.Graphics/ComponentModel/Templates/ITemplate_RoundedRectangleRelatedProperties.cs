namespace Sudoku.ComponentModel.Templates;

/// <summary>
/// Represents a grid template that supports drawing border lines as a rounded rectangle.
/// </summary>
public interface ITemplate_RoundedRectangleRelatedProperties
{
	/// <summary>
	/// Indicates whether the border should be drawn as a rounded rectangle. By default it's <see langword="true"/>.
	/// </summary>
	bool IsBorderRoundedRectangle { get; init; }

	/// <summary>
	/// Indicates corner radius ratio of rounded rectangle of border lines, relative to cell size.
	/// </summary>
	Scale BorderCornerRadius { get; init; }
}
