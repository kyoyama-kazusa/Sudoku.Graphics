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
