using System;

namespace UnitTestErrorVisualizer
{
	public class MessageLimiter
	{
		private const int kMinShortenLength = 10;
		public MessageLimiter(bool applyLimits, int maxContextLength, bool convertExcapedCharacters)
		{
			Shorten = applyLimits;
			MaxLength = maxContextLength;
			ConvertEscape = convertExcapedCharacters;
		}
		public bool ConvertEscape { get; set; }
		public int MaxLength { get; set; }
		public bool Shorten { get; set; }

		public void AdjustExpectedActualLengths(ref string expected, ref string actual, int differAt, out string correct, out string incorrect)
		{
			if (ConvertEscape)
			{
				ConvertEscapeCharacters(ref expected);
				ConvertEscapeCharacters(ref actual);
			}
			if (Shorten == true)
			{
				ShortenString(ref expected, differAt);
				ShortenString(ref actual, differAt);
				differAt = TestResultParser.GetDifferAtManual(expected, actual);
			}
			if (string.IsNullOrEmpty(actual) == false)
			{
				correct = actual.Substring(0, differAt);
				incorrect = actual.Substring(differAt);
			}
			else
			{
				correct = actual;
				incorrect = actual;
			}
		}

		private void ConvertEscapeCharacters(ref string source)
		{
			if (string.IsNullOrEmpty(source) == false && ConvertEscape == true)
            {
				source = source.Replace("\n", "\\n");
				source = source.Replace("\t", "\\t");
				source = source.Replace("\r", "\\r");
			}
		}

		private void ShortenString(ref string source, int differAt)
		{
			if (string.IsNullOrEmpty(source) == false && Shorten == true)
			{
				int max = Math.Max(kMinShortenLength, MaxLength);
				if (source.Length > max)
				{
					if (differAt < max - 3)
					{// trim end of string off
						source = string.Format("{0}...", source.Substring(0, max - 3));
					}
					else if (differAt >= source.Length - max + 3)
					{// trim front of string off
						int start = source.Length - max + 3;
						if (source[start - 1] == '\\' && (source[start] == 'n' || source[start] == 'r' || source[start] == 't'))
							// If we trimmed off the converted non printable it would be odd.
							--start;
						source = string.Format("...{0}", source.Substring(start));
					}
					else
					{// trim both ends off
						int start = differAt - ((max - 6) / 2);
						source = string.Format("...{0}...", source.Substring(start, max - 6));
					}
				}
			}
		}
	}
}
