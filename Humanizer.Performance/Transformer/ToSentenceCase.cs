namespace Humanizer.Performance;
class ToSentenceCase : ICulturedStringTransformer
{
    public string Transform(string input) =>
        Transform(input, null);

    public string Transform(string input, CultureInfo? culture)
    {
        culture ??= CultureInfo.CurrentCulture;

        if (input.Length >= 1)
        {
            if (char.IsUpper(input[0]))
            {
                return input;
            }
            var c = culture.TextInfo.ToUpper(input[0]);
            return StringHumanizeExtensions.Concat(new CharSpan(in c) , input.AsSpan(1));
        }

        return culture.TextInfo.ToUpper(input);
    }
}

