namespace MarathonSkills.WpfApp.Extensions
{
    public static class Int32Extension
    {
        /// <summary>
        /// Возвращает логическое значение, находится ли число num между n1 и n2 включительно.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static bool IsBeth(this int num, int n1, int n2) => num >= n1 && num <= n2;
    }
}
