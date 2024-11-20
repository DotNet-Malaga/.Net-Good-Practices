using System.Globalization;

namespace Humanizer.Performance;
public interface ICulturedStringTransformer : IStringTransformer
{
    /// <summary>
    /// Transform the input
    /// </summary>
    /// <param name="input">String to be transformed</param>
    string Transform(string input, CultureInfo culture);
}

