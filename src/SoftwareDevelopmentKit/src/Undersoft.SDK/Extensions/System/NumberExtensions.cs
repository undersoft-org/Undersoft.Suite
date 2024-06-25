namespace System
{
    public static class NumberExtensions
    {
        public static decimal FromMarkup(this decimal f, int decimals = 8, int divider = 100)
        {
            return Math.Round(((divider * f) - divider), decimals);
        }

        public static decimal FromFraction(this decimal f, int decimals = 8, int divider = 100)
        {
            return Math.Round(((divider * f)), decimals);
        }

        public static decimal ToMarkup(this decimal f, int decimals = 8, int divider = 100)
        {
            return Math.Round(((divider + f) / divider), decimals);
        }

        public static decimal ToFraction(this decimal f, int decimals = 8, int divider = 100)
        {
            return Math.Round(f / divider, decimals);
        }

        public static double FromMarkup(this double f, int decimals = 6, int divider = 100)
        {
            return Math.Round(((divider * f) - divider), decimals);
        }

        public static double FromFraction(this double f, int decimals = 6, int divider = 100)
        {
            return Math.Round(((divider * f)), decimals);
        }

        public static double ToMarkup(this double f, int decimals = 6, int divider = 100)
        {
            return Math.Round((divider + f) / divider, decimals);
        }

        public static double ToFraction(this double f, int decimals = 6, int divider = 100)
        {
            return Math.Round(f / divider, decimals);
        }

        public static float FromMarkup(this float f, int decimals = 4, int divider = 100)
        {
            return (float)Math.Round(((divider * f) - divider), decimals);
        }

        public static float FromFraction(this float f, int decimals = 4, int divider = 100)
        {
            return (float)Math.Round(((divider * f)), decimals);
        }

        public static float ToMarkup(this float f, int decimals = 4, int divider = 100)
        {
            return (float)Math.Round((divider + f) / divider, decimals);
        }

        public static float ToFraction(this float f, int decimals = 4, int divider = 100)
        {
            return (float)Math.Round(f / divider, decimals);
        }

        public static decimal Round(this decimal f, int decimals = 8)
        {
            return Math.Round(f, decimals);
        }

        public static double Round(this double f, int decimals = 6)
        {
            return Math.Round(f, decimals);
        }

        public static float Round(this float f, int decimals = 4)
        {
            return (float)Math.Round(f, decimals);
        }


    }
}
