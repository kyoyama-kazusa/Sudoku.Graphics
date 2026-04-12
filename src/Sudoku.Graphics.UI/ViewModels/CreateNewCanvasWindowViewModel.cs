namespace Sudoku.Graphics.UI.ViewModels;

internal sealed partial class CreateNewCanvasWindowViewModel : ObservableObject
{
	public CreateNewCanvasWindowViewModel(ICloseService closeService, Action<ICloseService, bool?> closeAction)
	{
		OkCommand = new RelayCommand(() => closeAction(closeService, true));
		CancelCommand = new RelayCommand(() => closeAction(closeService, false));
	}


	[ObservableProperty]
	public partial string CellSizeString { get; set; } = "120";

	[ObservableProperty]
	public partial string MarginString { get; set; } = "15";

	[ObservableProperty]
	public partial string GridRowsCountString { get; set; } = "9";

	[ObservableProperty]
	public partial string GridColumnsCountString { get; set; } = "9";

	[ObservableProperty]
	public partial string BlockRowsCountString { get; set; } = "3";

	[ObservableProperty]
	public partial string BlockColumnsCountString { get; set; } = "3";

	[ObservableProperty]
	public partial string VectorTopString { get; set; } = "0";

	[ObservableProperty]
	public partial string VectorBottomString { get; set; } = "0";

	[ObservableProperty]
	public partial string VectorLeftString { get; set; } = "0";

	[ObservableProperty]
	public partial string VectorRightString { get; set; } = "0";

	public ICommand OkCommand { get; }

	public ICommand CancelCommand { get; }


	public CanvasCreateResult? GetCanvasCreateResult()
	{
		if (!float.TryParse(CellSizeString, out var cellSize)
			|| !float.TryParse(MarginString, out var margin)
			|| !int.TryParse(GridRowsCountString, out var gridRowsCount)
			|| !int.TryParse(GridColumnsCountString, out var gridColumnsCount)
			|| !int.TryParse(BlockRowsCountString, out var blockRowsCount)
			|| !int.TryParse(BlockColumnsCountString, out var blockColumnsCount)
			|| !int.TryParse(VectorTopString, out var vectorTop)
			|| !int.TryParse(VectorBottomString, out var vectorBottom)
			|| !int.TryParse(VectorLeftString, out var vectorLeft)
			|| !int.TryParse(VectorRightString, out var vectorRight))
		{
			return null;
		}

		var vector = new GridTemplateSize
		{
			RowsCount = gridRowsCount,
			ColumnsCount = gridColumnsCount,
			Vector = new(vectorLeft, vectorTop, vectorRight, vectorBottom)
		};
		return new(cellSize, margin, vector, blockRowsCount, blockColumnsCount);
	}
}
