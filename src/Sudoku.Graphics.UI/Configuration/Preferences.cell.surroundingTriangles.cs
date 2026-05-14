namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	public Inherited<Scale> SurroundingTrianglesTipDistanceScale { get; set; } = Inherited<Scale>.FromValue(0.1M);

	public Inherited<Scale> SurroundingTrianglesCornerRadiusScale { get; set; } = Inherited<Scale>.FromValue(0.1M);

	public Inherited<Scale> SurroundingTrianglesSizeScale { get; set; } = Inherited<Scale>.FromValue(0.2M);

	public Inherited<Scale> SurroundingTrianglesStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(TemplateThinLineWidthScale));

	public Inherited<SerializableColor> SurroundingTrianglesStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultThinLineColor));

	public Inherited<SerializableColor> SurroundingTrianglesFillColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.White);
}
