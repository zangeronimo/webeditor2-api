using System.Globalization;
using System.Text;

namespace Webeditor.Application.Utils;

public static class StringsUtils
{
  public static string SlugGenerate(this string text)
  {
    return text.RemoveDiacritics().ToLower().Replace(" ", "-");
  }

  public static string RemoveDiacritics(this string text)
  {
    var normalizedString = text.Normalize(NormalizationForm.FormD);
    var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

    for (int i = 0; i < normalizedString.Length; i++)
    {
      char c = normalizedString[i];
      var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
      if (unicodeCategory != UnicodeCategory.NonSpacingMark)
      {
        stringBuilder.Append(c);
      }
    }

    return stringBuilder
        .ToString()
        .Normalize(NormalizationForm.FormC);
  }
}
