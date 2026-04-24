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
	/// Converts the specified character into index between 0 and 110.
	/// </summary>
	/// <param name="ch">The character.</param>
	/// <returns>The index to that character.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Throws when the character is not supported.</exception>
	public static int CharToIndex(char ch)
		=> ch switch
		{
			'0' or '.' => 0,
			>= '1' and <= '9' => ch - '0',
			>= 'A' and <= 'Z' => 10 + ch - 'A',
			>= 'a' and <= 'z' => 36 + ch - 'a',
			>= '\u0391' and <= '\u03A1' => 62 + ch - '\u0391',
			>= '\u03A3' and <= '\u03A9' => 62 + 17 + ch - '\u03A3',
			>= '\u03B1' and <= '\u03C9' => 86 + ch - '\u03B1',
			_ => throw new ArgumentOutOfRangeException(nameof(ch))
		};

	/// <summary>
	/// Converts the specified index into character.
	/// </summary>
	/// <param name="index">The index between 0 and 110.</param>
	/// <param name="placeholderCh">The placeholder character.</param>
	/// <returns>The character.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Throws when the value is out of range.</exception>
	public static char IndexToChar(int index, char placeholderCh = '.')
		=> placeholderCh is '.' or '0'
			? index switch
			{
				< 0 or >= 110 => throw new ArgumentOutOfRangeException(nameof(index)),
				0 => placeholderCh,
				< 10 => (char)('1' + index),
				< 36 => (char)('A' + index - 9),
				< 62 => (char)('a' + index - 35),
				< 86 when index - 62 is var local => local < 17 ? (char)('\u0391' + local) : (char)('\u03A3' + local - 17),
				_ => (char)('\u03B1' + index - 86)
			}
			: throw new ArgumentException(
				$"The argument '{nameof(placeholderCh)}' is invalid - it must be '.' or '0'.",
				nameof(placeholderCh)
			);
}
