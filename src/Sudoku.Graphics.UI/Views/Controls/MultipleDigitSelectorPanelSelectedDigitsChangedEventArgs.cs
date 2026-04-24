namespace Sudoku.Graphics.UI.Views.Controls;

public sealed class MultipleDigitSelectorPanelSelectedDigitsChangedEventArgs(int[]? digits, OperationHandlerContext context) :
	ContextBasedEventArgs(context)
{
	/// <summary>
	/// Indicates the digit.
	/// </summary>
	public int[]? Digits { get; } = digits;
}
