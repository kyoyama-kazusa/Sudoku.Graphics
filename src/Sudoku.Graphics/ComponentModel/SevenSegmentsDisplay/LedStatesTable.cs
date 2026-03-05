namespace Sudoku.ComponentModel.SevenSegmentsDisplay;

/// <summary>
/// Represents LED seven-segment display table.
/// </summary>
public static class LedStatesTable
{
	/// <summary>
	/// Provides a table of light-up segments in LED display.
	/// </summary>
	/// <remarks>
	/// Order:
	/// <code><![CDATA[
	///        [0]
	///      -------
	///     |       |
	/// [1] |       | [2]
	///     |  [3]  |
	///      -------
	///     |       |
	/// [4] |       | [5]
	///     |       |
	///      -------
	///        [6]
	/// ]]></code>
	/// </remarks>
	public static readonly bool[][][] Value = [
		// 0
		[
			[true, true, true, false, true, true, true]
		],

		// 1
		[
			[false, false, true, false, false, true, false]
		],

		// 2
		[
			[true, false, true, true, true, false, true]
		],

		// 3
		[
			[true, false, true, true, false, true, true]
		],

		// 4
		[
			[false, true, true, true, false, true, false]
		],

		// 5
		[
			[true, true, false, true, false, true, true]
		],

		// 6
		[
			[true, true, false, true, true, true, true],
			[false, true, false, true, true, true, true],
		],

		// 7
		[
			[true, false, true, false, false, true, false],
			[true, true, true, false, false, true, false]
		],

		// 8
		[
			[true, true, true, true, true, true, true]
		],

		// 9
		[
			[true, true, true, true, false, true, true],
			[true, true, true, true, false, true, false]
		]
	];
}
