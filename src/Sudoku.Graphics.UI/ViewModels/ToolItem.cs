namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a tool item.
/// </summary>
public sealed partial class ToolItem : ObservableObject
{
	/// <summary>
	/// Indicates the string to be displayed.
	/// </summary>
	public string ItemString => LocalizationResources.ResourceManager.GetString($"{nameof(ItemType)}_{ItemType}") ?? string.Empty;

	/// <summary>
	/// Indicates the item type.
	/// </summary>
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(ItemString))]
	public partial ItemType ItemType { get; set; }

	/// <summary>
	/// Indicates the icon to be diplayed.
	/// </summary>
	[ObservableProperty]
	public partial ImageSource? Icon { get; set; }
}
