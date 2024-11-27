using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.ViewModels
{
    public class ScoreDistributionBy
    {
        public double Score { get; set; }

        public int AccountScore { get; set; } = 0;
    }
    public class ScoreData
    {
        public double Score { get; set; } 
        public int CountScore { get; set; } 
    }

    public class SummaryData
    {
        public int TotalPass { get; set; }
        public int TotalFail { get; set; }
    }

    public class ScoreDistribution
    {
        public List<ScoreData> Data { get; set; }
        public SummaryData Summary { get; set; }
    }
}
