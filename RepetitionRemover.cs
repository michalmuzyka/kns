﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kns
{
    internal static class RepetitionRemover
    {
        public static string RemoveRepetition(StringBuilder word)
        {
            string repetition = string.Empty;

            if (word == null || word.Length < 2)
                return repetition;

            var lastPos = word.Length - 1;
            var sameAsLastCharPostions = new List<int>();
            for (int i=0; i < lastPos; ++i)
            {
                if (word[i] == word[lastPos])
                    sameAsLastCharPostions.Add(i);
            }
            sameAsLastCharPostions.Reverse();
            foreach(var pos in sameAsLastCharPostions)
            {
                var len = lastPos - pos;
                if (len - 1 > pos)
                    break;

                bool ok = true;
                for (int i=0; i<len; ++i)
                {
                    if (word[pos - i] != word[lastPos - i])
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    repetition = word.ToString(pos + 1, len);
                    word.Remove(pos + 1, len);
                    break;
                }
            }

            return repetition;
        }

    }
}
