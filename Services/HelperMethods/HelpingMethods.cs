using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.HelperMethods
{
    static internal class HelpingMethods
    {
        static internal double GetAverage(IEnumerable<int> values)
        {
            double sum = 0;
            foreach (double value in values)
            {
                sum += value;
            }
            return sum / values.Count();
        }
    }
}
