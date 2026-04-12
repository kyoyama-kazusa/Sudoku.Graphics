namespace Sudoku.Graphics.UI.Models;

public sealed record CanvasCreateResult(
	float CellSize,
	float Margin,
	GridTemplateSize TemplateSize,
	Relative BlockRowsCount,
	Relative BlockColumnsCount
);
