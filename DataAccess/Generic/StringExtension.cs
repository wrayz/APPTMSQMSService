namespace DataAccess.Generic
{
    public static class StringExtension
    {
        public static string If(this string str, bool condition)
        {
            return condition ? str : string.Empty;
        }
    }
}
