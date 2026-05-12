namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates tetromino corner radius scale.
	/// </summary>
	public Inherited<Scale> CellTetrominoCornerRadiusScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCornerRadiusScale));

	/// <summary>
	/// Indicates tetromino stroke thickness scale.
	/// </summary>
	public Inherited<Scale> CellTetrominoStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThinLineWidthScale));

	/// <summary>
	/// Indicates the tetromino small block size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellTetrominoSmallBlockSizeScale { get; set; } = Inherited<Scale>.FromValue(0.2M);

	/// <summary>
	/// Indicates tetromino line color.
	/// </summary>
	public Inherited<SerializableColor> CellTetrominoLineColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultMiscellaneousLineColor));

	/// <summary>
	/// Indicates tetromino fill color.
	/// </summary>
	public Inherited<SerializableColor> CellTetrominoFillColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.White);
}
