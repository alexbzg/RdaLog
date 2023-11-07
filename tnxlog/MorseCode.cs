using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamRadio
{
    class MorseCode
    {
        public static readonly char Dot = '∙';
        public static readonly char Dash = '-';
        public static Dictionary<char, char[]> Alphabet = new Dictionary<char, char[]>()
        {
            { 'A', new char[] {Dot, Dash} },
            { 'B', new char[] { Dash, Dot, Dot, Dot }},
            { 'C', new char[] { Dash, Dot, Dash, Dot }},
            { 'D', new char[] { Dash, Dot, Dot }},
            { 'E', new char[] { Dot }},
            { 'F', new char[] { Dot, Dot, Dash, Dot }},
            { 'G', new char[] { Dash, Dash, Dot }},
            { 'H', new char[] { Dot, Dot, Dot, Dot }},
            { 'I', new char[] { Dot, Dot }},
            { 'J', new char[] { Dot, Dash, Dash, Dash }},
            { 'K', new char[] { Dash, Dot, Dash }},
            { 'L', new char[] { Dot, Dash, Dot, Dot }},
            { 'M', new char[] { Dash, Dash }},
            { 'N', new char[] { Dash, Dot }},
            { 'O', new char[] { Dash, Dash, Dash }},
            { 'P', new char[] { Dot, Dash, Dash, Dot }},
            { 'Q', new char[] { Dash, Dash, Dot, Dash }},
            { 'R', new char[] { Dot, Dash, Dot }},
            { 'S', new char[] { Dot, Dot, Dot }},
            { 'T', new char[] { Dash }},
            { 'U', new char[] { Dot, Dot, Dash }},
            { 'V', new char[] { Dot, Dot, Dot, Dash }},
            { 'W', new char[] { Dot, Dash, Dash }},
            { 'X', new char[] { Dash, Dot, Dot, Dash }},
            { 'Y', new char[] { Dash, Dot, Dash, Dash }},
            { 'Z', new char[] { Dash, Dash, Dot, Dot }},
            { '0', new char[] { Dash, Dash, Dash, Dash, Dash }},
            { '1', new char[] { Dot, Dash, Dash, Dash, Dash }},
            { '2', new char[] { Dot, Dot, Dash, Dash, Dash }},
            { '3', new char[] { Dot, Dot, Dot, Dash, Dash }},
            { '4', new char[] { Dot, Dot, Dot, Dot, Dash }},
            { '5', new char[] { Dot, Dot, Dot, Dot, Dot }},
            { '6', new char[] { Dash, Dot, Dot, Dot, Dot }},
            { '7', new char[] { Dash, Dash, Dot, Dot, Dot }},
            { '8', new char[] { Dash, Dash, Dash, Dot, Dot }},
            { '9', new char[] { Dash, Dash, Dash, Dash, Dot } },
            { '&', new char[] { Dot, Dash, Dot, Dot, Dot } },
            { '\'', new char [] { Dot, Dash, Dash, Dash, Dash, Dot }},
            { '@', new char [] { Dot, Dash, Dash, Dot, Dash, Dot }},
            { ')', new char [] { Dash, Dot, Dash, Dash, Dot, Dash }},
            { '(', new char [] { Dash, Dot, Dash, Dash, Dot }},
            { ':', new char [] { Dash, Dash, Dash, Dot, Dot, Dot }},
            { ',', new char [] { Dash, Dash, Dot, Dot, Dash, Dash }},
            { '=', new char [] { Dash, Dot, Dot, Dot, Dash }},
            { '!', new char [] { Dash, Dot, Dash, Dot, Dash, Dash }},
            { '.', new char [] { Dot, Dash, Dot, Dash, Dot, Dash }},
            { '-', new char [] { Dash, Dot, Dot, Dot, Dot, Dash }},
            { '+', new char [] { Dot, Dash, Dot, Dash, Dot }},
            { '"', new char [] { Dot, Dash, Dot, Dot, Dash, Dot }},
            { '?', new char [] { Dot, Dot, Dash, Dash, Dot, Dot }},
            { '/', new char [] { Dash, Dot, Dot, Dash, Dot}}
         };
    }
}
