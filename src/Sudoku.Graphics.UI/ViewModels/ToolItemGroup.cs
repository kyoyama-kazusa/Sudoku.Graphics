namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a group of <see cref="ToolItem"/> instances.
/// </summary>
/// <seealso cref="ToolItem"/>
[ContentProperty(nameof(Items))]
public sealed partial class ToolItemGroup : ObservableObject
{
	/// <summary>
	/// Indicates the title of the group.
	/// </summary>
	[ObservableProperty]
	public partial string Title { get; set; }

	/// <summary>
	/// Indicates the inner items.
	/// </summary>
	[ObservableProperty]
	public partial ObservableCollection<ToolItem> Items { get; set; } = [];
}
