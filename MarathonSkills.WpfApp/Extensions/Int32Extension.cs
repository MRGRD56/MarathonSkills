using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.WpfApp
{
    public static class Int32Extension
    {
        public static bool IsBeth(this int num, int n1, int n2) => num >= n1 && num <= n2;
    }
}
