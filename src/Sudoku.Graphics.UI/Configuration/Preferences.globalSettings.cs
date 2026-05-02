namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates whether tetromino fill colors will be replaced with SRS
	/// (<see href="https://tetris.wiki/Super_Rotation_System">Super Rotation System</see>) defined colors.
	/// </summary>
	public Inherited<bool> UseSrsPredefinedTetrominoFillColors { get; set; } = Inherited<bool>.FromValue(true);
}
