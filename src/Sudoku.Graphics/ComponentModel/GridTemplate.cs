namespace Sudoku.ComponentModel;

/// <summary>
/// <para>
/// Represents a grid template type that draws block (thick) and normal (thin) lines
/// onto a canvas via the specified configuration.
/// </para>
/// <para>
/// Because it can define the shape of a grid,
/// the template will also be used as identifiers of puzzles, as basic grid panel.
/// </para>
/// </summary>
/// <param name="mapper"><inheritdoc cref="Mapper" path="/summary"/></param>
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
[JsonDerivedType(typeof(DefaultGridTemplate), nameof(DefaultGridTemplate))]
[JsonDerivedType(typeof(JigsawGridTemplate), nameof(JigsawGridTemplate))]
[JsonDerivedType(typeof(SpecifiedGridTemplate), nameof(SpecifiedGridTemplate))]
[JsonDerivedType(typeof(StandardGridTemplate), nameof(StandardGridTemplate))]
[JsonDerivedType(typeof(SujikenGridTemplate), nameof(SujikenGridTemplate))]
public abstract class GridTemplate(PointMapper mapper)
{
	/// <summary>
	/// Indicates point mapper instance that can project points into cells.
	/// </summary>
	public PointMapper Mapper { get; } = mapper;


	/// <summary>
	/// Try to draw lines onto the target canvas.
	/// </summary>
	/// <param name="canvas">The canvas object to receive drawn items.</param>
	/// <param name="options">The options.</param>
	public abstract void DrawLines(SKCanvas canvas, CanvasDrawingOptions options);
}
