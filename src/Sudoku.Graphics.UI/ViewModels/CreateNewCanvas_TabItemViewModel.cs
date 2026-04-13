namespace Sudoku.Graphics.UI.ViewModels;

internal abstract partial class CreateNewCanvas_TabItemViewModel : ObservableObject
{
	/// <summary>
	/// Indicates the title.
	/// </summary>
	public abstract string Title { get; }

	[ObservableProperty]
	public partial string TargetPictureSizeString { get; set; }


	public abstract CreateNewCanvasWindowResult? GetResult();
}
