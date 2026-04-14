namespace Sudoku.Graphics.UI.ViewModels;

[ContentProperty(nameof(Items))]
public sealed partial class ToolItemGroup : ObservableObject
{
	[ObservableProperty]
	public partial string Title { get; set; }

	[ObservableProperty]
	public partial ObservableCollection<ToolItem> Items { get; set; } = [];
}
