namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates cell pair island connector stroke width scale.
	/// </summary>
	public Inherited<Scale> CellPairIslandConnectorStrokeWidthScale { get; set; } = Inherited<Scale>.FromValue(0.08M);

	/// <summary>
	/// Indicates cell pair island connector stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellPairIslandConnectorStrokeColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.Black.WithAlpha(128));
}
