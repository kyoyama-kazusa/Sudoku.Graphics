namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Provides a way to represent a grid using a <see cref="string"/> value.
/// </summary>
public static class SudokuGridNotation
{
	/// <summary>
	/// Determine whether the character is valid or not.
	/// </summary>
	/// <param name="ch">The character.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	public static bool IsValidChar(char ch)
		=> ch
		is '.'
		or >= '0' and <= '9'
		or >= 'A' and <= 'Z'
		or >= 'a' and <= 'z'
		or >= '\u0391' and <= '\u03A1'
		or >= '\u03A3' and <= '\u03A9'
		or >= '\u03B1' and <= '\u03C9';

	/// <summary>
	/// Converts the specified character into index between -1 and 109.
	/// </summary>
	/// <param name="ch">The character.</param>
	/// <returns>The index to that character.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Throws when the character is not supported.</exception>
	public static int CharToIndex(char ch)
		=> ch switch
		{
			'0' or '.' => -1,
			>= '1' and <= '9' => ch - '1',
			>= 'A' and <= 'Z' => 9 + ch - 'A',
			>= 'a' and <= 'z' => 35 + ch - 'a',
			>= '\u0391' and <= '\u03A1' => 61 + ch - '\u0391',
			>= '\u03A3' and <= '\u03A9' => 61 + 17 + ch - '\u03A3',
			>= '\u03B1' and <= '\u03C9' => 85 + ch - '\u03B1',
			_ => throw new ArgumentOutOfRangeException(nameof(ch))
		};

	/// <summary>
	/// Converts the specified index into character.
	/// </summary>
	/// <param name="index">The index between -1 and 109.</param>
	/// <param name="placeholderCh">The placeholder character.</param>
	/// <returns>The character.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Throws when the value is out of range.</exception>
	public static char IndexToChar(int index, char placeholderCh = '.')
		=> placeholderCh is '.' or '0'
			? index switch
			{
				< -1 or > 109 => throw new ArgumentOutOfRangeException(nameof(index)),
				-1 => placeholderCh,
				< 9 => (char)('1' + index),
				< 35 => (char)('A' + index - 9),
				< 61 => (char)('a' + index - 35),
				< 85 when index - 61 is var local => local < 17 ? (char)('\u0391' + local) : (char)('\u03A3' + local - 17),
				_ => (char)('\u03B1' + index - 85)
			}
			: throw new ArgumentException(
				$"The argument '{nameof(placeholderCh)}' is invalid - it must be '.' or '0'.",
				nameof(placeholderCh)
			);
}
