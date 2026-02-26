namespace Sudoku.ComponentModel.Dices;

/// <summary>
/// Provides dice table.
/// </summary>
public static class DiceTable
{
	/// <summary>
	/// Represents values.
	/// </summary>
	public static readonly bool[][,] Values = [
		new bool[1, 1] { { true } },
		new bool[2, 2] { { true, false }, { false, true } },
		new bool[3, 3] { { true, false, false }, { false, true, false }, { false, false, true } },
		new bool[2, 2] { { true, true }, { true, true } },
		new bool[3, 3] { { true, false, true }, { false, true, false }, { true, false, true } },
		new bool[3, 2] { { true, true }, { true, true }, { true, true } },
		new bool[3, 3] { { true, true, true }, { false, true, false }, { true, true, true } },
		new bool[3, 3] { { true, true, true }, { true, false, true }, { true, true, true } },
		new bool[3, 3] { { true, true, true }, { true, true, true }, { true, true, true } }
	];
}
