namespace Sudoku.Graphics.UI.ViewModels;

internal sealed partial class CreateNewCanvas_LatinSquareTabItemViewModel : CreateNewCanvas_TabItemViewModel
{
	public CreateNewCanvas_LatinSquareTabItemViewModel() => CalculateSize();


	/// <inheritdoc/>
	public override string Title => LocalizationResources.CreateNewCanvasWindow_TabPage_LatinSquare;

	[ObservableProperty]
	public partial string CellSizeString { get; set; } = "120";

	[ObservableProperty]
	public partial string MarginString { get; set; } = "15";

	[ObservableProperty]
	public partial string GridRowsCountString { get; set; } = "9";

	[ObservableProperty]
	public partial string GridColumnsCountString { get; set; } = "9";

	[ObservableProperty]
	public partial string VectorTopString { get; set; } = "0";

	[ObservableProperty]
	public partial string VectorBottomString { get; set; } = "0";

	[ObservableProperty]
	public partial string VectorLeftString { get; set; } = "0";

	[ObservableProperty]
	public partial string VectorRightString { get; set; } = "0";


	/// <inheritdoc/>
	public override DefaultCreateNewCanvasWindowResult? GetResult()
	{
		if (!float.TryParse(CellSizeString, out var cellSize)
			|| !float.TryParse(MarginString, out var margin)
			|| !int.TryParse(GridRowsCountString, out var gridRowsCount)
			|| !int.TryParse(GridColumnsCountString, out var gridColumnsCount)
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
		return new(cellSize, margin, vector);
	}

	private void CalculateSize()
	{
		if (GetResult() is not { } result)
		{
			TargetPictureSizeString = LocalizationResources.CreateNewCanvasWindow_TargetPictureSizeStringDefaultValue;
			return;
		}

		var rowsCount = result.TemplateSize.AbsoluteRowsCount;
		var columnsCount = result.TemplateSize.AbsoluteColumnsCount;
		var sizeX = columnsCount * result.CellSize + result.Margin * 2;
		var sizeY = rowsCount * result.CellSize + result.Margin * 2;
		TargetPictureSizeString = $"{sizeX} x {sizeY}";
	}

	partial void OnCellSizeStringChanged(string value) => CalculateSize();

	partial void OnMarginStringChanged(string value) => CalculateSize();

	partial void OnGridRowsCountStringChanged(string value) => CalculateSize();

	partial void OnGridColumnsCountStringChanged(string value) => CalculateSize();

	partial void OnVectorTopStringChanged(string value) => CalculateSize();

	partial void OnVectorBottomStringChanged(string value) => CalculateSize();

	partial void OnVectorLeftStringChanged(string value) => CalculateSize();

	partial void OnVectorRightStringChanged(string value) => CalculateSize();
}
