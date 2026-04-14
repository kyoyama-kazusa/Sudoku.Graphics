namespace Sudoku.Graphics.UI.ViewModels;

public sealed partial class ToolItem : ObservableObject
{
	public string ItemString => LocalizationResources.ResourceManager.GetString($"{nameof(ItemType)}_{ItemType}") ?? string.Empty;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(ItemString))]
	public partial ItemType ItemType { get; set; }

	[ObservableProperty]
	public partial ImageSource? Icon { get; set; }
}
