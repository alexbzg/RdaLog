﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamRadio

{

    public class Mode
    {
        public static readonly string[] Names = new string[] {"CW", "SSB", "FT8", "RTTY"};
        public static readonly Dictionary<string, string> DefRst = new Dictionary<string, string>
        {
            {"CW", "599" },
            {"SSB", "59" },
            { "FT8", "-10" },
            { "RTTY", "599" }
        };
    }
    public class Band
    {
        internal class BandEntry
        {
            internal string name;
            internal int[] limits;
            internal string waveLength;
        }
        static BandEntry[] Bands = new BandEntry[]
        {
            new BandEntry() { name = "1.8", limits = new int[] {1800, 2000 }, waveLength = "160M" },
            new BandEntry() { name = "3.5", limits = new int[] { 3500, 4000 }, waveLength = "80M" },
            new BandEntry() { name = "7", limits = new int[] { 7000, 7300 }, waveLength = "40M" },
            new BandEntry() { name = "10", limits = new int[] { 10000, 10150 }, waveLength = "30M" },
            new BandEntry() { name = "14", limits = new int[] { 14000, 14350 }, waveLength = "20M" },
            new BandEntry() { name = "18", limits = new int[] { 18068, 18168 }, waveLength = "17M" },
            new BandEntry() { name = "21", limits = new int[] {21000, 21450 }, waveLength = "15M" },
            new BandEntry() { name = "24", limits = new int[] { 24890, 24990 }, waveLength = "12M" },
            new BandEntry() { name = "28", limits = new int[] { 28000, 29700 }, waveLength = "10M" }
        };

        public static string[] Names
        {
            get
            {
                return Bands.Select(item => item.name).ToArray();
            }
        }
        public static string fromFreq(decimal freq)
        {
            foreach (BandEntry entry in Bands)
                if (freq >= entry.limits[0] && freq <= entry.limits[1])
                    return entry.name;
            return string.Empty;
        }

        public static string waveLength(string name)
        {
            return Bands.First(item => item.name == name).waveLength;
        }
    }

    public class Qso
    {
        public string ts;
        public string myCallsign;
        public string band;
        public string freq;
        public string mode;
        public string callsign;
        public string rstSnt;
        public string rstRcvd;
        public string rda;
        public int no;
        public string rafa;
        public string loc;
        public string[] userFields;
    }
}
