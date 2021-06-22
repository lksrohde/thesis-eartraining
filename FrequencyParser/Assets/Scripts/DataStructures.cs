using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static DataStructures.GleichstufigFreq;

namespace DataStructures {
    public enum ReineIntervalleCent {
        PRIME = 0,
        KLEINE_SEKUNDE = 112,
        GROßE_SEKUNDE = 204,
        KLEINE_TERZ = 316,
        GROßE_TERZ = 386,
        QUARTE = 498,
        TRITONUS = 590,
        QUINTE = 702,
        KLEINE_SEXTE = 814,
        GROßE_SEXTE = 884,
        KLEINE_SEPTIME = 1018,
        GROßE_SEPTIME = 1088,
        OKTAVE = 1200
    }

    public enum GleichstufigFreq {
        c4 = 209300,
        h3 = 197553,
        ais3_b3 = 186466,
        a3 = 176000,
        gis3_as3 = 166122,
        g3 = 156798,
        fis3_ges3 = 147998,
        f3 = 139691,
        e3 = 131851,
        dis3_es3 = 124451,
        d3 = 117466,
        cis3_des3 = 110873,
        c3 = 104650,
        h2 = 98776,
        ais2_b2 = 93232,
        a2 = 88000,
        gis2_as2 = 83060,
        g2 = 78399,
        fis2_ges2 = 73998,
        f2 = 69845,
        e2 = 65925,
        dis2_es2 = 62225,
        d2 = 58733,
        cis2_des2 = 55436,
        c2 = 52325,
        h1 = 49388,
        ais1_b1 = 46616,
        a1 = 44000,
        gis1_as1 = 41530,
        g1 = 39199,
        fis1_ges1 = 36999,
        f1 = 34922,
        e1 = 32962,
        dis1_es1 = 31112,
        d1 = 29366,
        cis1_des1 = 27718,
        c1 = 26162, // Mittleres C
        h = 24694,
        ais_b = 23308,
        a = 22000,
        gis_as = 20765,
        g = 19599,
        fis_ges = 18499,
        f = 17461,
        e = 16481,
        dis_es = 15556,
        d = 14683,
        cis_des = 13859,
        c = 13081,
        H = 12347,
        Ais_B = 11654,
        A = 11000,
        Gis_As = 10382,
        G = 9799,
        Fis_Ges = 9249,
        F = 8730,
        E = 8240,
        Dis_Es = 7778,
        D = 7341,
        Cis_Des = 6929,
        C = 6540,
        H1 = 6173,
        Ais1_B1 = 5827,
        A1 = 5500,
        Gis1_As1 = 5191,
        G1 = 4899,
        Fis1_Ges1 = 4624,
        F1 = 4365,
        E1 = 4120,
        Dis1_Es1 = 3889,
        D1 = 3670,
        Cis1_Des1 = 3464,
        C1 = 3270,
        H2 = 3086,
        Ais2_B2 = 2913,
        A2 = 2750
    }

    public class VocalRanges {
        public readonly Tuple<GleichstufigFreq, GleichstufigFreq> Sopran =
            new Tuple<GleichstufigFreq, GleichstufigFreq>(c1, c3);

        public readonly  Tuple<GleichstufigFreq, GleichstufigFreq> MezzoSopran =
            new Tuple<GleichstufigFreq, GleichstufigFreq>(a, a2);

        public readonly  Tuple<GleichstufigFreq, GleichstufigFreq> Alt =
            new Tuple<GleichstufigFreq, GleichstufigFreq>(f, e2);

        public readonly  Tuple<GleichstufigFreq, GleichstufigFreq> Tenor =
            new Tuple<GleichstufigFreq, GleichstufigFreq>(H, a1);

        public readonly  Tuple<GleichstufigFreq, GleichstufigFreq> Bariton =
            new Tuple<GleichstufigFreq, GleichstufigFreq>(G, f1);

        public readonly  Tuple<GleichstufigFreq, GleichstufigFreq> Bass =
            new Tuple<GleichstufigFreq, GleichstufigFreq>(E, e1);

        public Tuple<GleichstufigFreq, GleichstufigFreq>[] ToArray() {
            return new []{Sopran, MezzoSopran, Alt, Tenor, Bariton, Bass};
        }
        public string[] ToStringArray() {
            return new []{"Sopran", "MezzoSopran", "Alt", "Tenor", "Bariton", "Bass"};
        }
        public int[] GetRangeValues(Tuple<GleichstufigFreq, GleichstufigFreq> range) {
            int length = 0;
            int start = 0;
            var enums = (int[]) Enum.GetValues(typeof(GleichstufigFreq));
            for (int i = 0; i < enums.Length; i++) {
                int item = enums[i];
                
                if (item == (int)range.Item1) {
                    start = i;
                }
                if (item == (int)range.Item2) {
                    length = i - start;
                }
            }

            int[] rangeArr = new int[length];
            
            Array.Copy(enums, start, rangeArr,0 , length);
            
            return rangeArr;
        }
        
        public string[] GetRangeNames(Tuple<GleichstufigFreq, GleichstufigFreq> range) {
            int length = 0;
            int start = 0;
            var enums = (int[]) Enum.GetValues(typeof(GleichstufigFreq));
            for (int i = 0; i < enums.Length; i++) {
                int item = enums[i];
                
                if (item == (int)range.Item1) {
                    start = i;
                }
                if (item == (int)range.Item2) {
                    length = i - start;
                }
            }

            string[] rangeArr = new string[length];
            string[] enumNames = Enum.GetNames(typeof(GleichstufigFreq));
            
            Array.Copy(enumNames, start, rangeArr,0 , length);
            
            return rangeArr;
        }
    }
}