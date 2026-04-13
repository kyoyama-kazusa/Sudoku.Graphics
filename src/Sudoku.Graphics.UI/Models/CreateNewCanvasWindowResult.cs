namespace Sudoku.Graphics.UI.Models;

/// <summary>
/// Defines a canvas result model, created after closed the window <see cref="CreateNewCanvasWindow"/>.
/// </summary>
/// <param name="CellSize">The size of cell.</param>
/// <param name="Margin">The margin.</param>
/// <param name="TemplateSize">The template size.</param>
/// <param name="BlockRowsCount">The number of rows in a block.</param>
/// <param name="BlockColumnsCount">The number of columns in a block.</param>
/// <seealso cref="CreateNewCanvasWindow"/>
public abstract record CreateNewCanvasWindowResult(float CellSize, float Margin, GridTemplateSize TemplateSize)
{
	/// <summary>
	/// Creates a template via the current instance.
	/// </summary>
	/// <returns>The template instance.</returns>
	public abstract Template CreateTemplate();
}

/// <summary>
/// Defines a model that is created with <see cref="StandardTemplate"/>.
/// </summary>
/// <param name="CellSize"><inheritdoc cref="CreateNewCanvasWindowResult.CellSize" path="/summary"/></param>
/// <param name="Margin"><inheritdoc cref="CreateNewCanvasWindowResult.Margin" path="/summary"/></param>
/// <param name="TemplateSize"><inheritdoc cref="CreateNewCanvasWindowResult.TemplateSize" path="/summary"/></param>
/// <param name="BlockRowsCount">The number of rows in a block.</param>
/// <param name="BlockColumnsCount">The number of columns in a block.</param>
/// <seealso cref="StandardTemplate"/>
public sealed record StandardCreateNewCanvasWindowResult(
	float CellSize,
	float Margin,
	GridTemplateSize TemplateSize,
	Relative BlockRowsCount,
	Relative BlockColumnsCount
) : CreateNewCanvasWindowResult(CellSize, Margin, TemplateSize)
{
	/// <inheritdoc/>
	public override StandardTemplate CreateTemplate()
	{
		var mapper = new PointMapper { CellSize = CellSize, Margin = Margin, TemplateSize = TemplateSize };
		return new(BlockRowsCount, BlockColumnsCount, mapper)
		{
			ThickLineColor = SKColors.Black,//Config
			ThickLineWidth = 0.06M,//Config
			ThickLineDashSequence = [],//Config
			ThinLineColor = SKColors.Black,//Config
			ThinLineWidth = 0.0225M,//Config
			ThinLineDashSequence = []//Config
		};
	}
}
