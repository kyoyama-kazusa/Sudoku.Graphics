namespace Sudoku.Graphics.UI.Views.Controls;

public sealed class DigitSelectorPanelSelectedDigitChangedEventArgs(int digit)
{
	/// <summary>
	/// Indicates the digit.
	/// </summary>
	public int Digit { get; } = digit;
}
