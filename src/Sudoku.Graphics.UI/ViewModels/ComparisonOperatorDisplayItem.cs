namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents comparison operator display item.
/// </summary>
public sealed class ComparisonOperatorDisplayItem : ITextDisplayItem
{
	/// <summary>
	/// Indicates the operator string.
	/// </summary>
	public string OperatorString => Operator.Text;

	/// <summary>
	/// Indicates the operator.
	/// </summary>
	public ComparisonOperator Operator { get; set; }

	/// <inheritdoc/>
	string ITextDisplayItem.Text => OperatorString;
}
