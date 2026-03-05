namespace Sudoku.Graphics;

public partial class SKCanvasDrawings
{
	extension(SKCanvas @this)
	{
		/// <summary>
		/// Draws a hexagon symbol into the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="sizeScale">The scale of size.</param>
		/// <param name="strokeColor">The stroke color.</param>
		/// <param name="strokeWidthScale">The stroke width scale.</param>
		/// <param name="fillColor">The fill color.</param>
		/// <param name="orientation">The orientation.</param>
		/// <param name="mapper">The mapper.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws when <paramref name="orientation"/> is not defined or <see cref="Orientation2.None"/>.
		/// </exception>
		/// <seealso cref="Orientation2.None"/>
		public void DrawHexagonToCell(
			Absolute cell,
			Scale sizeScale,
			SerializableColor strokeColor,
			Scale strokeWidthScale,
			SerializableColor fillColor,
			Orientation2 orientation,
			PointMapper mapper
		) => @this.DrawPolygonToCell(
			cell,
			6,
			sizeScale,
			strokeWidthScale,
			strokeColor,
			fillColor,
			mapper,
			orientation switch
			{
				Orientation2.Horizontal => 30,
				Orientation2.Vertical => 0,
				_ => throw new ArgumentOutOfRangeException(nameof(orientation))
			}
		);
	}
}
