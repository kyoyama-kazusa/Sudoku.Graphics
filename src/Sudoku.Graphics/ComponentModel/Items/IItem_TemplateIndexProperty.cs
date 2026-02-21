namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a type that includes <see cref="TemplateIndex"/> property.
/// </summary>
public interface IItem_TemplateIndexProperty
{
	/// <summary>
	/// Indicates the target template index.
	/// </summary>
	int TemplateIndex { get; init; }
}
