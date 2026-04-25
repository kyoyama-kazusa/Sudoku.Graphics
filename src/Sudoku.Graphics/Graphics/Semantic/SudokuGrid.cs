namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Represents a sudoku grid.
/// </summary>
public sealed partial class SudokuGrid : ICloneable, IEquatable<SudokuGrid>, IEqualityOperators<SudokuGrid, SudokuGrid, bool>
{
	/// <summary>
	/// Indicates the givens.
	/// </summary>
	private readonly int[] _givens;

	/// <summary>
	/// Indicates the modifiables.
	/// </summary>
	private readonly int[] _modifiables;

	/// <summary>
	/// Indicates the candidates.
	/// </summary>
	private readonly BitArray _candidates;


	/// <summary>
	/// Initializes a <see cref="SudokuGrid"/> instance via the specified number of rows and columns.
	/// </summary>
	/// <param name="rowsCount">The number of rows.</param>
	/// <param name="columnsCount">The number of columns.</param>
	/// <param name="digitsCount">The number of digits.</param>
	public SudokuGrid(Absolute rowsCount, Absolute columnsCount, Absolute digitsCount)
	{
		RowsCount = rowsCount;
		ColumnsCount = columnsCount;
		DigitsCount = digitsCount;

		_givens = new int[RowsCount * ColumnsCount];
		_modifiables = new int[RowsCount * ColumnsCount];
		_candidates = new(RowsCount * ColumnsCount * DigitsCount, false);
	}

	/// <summary>
	/// Creates a clone from the specified instance.
	/// </summary>
	/// <param name="givens"><inheritdoc cref="_givens" path="/summary"/></param>
	/// <param name="modifiables"><inheritdoc cref="_modifiables" path="/summary"/></param>
	/// <param name="candidates"><inheritdoc cref="_candidates" path="/summary"/></param>
	/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
	/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
	/// <param name="digitsCount"><inheritdoc cref="DigitsCount" path="/summary"/></param>
	/// <param name="clone">
	/// Indicates whether this constructor will clone parameters
	/// <paramref name="givens"/>, <paramref name="modifiables"/> and <paramref name="candidates"/>.
	/// </param>
	private SudokuGrid(
		int[] givens,
		int[] modifiables,
		BitArray? candidates,
		Absolute rowsCount,
		Absolute columnsCount,
		Absolute digitsCount,
		bool clone = true
	)
	{
		_givens = clone ? givens[..] : givens;
		_modifiables = clone ? modifiables[..] : modifiables;
		_candidates = candidates is null ? new(rowsCount * columnsCount * digitsCount) : clone ? new(candidates) : candidates;
		RowsCount = rowsCount;
		ColumnsCount = columnsCount;
		DigitsCount = digitsCount;
	}


	/// <summary>
	/// Indicates the number of cells.
	/// </summary>
	public int CellsCount => RowsCount * ColumnsCount;

	/// <summary>
	/// Indicates the number of candidates assigned.
	/// </summary>
	public int CandidatesCount => _candidates.Cardinality;

	/// <summary>
	/// Indicates the number of rows.
	/// </summary>
	public Absolute RowsCount { get; }

	/// <summary>
	/// Indicates the number of columns.
	/// </summary>
	public Absolute ColumnsCount { get; }

	/// <summary>
	/// Indicates the number of digits.
	/// </summary>
	public Absolute DigitsCount { get; }

	/// <summary>
	/// Indicates the givens.
	/// </summary>
	public ReadOnlySpan<int> Givens => _givens;

	/// <summary>
	/// Indicates the modifiables.
	/// </summary>
	public ReadOnlySpan<int> Modifiables => _modifiables;

	/// <summary>
	/// Indicates the candidates.
	/// </summary>
	public ReadOnlySpan<BitArray?> Candidates
	{
		get
		{
			var result = new BitArray?[CellsCount];
			for (var cell = 0; cell < CellsCount; cell++)
			{
				result[cell] = GetCandidates(cell) is { Cardinality: not 0 } valid ? valid : null;
			}
			return result;
		}
	}



