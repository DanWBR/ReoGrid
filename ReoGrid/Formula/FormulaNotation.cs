/*****************************************************************************
 *
 * ReoGrid - .NET Spreadsheet Control
 *
 * https://reogrid.net/
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
 * PURPOSE.
 *
 ****************************************************************************/

#if FORMULA

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace unvell.ReoGrid.Formula
{
	/// <summary>
	/// The argument separator and the decimal mark a formula is written with.
	/// </summary>
	public struct FormulaSeparators : IEquatable<FormulaSeparators>
	{
		/// <summary>Separator between function arguments, for example "," or ";".</summary>
		public string Parameter { get; }

		/// <summary>Decimal mark of number literals, for example "." or ",".</summary>
		public string Decimal { get; }

		/// <summary>Create a separator pair.</summary>
		public FormulaSeparators(string parameter, string @decimal)
		{
			this.Parameter = parameter;
			this.Decimal = @decimal;
		}

		/// <summary>
		/// The form formulas are stored in: ";" between arguments and "." as decimal mark,
		/// the separators the classic DWSIM spreadsheet uses on an English locale.
		/// </summary>
		public static FormulaSeparators Canonical => new FormulaSeparators(";", ".");

		/// <summary>The form of the Excel file format: "," between arguments and "." as decimal mark.</summary>
		public static FormulaSeparators Excel => new FormulaSeparators(",", ".");

		/// <summary>The form the formula parser reads now, from <see cref="FormulaExtension"/>.</summary>
		public static FormulaSeparators Current =>
			new FormulaSeparators(FormulaExtension.ParameterSeparator, FormulaExtension.NumberDecimalSeparator);

		/// <summary>The form a culture types formulas in: its list separator and its decimal mark.</summary>
		public static FormulaSeparators FromCulture(CultureInfo culture) =>
			new FormulaSeparators(culture.TextInfo.ListSeparator, culture.NumberFormat.NumberDecimalSeparator);

		/// <inheritdoc/>
		public bool Equals(FormulaSeparators other) => this.Parameter == other.Parameter && this.Decimal == other.Decimal;

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is FormulaSeparators o && Equals(o);

		/// <inheritdoc/>
		public override int GetHashCode() => ((Parameter ?? "").GetHashCode() * 397) ^ (Decimal ?? "").GetHashCode();

		/// <inheritdoc/>
		public override string ToString() => "'" + Parameter + "' '" + Decimal + "'";
	}

	/// <summary>
	/// Converts formula text between the argument separators and decimal marks of different locales.
	/// </summary>
	/// <remarks>
	/// The conversion walks the tokens of the formula with the same grammar the parser uses, and
	/// rewrites only argument separator tokens and the decimal mark inside number literals. String
	/// literals, cell and range references, names and everything else are copied as they are, so a
	/// comma or a semicolon inside "quoted text" never changes.
	/// </remarks>
	public static class FormulaNotation
	{
		/// <summary>
		/// Value of the RGF head meta element "notation" that marks a worksheet whose formulas are
		/// stored in the canonical form (<see cref="FormulaSeparators.Canonical"/>) and whose numbers
		/// are stored in the invariant culture.
		/// </summary>
		public const string CanonicalFormatName = "canonical";

		/// <summary>
		/// When true (the default), RGF files are written with formulas in the canonical form and
		/// numbers in the invariant culture, and marked so; the loader translates them back to the
		/// form of the parser and the culture of the machine. Turned off, files are written as before.
		/// </summary>
		public static bool CanonicalStorage { get; set; } = true;

		private static readonly ConcurrentDictionary<FormulaSeparators, Regex> grammars =
			new ConcurrentDictionary<FormulaSeparators, Regex>();

		private static Regex Grammar(FormulaSeparators form) =>
			grammars.GetOrAdd(form, f => ExcelFormulaLexer.BuildTokenRegex(f.Parameter, f.Decimal));

		/// <summary>
		/// Convert a formula written in the <paramref name="from"/> form to the <paramref name="to"/> form.
		/// Characters the grammar does not know are copied as they are.
		/// </summary>
		public static string Translate(string formula, FormulaSeparators from, FormulaSeparators to)
		{
			TryTranslate(formula, from, to, out var result);
			return result;
		}

		/// <summary>
		/// Convert a formula written in the <paramref name="from"/> form to the <paramref name="to"/> form.
		/// Returns false when the text holds characters that are not a token of the
		/// <paramref name="from"/> form, outside string literals: a hint that the formula was written in
		/// another form. The converted text is returned either way.
		/// </summary>
		public static bool TryTranslate(string formula, FormulaSeparators from, FormulaSeparators to, out string result)
		{
			if (string.IsNullOrEmpty(formula))
			{
				result = formula;
				return true;
			}

			bool clean = true;
			var sb = new StringBuilder(formula.Length + 8);
			int pos = 0;
			bool same = from.Equals(to);

			for (var m = Grammar(from).Match(formula); m.Success; m = m.NextMatch())
			{
				if (m.Index > pos)
				{
					clean &= IsBlank(formula, pos, m.Index);
					sb.Append(formula, pos, m.Index - pos);
				}

				Group number = m.Groups["number"], token = m.Groups["token"];

				if (!same && number.Success && !string.IsNullOrEmpty(from.Decimal) && number.Value.Contains(from.Decimal))
				{
					sb.Append(formula, m.Index, number.Index - m.Index);
					sb.Append(number.Value.Replace(from.Decimal, to.Decimal));
				}
				else if (!same && token.Success && token.Value == from.Parameter)
				{
					sb.Append(formula, m.Index, token.Index - m.Index);
					sb.Append(to.Parameter);
				}
				else
				{
					sb.Append(m.Value);
				}

				pos = m.Index + m.Length;
			}

			if (pos < formula.Length)
			{
				clean &= IsBlank(formula, pos, formula.Length);
				sb.Append(formula, pos, formula.Length - pos);
			}

			result = sb.ToString();
			return clean;
		}

		/// <summary>Convert a formula from the parser's form to the canonical form.</summary>
		public static string ToCanonical(string formula) =>
			Translate(formula, FormulaSeparators.Current, FormulaSeparators.Canonical);

		/// <summary>Convert a formula from the canonical form to the parser's form.</summary>
		public static string FromCanonical(string formula) =>
			Translate(formula, FormulaSeparators.Canonical, FormulaSeparators.Current);

		/// <summary>Convert a formula from the parser's form to the form of the Excel file format.</summary>
		public static string ToExcel(string formula) =>
			Translate(formula, FormulaSeparators.Current, FormulaSeparators.Excel);

		/// <summary>Convert a formula from the form of the Excel file format to the parser's form.</summary>
		public static string FromExcel(string formula) =>
			Translate(formula, FormulaSeparators.Excel, FormulaSeparators.Current);

		/// <summary>True when the formula parses with the separators the parser uses now.</summary>
		public static bool CanParse(string formula)
		{
			if (string.IsNullOrWhiteSpace(formula)) return false;
			try
			{
				return Parser.Parse(null, null, formula) != null;
			}
			catch
			{
				return false;
			}
		}

		private static bool IsBlank(string s, int from, int to)
		{
			for (int i = from; i < to; i++)
			{
				if (!char.IsWhiteSpace(s[i])) return false;
			}
			return true;
		}

		#region Stored form of a worksheet
		/// <summary>
		/// Translate the formulas of one stored worksheet to the parser's form.
		/// </summary>
		/// <param name="formulas">The formulas as stored.</param>
		/// <param name="notation">The "notation" of the file: <see cref="CanonicalFormatName"/>
		/// for files written in the canonical form, null for files written before it existed.</param>
		/// <param name="cultureName">The culture the file was written on (RGF head meta), or null.</param>
		/// <param name="coreVersion">The ReoGrid version that wrote the file (RGF head meta), or null.</param>
		/// <returns>The formulas in the parser's form, in the same order.</returns>
		/// <remarks>
		/// <para>A file marked canonical is read in the canonical form.</para>
		/// <para>A file without the mark carries the form of the machine that wrote it. The candidates
		/// are, in order: the form its head says (the classic DWSIM spreadsheet, ReoGrid 2.x, always
		/// separates arguments with ";" and uses the decimal mark of the culture; later versions use
		/// the list separator and the decimal mark of the culture), the parser's form, the canonical
		/// form, the en-US form and the pt-BR form. The candidate under which most formulas translate
		/// cleanly and parse is the form of the worksheet, the first in the order on a tie.</para>
		/// <para>A formula that does not parse in the chosen form takes the first candidate it parses in;
		/// one that parses in none is translated from the chosen form and kept as text, as before.</para>
		/// </remarks>
		public static IList<string> FromStored(IList<string> formulas, string notation, string cultureName, string coreVersion)
		{
			var current = FormulaSeparators.Current;
			var result = new string[formulas.Count];

			var candidates = new List<FormulaSeparators>();
			void Add(FormulaSeparators f)
			{
				if (!string.IsNullOrEmpty(f.Parameter) && !string.IsNullOrEmpty(f.Decimal) && !candidates.Contains(f))
					candidates.Add(f);
			}

			bool canonical = string.Equals(notation, CanonicalFormatName, StringComparison.OrdinalIgnoreCase);
			if (canonical)
			{
				Add(FormulaSeparators.Canonical);
			}
			else
			{
				var hint = StoredFormHint(cultureName, coreVersion);
				if (hint.HasValue) Add(hint.Value);
			}
			Add(current);
			Add(FormulaSeparators.Canonical);
			Add(new FormulaSeparators(",", "."));
			Add(new FormulaSeparators(";", ","));

			// formula i read in candidate c, or null when it does not parse that way; worked out once
			var translations = new string[candidates.Count][];
			var done = new bool[candidates.Count][];
			for (int c = 0; c < candidates.Count; c++)
			{
				translations[c] = new string[formulas.Count];
				done[c] = new bool[formulas.Count];
			}

			string Read(int c, int i)
			{
				if (!done[c][i])
				{
					done[c][i] = true;
					if (TryTranslate(formulas[i], candidates[c], current, out var t) && CanParse(t)) translations[c][i] = t;
				}
				return translations[c][i];
			}

			// a marked file is canonical; an unmarked one is in the form most of its formulas parse in
			int chosen = 0;
			if (!canonical)
			{
				int best = -1;
				for (int c = 0; c < candidates.Count; c++)
				{
					int score = 0;
					for (int i = 0; i < formulas.Count; i++)
					{
						if (!string.IsNullOrEmpty(formulas[i]) && Read(c, i) != null) score++;
					}
					if (score > best) { best = score; chosen = c; }
				}
			}

			for (int i = 0; i < formulas.Count; i++)
			{
				var f = formulas[i];
				if (string.IsNullOrEmpty(f)) { result[i] = f; continue; }

				string t = Read(chosen, i);
				for (int c = 0; t == null && c < candidates.Count; c++) t = Read(c, i);
				result[i] = t ?? Translate(f, candidates[chosen], current);
			}

			return result;
		}

		/// <summary>
		/// The form a file without the "notation" mark was most likely written in, from the
		/// culture and the ReoGrid version in its head; null when the head says nothing.
		/// </summary>
		internal static FormulaSeparators? StoredFormHint(string cultureName, string coreVersion)
		{
			CultureInfo culture = null;
			if (cultureName != null)
			{
				try { culture = cultureName.Length == 0 ? CultureInfo.InvariantCulture : new CultureInfo(cultureName, false); }
				catch { culture = null; }
			}

			bool classic = false;
			if (!string.IsNullOrEmpty(coreVersion) && Version.TryParse(coreVersion, out var v)) classic = v.Major < 3;

			if (classic)
			{
				// the classic DWSIM build of ReoGrid 2.x reads ";" between arguments on every locale
				// and the decimal mark of the locale
				return new FormulaSeparators(";", culture != null ? culture.NumberFormat.NumberDecimalSeparator : ".");
			}

			if (culture != null) return FormulaSeparators.FromCulture(culture);

			// a file written on the invariant culture stores an empty culture name, which the
			// XML round trip through JSON turns into a missing one
			if (!string.IsNullOrEmpty(coreVersion)) return FormulaSeparators.FromCulture(CultureInfo.InvariantCulture);

			return null;
		}
		#endregion // Stored form of a worksheet
	}
}

#endif // FORMULA
