namespace QueryingOmega.Extensions
{
    internal static class IntExtensions
    {
        public static int ThisIfLess(this int number, int comparison)
        {
            return number < comparison ? comparison : number;
        }
    }
}