	[GeneratedRegex(""".{3}\:\s*.+(?:\:\s*.{3,}(?:\s+.{3,})*)?""", RegexOptions.Compiled | RegexOptions.Singleline)]
	private static partial Regex FormatPattern { get; }


	/// <summary>
	/// Represents an event that will be triggered when digits are adding.
	/// </summary>
	public event EventHandler<SudokuGrid, SudokuGridDigitAddingEventArgs>? DigitsAdding;

	/// <summary>
	/// Represents an event that will be triggered when digits are added.
	/// </summary>
	public event EventHandler<SudokuGrid, SudokuGridDigitAddedEventArgs>? DigitsAdded;

	/// <summary>
	/// Represents an event that will be triggered when the whole grid is cleared.
	/// </summary>
	public event EventHandler<SudokuGrid, SudokuGridClearedEventArgs>? Cleared;

	/// <summary>
	/// Represents an event that will be trigged when any digit-related conflict is detected.
	/// </summary>
	public event EventHandler<SudokuGrid, SudokuGridDigitConflictDetectedEventArgs>? DigitConflictDetected;


	/// <summary>
	/// Adds given digit into the grid.
	/// </summary>
	/// <param name="cell">The cell.</param>
	/// <param name="digit">The digit.</param>
	/// <exception cref="ArgumentException">
	/// Throws when either argument <paramref name="cell"/> or <paramref name="digit"/> is invalid.
	/// </exception>
	public void AddGiven(Absolute cell, int digit)
	{
		VerifyArgumentCell(cell);
		VerifyArgumentDigit(digit);

		// Trigger event (adding).
		var addingEventArgs = new SudokuGridDigitAddingEventArgs(DigitType.Given, cell, [digit]);
		DigitsAdding?.Invoke(this, addingEventArgs);
		if (addingEventArgs.Handled)
		{
			return;
		}

		// Check conflict.
		if (_givens[cell] is var originalDigit and not 0 && originalDigit != digit)
		{
			var conflictEventArgs = new SudokuGridDigitConflictDetectedEventArgs(DigitType.Given, cell, [originalDigit], [digit]);
			DigitConflictDetected?.Invoke(this, conflictEventArgs);
			if (conflictEventArgs.Handled)
			{
				return;
			}
		}
		if (_modifiables[cell] != 0)
		{
			// Remove modifiable digit and set given.
			_modifiables[cell] = 0;
		}
		if (GetCandidates(cell).Cardinality != 0)
		{
			// Remove candidates and set given.
			for (var d = 0; d < DigitsCount; d++)
			{
				_candidates[cell * DigitsCount + d] = false;
			}
		}

		// Add given.
		_givens[cell] = digit;

		// Trigger event (added).
		DigitsAdded?.Invoke(this, new(DigitType.Given, cell, [digit]));
	}

	/// <summary>
	/// Adds modifiable digit into the grid.
	/// </summary>
	/// <param name="cell">The cell.</param>
	/// <param name="digit">The digit.</param>
	/// <exception cref="ArgumentException">
	/// Throws when either argument <paramref name="cell"/> or <paramref name="digit"/> is invalid.
	/// </exception>
	public void AddModifiable(Absolute cell, int digit)
	{
		VerifyArgumentCell(cell);
		VerifyArgumentDigit(digit);

		// Trigger event (adding).
		var addingEventArgs = new SudokuGridDigitAddingEventArgs(DigitType.Modifiable, cell, [digit]);
		DigitsAdding?.Invoke(this, addingEventArgs);
		if (addingEventArgs.Handled)
		{
			return;
		}

		// Check conflict.
		if (_givens[cell] != 0)
		{
			// Do nothing - givens cannot be replaced with other values, and we also don't append modifiable digits into the cell.
			return;
		}
		if (_modifiables[cell] is var modifiableDigit and not 0 && modifiableDigit != digit)
		{
			// Remove it and set modifiable.
			_modifiables[cell] = 0;
		}
		if (GetCandidates(cell).Cardinality != 0)
		{
			// Remove candidates and set modifiable.
			for (var d = 0; d < DigitsCount; d++)
			{
				_candidates[cell * DigitsCount + d] = false;
			}
		}

		// Add modifiable.
		_modifiables[cell] = digit;

		// Trigger event (added).
		DigitsAdded?.Invoke(this, new(DigitType.Modifiable, cell, [digit]));
	}

