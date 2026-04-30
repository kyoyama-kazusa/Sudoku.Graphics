namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates whether seven-segment display will use secondary digit styles.
	/// </summary>
	public Inherited<bool> CellSevenSegmentDisplayUseSecondaryDigitStyle { get; set; } = Inherited<bool>.FromValue(false);

	/// <summary>
	/// Indicates whether phantom segments will also be displayed for seven-segment display.
	/// </summary>
	public Inherited<bool> CellSevenSegmentDisplayShowPhantomSegments { get; set; } = Inherited<bool>.FromValue(true);

	/// <summary>
	/// Indicates cell seven segment display size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellSevenSegmentDisplaySizeScale { get; set; } = Inherited<Scale>.FromValue(0.75M);

	/// <summary>
	/// Indicates cell seven segment display segment rectangle width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellSevenSegmentDisplaySegmentRectWidthScale { get; set; } = Inherited<Scale>.FromValue(0.3M);

	/// <summary>
	/// Indicates cell seven segment display segment rectangle height scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellSevenSegmentDisplaySegmentRectHeightScale { get; set; } = Inherited<Scale>.FromValue(0.1M);

	/// <summary>
	/// Indicates the cell seven segment display stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellSevenSegmentDisplayStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThinLineWidth));

	/// <summary>
	/// Indicates cell seven segment display phantom stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellSevenSegmentDisplayPhantomStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThinLineWidth));

	/// <summary>
	/// Indicates the cell seven segment display stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellSevenSegmentDisplayStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultThinLineColor));

	/// <summary>
	/// Indicates the cell seven segment display phantom segment stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellSevenSegmentDisplayPhantomStrokeColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.LightGray);

	/// <summary>
	/// Indicates the cell  line fill color.
	/// </summary>
	public Inherited<SerializableColor> CellSevenSegmentDisplayFillColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.Gray);
}
