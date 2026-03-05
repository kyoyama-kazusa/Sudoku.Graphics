namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a hexagon symbol into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="sizeScale">The scale of size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="strokeWidthScale">The stroke width scale.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="mapper">The mapper.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when <paramref name="direction"/> is not defined or <see cref="Direction8.None"/>.
		/// </exception>
		/// <seealso cref="Direction8.None"/>
		public void DrawTriangleToCell(
			Absolute cell,
			Direction8 direction,
			Scale sizeScale,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			SerializableColor fillColor,
			PointMapper mapper
		) => @this.DrawPolygonToCell(
			cell,
			3,
			sizeScale,
			strokeWidthScale,
			strokeColor,
			fillColor,
			mapper,
			direction.RotationDegrees
		);
	}
}
