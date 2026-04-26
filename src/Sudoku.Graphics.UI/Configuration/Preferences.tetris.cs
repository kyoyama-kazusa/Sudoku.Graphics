namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates tetromino corner radius scale.
	/// </summary>
	public Inherited<Scale> TetrominoCornerRadiusScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCornerRadiusScale));

	/// <summary>
	/// Indicates tetromino stroke thickness scale.
	/// </summary>
	public Inherited<Scale> TetrominoStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThinLineWidth));

	/// <summary>
	/// Indicates the tetromino small block size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> TetrominoSmallBlockSizeScale { get; set; } = Inherited<Scale>.FromValue(0.2M);

	/// <summary>
	/// Indicates tetromino line color.
	/// </summary>
	public Inherited<SerializableColor> TetrominoLineColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultMiscellaneousLineColor));

	/// <summary>
	/// Indicates tetromino fill color.
	/// </summary>
	public Inherited<SerializableColor> TetrominoFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultFillColor));
}
