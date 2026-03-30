namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a battenburg mark into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="sizeScale">The scale of size, related to cell size.</param>
		/// <param name="colorA">Indicates color 1 (top-left and bottom-right).</param>
		/// <param name="colorB">Indicates color 2 (top-right and bottom-left).</param>
		/// <param name="strokeColor">The stroke line color.</param>
		/// <param name="strokeWidthScale">The scale of stroke width, related to cell size.</param>
		/// <param name="cornerRadiiScale">The scale of corner radii.</param>
		/// <param name="mapper">The mapper instance.</param>
		public void DrawBattenburgToCell(
			Absolute cell,
			Scale sizeScale,
			SerializableColor colorA,
			SerializableColor colorB,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			Scale[]? cornerRadiiScale,
			PointMapper mapper
		) => @this.DrawBattenburg_Generic(
			mapper.GetPoint(cell, Alignment.Center),
			sizeScale,
			colorA,
			colorB,
			strokeColor,
			strokeWidthScale,
			cornerRadiiScale,
			mapper
		);
	}
}
