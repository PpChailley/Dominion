using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace gbd.Dominion.Tools
{
  /// <summary>
  /// from http://stackoverflow.com/questions/359827/ignoring-accented-letters-in-string-comparison
  /// </summary>
  public static class StringExtensions
  {
    private const String ACCENTS_FR = "àâäÀÂÄéèêëÉÈÊËîïÎÏôöÔÖùûüÙÛÜ";
    private const String ACCENTS_FR_REMOVED = "aaaAAAeeeeEEEEiiIIooOOuuuUUU";




    private static String Canonical(this String text)
    {
      if (text == null)
        return String.Empty;

      String trimmed = text.Trim().ToLower();

      String toreturn = String.Concat(
        trimmed.Normalize(NormalizationForm.FormD)
          .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                       UnicodeCategory.NonSpacingMark)
        ).Normalize(NormalizationForm.FormC);

      return toreturn;
    }

    private static String CanonicalSkimmed(this String text)
    {
      String skimmed = Regex.Replace(text, "\\s+", "");
      return skimmed.Canonical();
    }



    public static bool ContainsCanonical(this String container, String contained)
    {
      return container.Canonical().Contains(contained.Canonical());
    }

    public static bool EqualsCanonical(this String me, String you)
    {
      return me.Canonical().Equals(you.Canonical());
    }

    public static bool StartsWithCanonical(this String container, String contained)
    {
      return container.Canonical().StartsWith(contained.Canonical());
    }

    public static bool ContainsSkimmed(this String container, String contained)
    {
      return container.CanonicalSkimmed().Contains(contained.CanonicalSkimmed());
    }

    public static bool EqualsSkimmed(this String me, String you)
    {
      return me.CanonicalSkimmed().Equals(you.CanonicalSkimmed());
    }

    public static bool StartsWithSkimmed(this String container, String contained)
    {
      return container.CanonicalSkimmed().StartsWith(contained.CanonicalSkimmed());
    }


    public static String Transliterate(this String input, String charsFrom, String charsTo)
    {
      String transliterated = input;

      if (charsFrom.Length != charsTo.Length)
        throw new InvalidOperationException("Replacement strings nust be the same length");

      for (int i = 0; i < charsFrom.Length; i++)
      {
        transliterated = transliterated.Replace(charsFrom[i], charsTo[i]);
      }

      return transliterated;
    }
    
    public static String RemoveAccentsFr(this String input)
    {
      return input.Transliterate(ACCENTS_FR, ACCENTS_FR_REMOVED);
    }

    public static bool IsNormalFr(this String input)
    {
      return !ACCENTS_FR.Any(input.Contains);
    }
  }
}

