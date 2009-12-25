using System;
using Xunit;
using UnitTestErrorVisualizer;

namespace UnitTestVisualizer.Tests
{
	public class MessageLimiterTests
	{
		[Fact]
		public void MessageLimiter_Null_Null()
		{
			MessageLimiter underTest = new MessageLimiter(true, 40, true);
			string expected = null;
			string actual = null;
			string correct;
			string incorrect;

            underTest.AdjustExpectedActualLengths(ref expected, ref actual, 0, out correct, out incorrect);

			Assert.Null(expected);
			Assert.Null(actual);
			Assert.Null(correct);
			Assert.Null(incorrect);
		}

		[Fact]
		public void MessageLimiter_Empty_Empty()
		{
			MessageLimiter underTest = new MessageLimiter(true, 40, true);
			string expected = String.Empty;
			string actual = String.Empty;
			string correct;
			string incorrect;

			underTest.AdjustExpectedActualLengths(ref expected, ref actual, 0, out correct, out incorrect);

			Assert.Equal(String.Empty, expected);
			Assert.Equal(String.Empty, actual);
			Assert.Equal(String.Empty, correct);
			Assert.Equal(String.Empty, incorrect);
		}

		[Fact]
		public void MessageLimiter_LongNonShortening_NoChange()
		{
			MessageLimiter underTest = new MessageLimiter(false, 1, false);
			string expected = "fifty";
			string actual = "fofty";
			string correct;
			string incorrect;

			underTest.AdjustExpectedActualLengths(ref expected, ref actual, 1, out correct, out incorrect);

			Assert.Equal("fifty", expected);
			Assert.Equal("fofty", actual);
			Assert.Equal("f", correct);
			Assert.Equal("ofty", incorrect);
		}

		[Fact]
		public void MessageLimiter_EscapeCharNoConvert_NoChange()
		{
			MessageLimiter underTest = new MessageLimiter(true, 20, false);
			string expected = "fifty\tfive\n";
			string actual = "fifty\tfivi\n";
			string correct;
			string incorrect;

            underTest.AdjustExpectedActualLengths(ref expected, ref actual, 10, out correct, out incorrect);

			Assert.Equal("fifty\tfive\n", expected);
			Assert.Equal("fifty\tfivi\n", actual);
			Assert.Equal("fifty\tfiv", correct);
			Assert.Equal("i\n", incorrect);
		}

		[Fact]
		public void MessageLimiter_ShortenDoesNothingUnder10Chars_NoChange()
		{// Arbitrary value picked that where we will do nothing because shortining would make things worse.
			MessageLimiter underTest = new MessageLimiter(true, 9, false);
			string expected = "abcdefghij";
			string actual = "abbdefghij";
			string correct;
			string incorrect;

            underTest.AdjustExpectedActualLengths(ref expected, ref actual, 0, out correct, out incorrect);

			Assert.Equal("abcdefghij", expected);
			Assert.Equal("abbdefghij", actual);
			Assert.Equal("ab", correct);
			Assert.Equal("bdefghij", incorrect);
		}

		[Fact]
		public void MessageLimiter_ShortenDifferFront_EndElipsis()
		{// Arbitrary value picked that where we will do nothing because shortining would make things worse.
			MessageLimiter underTest = new MessageLimiter(true, 10, false);
			string expected = "abcdefghijklmnopqrstuvwxyz";
			string actual = "aacdefghijklmnopqrstuvwxyz";
			string correct;
			string incorrect;

            underTest.AdjustExpectedActualLengths(ref expected, ref actual, 1, out correct, out incorrect);

			Assert.Equal("abcdefg...", expected);
			Assert.Equal("aacdefg...", actual);
			Assert.Equal("a", correct);
			Assert.Equal("acdefg...", incorrect);
		}

		[Fact]
		public void MessageLimiter_ShortenDifferMid_FrontEndElipsis()
		{// Arbitrary value picked that where we will do nothing because shortining would make things worse.
			MessageLimiter underTest = new MessageLimiter(true, 10, false);
			string expected = "abcdefghijklmnopqrstuvwxyz";
			string actual = "abcdefghijklmnapqrstuvwxyz";
			string correct;
			string incorrect;

			underTest.AdjustExpectedActualLengths(ref expected, ref actual, 15, out correct, out incorrect);

			Assert.Equal("...nopq...", expected);
			Assert.Equal("...napq...", actual);
			Assert.Equal("...n", correct);
			Assert.Equal("apq...", incorrect);
		}

