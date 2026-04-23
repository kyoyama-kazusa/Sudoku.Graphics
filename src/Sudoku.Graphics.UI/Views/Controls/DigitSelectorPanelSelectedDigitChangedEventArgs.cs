namespace Sudoku.Graphics.UI.Views.Controls;

public sealed class DigitSelectorPanelSelectedDigitChangedEventArgs(int digit, OperationHandlerContext context) :
	ContextBasedEventArgs(context)
{
	/// <summary>
	/// Indicates the digit.
	/// </summary>
	public int Digit { get; } = digit;
}
