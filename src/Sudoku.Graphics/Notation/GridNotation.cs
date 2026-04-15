namespace Sudoku.Notation;

/// <summary>
/// Provides a way to represent a grid using a <see cref="string"/> value.
/// </summary>
public static class GridNotation
{
	/// <summary>
	/// Converts the specified character into index between 0 and 109.
	/// </summary>
	/// <param name="ch">The character.</param>
	/// <returns>The index to that character.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Throws when the character is not supported.</exception>
	public static int CharToIndex(char ch)
		=> ch switch
		{
			>= '1' and <= '9' => ch - '1',
			>= 'A' and <= 'Z' => 9 + (ch - 'A'),
			>= 'a' and <= 'z' => 35 + (ch - 'a'),
			>= '\u0391' and <= '\u03A1' => 61 + (ch - '\u0391'),
			>= '\u03A3' and <= '\u03A9' => 61 + 17 + (ch - '\u03A3'),
			>= '\u03B1' and <= '\u03C9' => 85 + (ch - '\u03B1'),
			_ => throw new ArgumentOutOfRangeException(nameof(ch))
		};

	/// <summary>
	/// Converts the specified index into character.
	/// </summary>
	/// <param name="index">The index between 0 and 109.</param>
	/// <returns>The character.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Throws when the value is out of range.</exception>
	public static char IndexToChar(int index)
		=> index switch
		{
			< 0 or >= 110 => throw new ArgumentOutOfRangeException(nameof(index)),
			< 9 => (char)('1' + index),
			< 35 => (char)('A' + index - 9),
			< 61 => (char)('a' + index - 35),
			< 85 when index - 61 is var local => local < 17 ? (char)('\u0391' + local) : (char)('\u03A3' + (local - 17)),
			_ => (char)('\u03B1' + index - 85)
		};
}
