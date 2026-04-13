namespace Sudoku.Graphics.UI.ViewModels;

internal sealed partial class CreateNewCanvasWindowViewModel : ObservableObject
{
	public CreateNewCanvasWindowViewModel(ICloseService closeService, Action<ICloseService, bool?> closeAction)
	{
		OkCommand = new RelayCommand(() => closeAction(closeService, true));
		CancelCommand = new RelayCommand(() => closeAction(closeService, false));

		Tabs = [
			new CreateNewCanvas_StandardTabItemViewModel(),
			new CreateNewCanvas_LatinSquareTabItemViewModel()
		];
		SelectedTab = Tabs[0];
	}


	/// <summary>
	/// Indicates selected tab.
	/// </summary>
	[ObservableProperty]
	public partial CreateNewCanvas_TabItemViewModel? SelectedTab { get; set; }

	/// <summary>
	/// Indicates tabs.
	/// </summary>
	public ObservableCollection<CreateNewCanvas_TabItemViewModel> Tabs { get; }

	public ICommand OkCommand { get; }

	public ICommand CancelCommand { get; }


	public CreateNewCanvasWindowResult? GetResult() => SelectedTab?.GetResult();
}