	/// <summary>
	/// Adds candidates into the grid.
	/// </summary>
	/// <param name="cell">The cell.</param>
	/// <param name="digits">The digits.</param>
	/// <exception cref="ArgumentException">
	/// Throws when either argument <paramref name="cell"/> or <paramref name="digits"/> is invalid.
	/// </exception>
	public void AddCandidates(Absolute cell, params int[] digits)
	{
		VerifyArgumentCell(cell);
		VerifyArgumentDigits(digits);

		// Trigger event (adding).
		var addingEventArgs = new SudokuGridDigitAddingEventArgs(DigitType.Candidate, cell, digits);
		DigitsAdding?.Invoke(this, addingEventArgs);
		if (addingEventArgs.Handled)
		{
			return;
		}

		// Check conflict.
		if (_givens[cell] != 0)
		{
			// Do nothing - givens cannot be replaced with other values, and we also don't append candidates into the cell.
			return;
		}
		if (_modifiables[cell] != 0)
		{
			// Also do nothing.
			return;
		}
		if (GetCandidates(cell).Cardinality != 0)
		{
			// Remove candidates and set candidates.
			for (var d = 0; d < DigitsCount; d++)
			{
				_candidates[cell * DigitsCount + d] = false;
			}
		}

		// Add candidates.
		foreach (var digit in digits)
		{
			_candidates[cell * DigitsCount + digit] = true;
		}

		// Trigger event (added).
		DigitsAdded?.Invoke(this, new(DigitType.Candidate, cell, digits));
	}

