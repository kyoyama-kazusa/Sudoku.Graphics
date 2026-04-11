namespace Sudoku.Graphics.UI.ViewModels;

public sealed record CanvasCreatedResult(
	float CellSize,
	float Margin,
	GridTemplateSize TemplateSize,
	Relative BlockRowsCount,
	Relative BlockColumnsCount
);
