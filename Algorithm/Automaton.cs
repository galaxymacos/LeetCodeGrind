using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class Automaton
    {
        private string state = "start";

        private Dictionary<string, List<string>> table = new Dictionary<string, List<string>>()
        {
            {"start", new List<string>() {"start", "signed", "in_number", "end"}},
            {"signed", new List<string>() {"end", "end", "in_number", "end"}},
            {"in_number", new List<string>() {"end", "end", "in_number", "end"}},
            {"end", new List<string>() {"end", "end", "end", "end"}}
        };

        private int GetCol(char c)
        {
            if (c == ' ') return 0;
            if (c == '+' || c == '-') return 1;
            if (c >= '0' && c <= '9') return 2;
            return 3;
        }

        public int sign = 1;
        public long ans = 0;

        public void Get(char c)
        {
            state = table[state][GetCol(c)];
            if (state == "in_number")
            {
                ans = ans * 10 + c - '0';
                ans = sign == 1? Math.Min(ans, (long)int.MaxValue):Math.Min(ans, -(long)int.MaxValue);
            }
            else if (state == "signed")
            {
                sign = c == '+' ? 1 : -1;
            }
        }


    }
}
