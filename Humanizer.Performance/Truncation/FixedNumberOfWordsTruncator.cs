﻿using System.Diagnostics.CodeAnalysis;

namespace Humanizer.Performance;

class FixedNumberOfWordsTruncator : ITruncator
{
    [return: NotNullIfNotNull(nameof(value))]
    public string? Truncate(string? value, int length, string? truncationString, TruncateFrom truncateFrom = TruncateFrom.Right)
    {
        if (value == null)
        {
            return null;
        }

        if (value.Length == 0)
        {
            return value;
        }

        var numberOfWords = value.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
        if (numberOfWords <= length)
        {
            return value;
        }

        return truncateFrom == TruncateFrom.Left
            ? TruncateFromLeft(value, length, truncationString)
            : TruncateFromRight(value, length, truncationString);
    }

    static string TruncateFromRight(string value, int length, string? truncationString)
    {
        var lastCharactersWasWhiteSpace = true;
        var numberOfWordsProcessed = 0;
        for (var i = 0; i < value.Length; i++)
        {
            if (char.IsWhiteSpace(value[i]))
            {
                if (!lastCharactersWasWhiteSpace)
                {
                    numberOfWordsProcessed++;
                }

                lastCharactersWasWhiteSpace = true;

                if (numberOfWordsProcessed == length)
                {
                    return StringHumanizeExtensions.Concat(value.AsSpan(0, i), truncationString.AsSpan());
                }
            }
            else
            {
                lastCharactersWasWhiteSpace = false;
            }
        }
        return value + truncationString;
    }

    static string TruncateFromLeft(string value, int length, string? truncationString)
    {
        var lastCharactersWasWhiteSpace = true;
        var numberOfWordsProcessed = 0;
        for (var i = value.Length - 1; i > 0; i--)
        {
            if (char.IsWhiteSpace(value[i]))
            {
                if (!lastCharactersWasWhiteSpace)
                {
                    numberOfWordsProcessed++;
                }

                lastCharactersWasWhiteSpace = true;

                if (numberOfWordsProcessed == length)
                {
                    return StringHumanizeExtensions.Concat(truncationString.AsSpan(), value.AsSpan(i + 1).TrimEnd());
                }
            }
            else
            {
                lastCharactersWasWhiteSpace = false;
            }
        }
        return truncationString + value;
    }
}
