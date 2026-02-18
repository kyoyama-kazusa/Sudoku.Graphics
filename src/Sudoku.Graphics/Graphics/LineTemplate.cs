namespace Sudoku.Graphics;

/// <summary>
/// Represents a line template type that draws block (thick) and normal (thin) lines
/// onto a canvas via the specified configuration.
/// </summary>
/// <param name="mapper"><inheritdoc cref="Mapper" path="/summary"/></param>
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
[JsonDerivedType(typeof(DefaultLineTemplate), nameof(DefaultLineTemplate))]
[JsonDerivedType(typeof(JigsawLineTemplate), nameof(JigsawLineTemplate))]
[JsonDerivedType(typeof(SpecifiedLineTemplate), nameof(SpecifiedLineTemplate))]
[JsonDerivedType(typeof(StandardLineTemplate), nameof(StandardLineTemplate))]
[JsonDerivedType(typeof(SujikenLineTemplate), nameof(SujikenLineTemplate))]
public abstract class LineTemplate(PointMapper mapper)
{
	/// <summary>
	/// Indicates point mapper instance.
	/// </summary>
	public PointMapper Mapper { get; } = mapper;


	/// <summary>
	/// Try to draw lines onto the target canvas.
	/// </summary>
	/// <param name="canvas">The canvas object to receive drawn items.</param>
	/// <param name="options">The options.</param>
	public abstract void DrawLines(SKCanvas canvas, CanvasDrawingOptions options);
}
