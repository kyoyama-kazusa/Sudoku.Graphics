namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a moon phase.
/// </summary>
public enum MoonPhase
{
	/// <summary>
	/// Indicates the phase is unknown.
	/// </summary>
	Unknown = 0,

	/// <summary>
	/// Indicates the phase is full.
	/// </summary>
	Full,

	/// <summary>
	/// Indicates upper half, line.
	/// </summary>
	UpperHalf_Line,

	/// <summary>
	/// Indicates upper half, curve.
	/// </summary>
	UpperHalf_Curve,

	/// <summary>
	/// Indicates lower half, line.
	/// </summary>
	LowerHalf_Line,

	/// <summary>
	/// Indicates lower curve, curve.
	/// </summary>
	LowerHalf_Curve
}