		[Fact]
		public void MessageLimiter_ShortenDifferEnd_FrontElipsis()
		{// Arbitrary value picked that where we will do nothing because shortining would make things worse.
			MessageLimiter underTest = new MessageLimiter(true, 10, false);
			string expected = "abcdefghijklmnopqrstuvwxyz";
			string actual = "abcdefghijklmnopqrsttvwxyz";
			string correct;
			string incorrect;

            underTest.AdjustExpectedActualLengths(ref expected, ref actual, 20, out correct, out incorrect);

			Assert.Equal("...tuvwxyz", expected);
			Assert.Equal("...ttvwxyz", actual);
			Assert.Equal("...t", correct);
			Assert.Equal("tvwxyz", incorrect);
		}

		[Fact]
		public void MessageLimiter_ShortenDifferEnd_FrontElipsis2()
		{// Arbitrary value picked that where we will do nothing because shortining would make things worse.
			MessageLimiter underTest = new MessageLimiter(true, 10, false);
			string expected = "Supercalifragilisticexpealidocious";
			string actual = "Supercalifragilisticxpealidocious";
			string correct;
			string incorrect;

            underTest.AdjustExpectedActualLengths(ref expected, ref actual, 20, out correct, out incorrect);

			Assert.Equal("...icex...", expected);
			Assert.Equal("...icxp...", actual);
			Assert.Equal("...ic", correct);
			Assert.Equal("xp...", incorrect);
		}

		[Fact]
		public void MessageLimiter_ContainsNonPrintable_NotEncoded()
		{// Arbitrary value picked that where we will do nothing because shortining would make things worse.
			MessageLimiter underTest = new MessageLimiter(true, 10, false);
			string expected = "Supercalifragilisticexpealidoc\ti\nous";
			string actual = "Supercalifragilisticexpealidoc\to\nous";
			string correct;
			string incorrect;

            underTest.AdjustExpectedActualLengths(ref expected, ref actual, 33, out correct, out incorrect);

			Assert.Equal("...c\ti\nous", expected);
			Assert.Equal("...c\to\nous", actual);
			Assert.Equal("...c\t", correct);
			Assert.Equal("o\nous", incorrect);
		}

		[Fact]
		public void MessageLimiter_ContainsNonPrintable_Escaped()
		{// Arbitrary value picked that where we will do nothing because shortining would make things worse.
			MessageLimiter underTest = new MessageLimiter(true, 10, true);
			string expected = "Supercalifragilisticexpealidoc\ti\nous";
			string actual = "Supercalifragilisticexpealidoc\to\nous";
			string correct;
			string incorrect;

			underTest.AdjustExpectedActualLengths(ref expected, ref actual, 35, out correct, out incorrect);

			Assert.Equal("...\\ti\\nous", expected);
			Assert.Equal("...\\to\\nous", actual);
			Assert.Equal("...\\t", correct);
			Assert.Equal("o\\nous", incorrect);
		}

		[Fact]
		public void MessageLimiter_ContainsSlash_StartNotMoved()
		{// Arbitrary value picked that where we will do nothing because shortining would make things worse.
			MessageLimiter underTest = new MessageLimiter(true, 10, true);
			string expected = "C:\\somereallylongpath\\atEnd.?";
			string actual = "C:\\somereallylongpath\\itEnd.?";
			string correct;
			string incorrect;

			underTest.AdjustExpectedActualLengths(ref expected, ref actual, 22, out correct, out incorrect);

			Assert.Equal("...atEnd.?", expected);
			Assert.Equal("...itEnd.?", actual);
			Assert.Equal("...", correct);
			Assert.Equal("itEnd.?", incorrect);
		}

		[Fact]
		public void MessageLimiter_ShortActual_Elipsis()
		{// Arbitrary value picked that where we will do nothing because shortining would make things worse.
			MessageLimiter underTest = new MessageLimiter(true, 10, true);
			string expected = "abcdefghijklmnopqrstuvwxyz";
			string actual = "ab";
			string correct;
			string incorrect;

			underTest.AdjustExpectedActualLengths(ref expected, ref actual, 3, out correct, out incorrect);

			Assert.Equal("abcdefg...", expected);
			Assert.Equal("ab", actual);
			Assert.Equal("ab", correct);
			Assert.Equal(String.Empty, incorrect);
		}
	}
}
