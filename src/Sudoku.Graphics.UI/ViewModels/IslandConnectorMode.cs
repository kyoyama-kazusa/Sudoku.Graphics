namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a mode of island connector.
/// </summary>
public enum IslandConnectorMode
{
	/// <summary>
	/// Indicates the default value.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates the direct mode.
	/// </summary>
	Direct,

	/// <summary>
	/// Indicates the single corner mode.
	/// </summary>
	SingleCorner,

	/// <summary>
	/// Indicates the double corners mode.
	/// </summary>
	DoubleCorners
}
