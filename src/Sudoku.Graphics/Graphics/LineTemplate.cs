namespace Sudoku.Graphics;

/// <summary>
/// Represents a line template type that draws block (thick) and normal (thin) lines
/// onto a canvas via the specified configuration.
/// </summary>
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
[JsonDerivedType(typeof(AggregatedLineTemplate), nameof(AggregatedLineTemplate))]
[JsonDerivedType(typeof(DefaultLineTemplate), nameof(DefaultLineTemplate))]
[JsonDerivedType(typeof(JigsawLineTemplate), nameof(JigsawLineTemplate))]
[JsonDerivedType(typeof(SpecifiedLineTemplate), nameof(SpecifiedLineTemplate))]
[JsonDerivedType(typeof(StandardLineTemplate), nameof(StandardLineTemplate))]
[JsonDerivedType(typeof(SujikenLineTemplate), nameof(SujikenLineTemplate))]
public abstract class LineTemplate
{
	/// <summary>
	/// Try to draw lines onto the target canvas.
	/// </summary>
	/// <param name="mapper">The mapper instance.</param>
	/// <param name="canvas">The canvas object to receive drawn items.</param>
	/// <param name="options">The options.</param>
	public abstract void DrawLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options);
}
