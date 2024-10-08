﻿namespace Mortgage.Ecosystem.BusinessLogic.Layer.Helpers
{
    internal class MathHelper
    {
        public static double Percent(double amount, double total, int decimalPlaces)
        {
            return Math.Round((amount / total) * 100, decimalPlaces);
        }

        public static double Sum(params double[] values)
        {
            return values.Sum();
        }
    }
}
