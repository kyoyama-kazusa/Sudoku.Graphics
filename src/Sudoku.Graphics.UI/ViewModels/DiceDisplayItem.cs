namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a dice display item.
/// </summary>
public sealed partial class DiceDisplayItem : ObservableObject
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(ValueString))]
	public partial int Value { get; set; }

	/// <summary>
	/// Indicates display string of value.
	/// </summary>
	public string ValueString => (Value + 1).ToString();
}
