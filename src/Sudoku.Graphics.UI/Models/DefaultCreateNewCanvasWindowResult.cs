namespace Sudoku.Graphics.UI.Models;

/// <summary>
/// Defines a model that is created with <see cref="DefaultTemplate"/>.
/// </summary>
/// <param name="CellSize"><inheritdoc cref="CreateNewCanvasWindowResult.CellSize" path="/summary"/></param>
/// <param name="Margin"><inheritdoc cref="CreateNewCanvasWindowResult.Margin" path="/summary"/></param>
/// <param name="TemplateSize"><inheritdoc cref="CreateNewCanvasWindowResult.TemplateSize" path="/summary"/></param>
/// <param name="BlockRowsCount">The number of rows in a block.</param>
/// <param name="BlockColumnsCount">The number of columns in a block.</param>
/// <seealso cref="DefaultTemplate"/>
public sealed record DefaultCreateNewCanvasWindowResult(
	float CellSize,
	float Margin,
	GridTemplateSize TemplateSize
) : CreateNewCanvasWindowResult(CellSize, Margin, TemplateSize)
{
	/// <inheritdoc/>
	public override DefaultTemplate CreateTemplate()
	{
		var mapper = new PointMapper { CellSize = CellSize, Margin = Margin, TemplateSize = TemplateSize };
		return new()
		{
			Mapper = mapper,
			ThickLineColor = SKColors.Black,//Config
			ThickLineWidth = 0.06M,//Config
			ThickLineDashSequence = [],//Config
			ThinLineColor = SKColors.Black,//Config
			ThinLineWidth = 0.0225M,//Config
			ThinLineDashSequence = []//Config
		};
	}
}