	/// <summary>
	/// Clears the whole grid; removing all givens, modifiables and candidates.
	/// </summary>
	public void Clear()
	{
		for (var cell = 0; cell < CellsCount; cell++)
		{
			_givens[cell] = 0;
			_modifiables[cell] = 0;
			for (var digit = 0; digit < DigitsCount; digit++)
			{
				_candidates[cell * DigitsCount + digit] = false;
			}
		}

		Cleared?.Invoke(this, new());
	}

	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as SudokuGrid);

	/// <inheritdoc/>
	public bool Equals([NotNullWhen(true)] SudokuGrid? other) => other is not null && ToString() == other.ToString();

	/// <summary>
	/// Indicates whether the specified candidate exists in the collection or not.
	/// </summary>
	/// <param name="cell">The cell.</param>
	/// <param name="digit">The digit.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	public bool ContainsCandidate(Absolute cell, int digit) => _candidates[cell * DigitsCount + digit];

	/// <summary>
	/// Indicates whether the specified candidate exists in the collection or not.
	/// </summary>
	/// <param name="candidate">The candidate.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	public bool ContainsCandidate(CandidatePosition candidate) => ContainsCandidate(candidate.Cell, candidate.InnerIndex);

	/// <inheritdoc/>
	public override int GetHashCode() => ToString().GetHashCode();

	/// <inheritdoc/>
	/// <remarks>
	/// Format:
	/// <code>
	/// format
	///   : rows_count_character columns_count_character digits_count_character ':' ' '* value_sequence (':' ' '* candidates)?
	///   ;
	///
	/// rows_count_character : value_character ;
	///
	/// columns_count_character : value_character ;
	///
	/// digits_count_character : value_character ;
	///
	/// value_sequence : ('+'? value_character)+ ;
	///
	/// candidates
	///   : digit_character+ cell_row_index cell_column_index (' '+ digit_character+ cell_row_index cell_column_index)*
	///   ;
	///
	/// value_character
	///   : '.' | '0' - '9' | 'A' - 'Z' | 'a' - 'z' | greek_letters_upper | greek_letters_lower
	///   ;
	///
	/// cell_row_index : value_character ;
	///
	/// cell_column_index : value_character ;
	///
	/// digit_character : value_character ;
	/// </code>
	/// </remarks>
	public override string ToString()
	{
		var rowsCountStr = SudokuGridNotation.IndexToChar(RowsCount);
		var columnsCountStr = SudokuGridNotation.IndexToChar(ColumnsCount);
		var digitsCountStr = SudokuGridNotation.IndexToChar(DigitsCount);

		var valuesSb = new StringBuilder();
		foreach (var digit in _givens)
		{
			valuesSb.Append(digit);
		}
		foreach (var digit in _modifiables)
		{
			valuesSb.Append($"+{digit}");
		}

		if (!(EnumerateCandidates().GetCandidatePositionsImmediately() is { Length: not 0 } candidates))
		{
			return $"{rowsCountStr}{columnsCountStr}{digitsCountStr}:{valuesSb}";
		}

		// Build candidate part.
		var comparer = Comparer<CandidatePosition>.Create(static (left, right) => left.InnerIndex.CompareTo(right.InnerIndex));
		var cellCandidatePositionGroups = new SortedDictionary<int, SortedSet<CandidatePosition>>();
		foreach (var candidatePosition in candidates)
		{
			var cell = candidatePosition.Cell;
			if (!cellCandidatePositionGroups.TryAdd(cell, [with(comparer), candidatePosition]))
			{
				cellCandidatePositionGroups[cell].Add(candidatePosition);
			}
		}

		var strings = new List<string>();
		foreach (var (cell, positions) in cellCandidatePositionGroups)
		{
			var digitsStr = string.Concat(from pos in positions select pos.InnerIndex + 1);
			var row = cell / ColumnsCount;
			var column = cell % ColumnsCount;
			var rowStr = SudokuGridNotation.IndexToChar(row);
			var columnStr = SudokuGridNotation.IndexToChar(column);
			strings.Add($"{digitsStr}{rowStr}{columnStr}");
		}

		return $"{rowsCountStr}{columnsCountStr}{digitsCountStr}:{valuesSb}:{string.Join(' ', strings)}";
	}

	/// <summary>
	/// Try to enumerate all possible candidates defined in this collection.
	/// </summary>
	/// <returns>An enumerator type that can iterate candidates.</returns>
	public CandidatesEnumerator EnumerateCandidates() => new(_candidates, DigitsCount);

	/// <summary>
	/// Returns the candidates of the specified cell.
	/// </summary>
	/// <param name="cell">The cell.</param>
	/// <returns>The candidates.</returns>
	public BitArray GetCandidates(Absolute cell)
	{
		var result = new BitArray(DigitsCount);
		for (var i = 0; i < DigitsCount; i++)
		{
			result[i] = ContainsCandidate(cell, i);
		}
		return result;
	}

	/// <inheritdoc cref="ICloneable.Clone"/>
	public SudokuGrid Clone() => new(_givens, _modifiables, _candidates, RowsCount, ColumnsCount, DigitsCount);

	/// <inheritdoc/>
	object ICloneable.Clone() => Clone();

	/// <summary>
	/// Verify validity of argument <paramref name="cell"/>.
	/// </summary>
	/// <param name="cell">The cell.</param>
	/// <exception cref="ArgumentException">Throws when argument is invalid.</exception>
	private void VerifyArgumentCell(Absolute cell)
	{
		if (cell < 0 || cell >= CellsCount)
		{
			throw new ArgumentException($"The argument '{nameof(cell)}' must be between 0 and '{CellsCount} - 1'.", nameof(cell));
		}
	}

	/// <summary>
	/// Verify validity of argument <paramref name="digit"/>.
	/// </summary>
	/// <param name="digit">The digit.</param>
	/// <exception cref="ArgumentException">Throws when argument is invalid.</exception>
	private void VerifyArgumentDigit(int digit)
	{
		if (digit <= 0 || digit > DigitsCount)
		{
			throw new ArgumentException($"The argument '{nameof(digit)}' must be between 1 and '{DigitsCount}'.", nameof(digit));
		}
	}

	/// <summary>
	/// Verify validity of argument <paramref name="digits"/>.
	/// </summary>
	/// <param name="digits">The digits.</param>
	/// <exception cref="ArgumentException">Throws when argument is invalid.</exception>
	private void VerifyArgumentDigits(int[] digits)
	{
		foreach (var digit in digits)
		{
			VerifyArgumentDigit(digit);
		}
	}


	/// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)"/>
	public static bool TryParse([NotNullWhen(true)] string? s, [NotNullWhen(true)] out SudokuGrid? result)
	{
		try
		{
			if (s is null)
			{
				goto ReturnFalse;
			}

			result = Parse(s);
			return true;
		}
		catch (FormatException)
		{
		}

	ReturnFalse:
		result = default;
		return false;
	}

	/// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)"/>
	public static SudokuGrid Parse(string s)
	{
		if (FormatPattern.Match(s) is not { Success: true, Value: var value })
		{
			goto ThrowFormatException;
		}

		var split = value.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		var firstPart = split[0];
		if (firstPart is not [var rowsCountCh, var columnsCountCh, var digitsCountCh]
			|| !SudokuGridNotation.IsValidChar(rowsCountCh)
			|| !SudokuGridNotation.IsValidChar(columnsCountCh)
			|| !SudokuGridNotation.IsValidChar(digitsCountCh))
		{
			goto ThrowFormatException;
		}

		var rowsCount = SudokuGridNotation.CharToIndex(rowsCountCh);
		var columnsCount = SudokuGridNotation.CharToIndex(columnsCountCh);
		var digitsCount = SudokuGridNotation.CharToIndex(digitsCountCh);

		var givens = new int[rowsCount * columnsCount];
		var modifiables = new int[rowsCount * columnsCount];
		var candidates = default(BitArray);

		// Parse given and modifiable values.
		var secondPart = split[1];
		for (var (i, cell) = (0, 0); i < secondPart.Length; cell++)
		{
			switch (secondPart[i])
			{
				case '0' or '.':
				{
					givens[cell] = 0;
					i++;
					break;
				}
				case '+':
				{
					if (i + 1 < secondPart.Length)
					{
						switch (secondPart[i + 1])
						{
							case '0' or '.':
							{
								modifiables[cell] = 0;
								break;
							}
							case var ch when SudokuGridNotation.IsValidChar(ch):
							{
								modifiables[cell] = SudokuGridNotation.CharToIndex(ch);
								break;
							}
							default:
							{
								goto ThrowFormatException;
							}
						}
						i += 2;
					}
					else
					{
						goto ThrowFormatException;
					}
					break;
				}
				case var ch when SudokuGridNotation.IsValidChar(ch):
				{
					givens[cell] = SudokuGridNotation.CharToIndex(ch);
					i++;
					break;
				}
				default:
				{
					goto ThrowFormatException;
				}
			}
		}

		// Parse candidates.
		if (split is [_, _, var thirdPart])
		{
			candidates = new(rowsCount * columnsCount * digitsCount, false);
			foreach (var part in thirdPart.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
			{
				var rowCh = part[^2];
				var columnCh = part[^1];
				if (!SudokuGridNotation.IsValidChar(rowCh) || !SudokuGridNotation.IsValidChar(columnCh))
				{
					goto ThrowFormatException;
				}

				var row = SudokuGridNotation.CharToIndex(rowCh);
				var column = SudokuGridNotation.CharToIndex(columnCh);
				foreach (var ch in part[..^2])
				{
					var digit = SudokuGridNotation.CharToIndex(ch);
					candidates[(row * columnsCount + column) * digitsCount + digit] = true;
				}
			}
		}

		return new(givens, modifiables, candidates, rowsCount, columnsCount, digitsCount, false);

	ThrowFormatException:
		throw new FormatException();
	}


	/// <inheritdoc/>
	public static bool operator ==(SudokuGrid? left, SudokuGrid? right)
		=> (left, right) switch { (null, null) => true, (not null, not null) => left.Equals(right), _ => false };

	/// <inheritdoc/>
	public static bool operator !=(SudokuGrid? left, SudokuGrid? right) => !(left == right);
}
