using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Statistics.Descriptive;

namespace Statistics.Comparisons.Parametric
{
    /// <summary>
    /// Independent confidence interval for mean differences
    /// using student's t-distribution.
    /// Assumes the varianes are NOT equal.
    /// </summary>
    public class ConfidenceIntervalForMeanDifference
    {
        protected BasicStatistics statsVariable1;
        protected BasicStatistics statsVariable2;
        protected ConfidenceLevel level;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="statsVariable1">Statistics object for variable 1</param>
        /// <param name="statsVariable2">Statistics object for variable 2</param>
        public ConfidenceIntervalForMeanDifference(BasicStatistics statsVariable1, BasicStatistics statsVariable2)
        {
            this.statsVariable1 = statsVariable1;
            this.statsVariable2 = statsVariable2;
            this.level = ConfidenceLevel.NinetyFive;
        }


        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="statsVariable1">Statistics object for variable 1</param>
        /// <param name="statsVariable2">Statistics object for variable 2</param>
        /// <param name="level">Chosen confidence level</param>
        public ConfidenceIntervalForMeanDifference(BasicStatistics statsVariable1, BasicStatistics statsVariable2, ConfidenceLevel level)
        {
            this.statsVariable1 = statsVariable1;
            this.statsVariable2 = statsVariable2;
            this.level = level;
        }

        /// <summary>
        /// T-distribution table (2-tailed).
        /// </summary>
        /// <returns>Dictionary containing table values stored by ConfidenceLevel</returns>
        protected Dictionary<ConfidenceLevel, List<double>> TTable()
        {
            var tTable = new Dictionary<ConfidenceLevel, List<double>>();

            tTable.Add(ConfidenceLevel.Ninety, new List<double>() { 6.314, 2.92, 2.353, 2.132, 2.015, 1.943, 1.895, 1.86, 1.833, 1.812, 1.796, 1.782, 1.771, 1.761, 1.753, 1.746, 1.74, 1.734, 1.729, 1.725, 1.721, 1.717, 1.714, 1.711, 1.708, 1.706, 1.703, 1.701, 1.699, 1.697, 1.684, 1.676, 1.671, 1.664, 1.66, 1.658, 1.645 });
            tTable.Add(ConfidenceLevel.NinetyFive, new List<double>() { 12.71,	4.303,	3.182,	2.776,	2.571,	2.447,	2.365,	2.306,	2.262,	2.228,	2.201,	2.179,	2.16,	2.145,	2.131,	2.12,	2.11,	2.101,	2.093,	2.086,	2.08,	2.074,	2.069,	2.064,	2.06,	2.056,	2.052,	2.048,	2.045,	2.042,	2.021,	2.009,	2,	1.99,	1.984,	1.98,	1.96});
            tTable.Add(ConfidenceLevel.NinetyNine, new List<double>() { 31.82, 6.965, 4.541, 3.747, 3.365, 3.143, 2.998, 2.896, 2.821, 2.764, 2.718, 2.681, 2.65, 2.624, 2.602, 2.583, 2.567, 2.552, 2.539, 2.528, 2.518, 2.508, 2.5, 2.492, 2.485, 2.479, 2.473, 2.467, 2.462, 2.457, 2.423, 2.403, 2.39, 2.374, 2.364, 2.358, 2.326 });

            return tTable;
        }

        /// <summary>
        /// Standard error of the mean
        /// </summary>
        public double StandardError
        {
            get
            {
                return Math.Sqrt((this.statsVariable1.Variance / this.statsVariable1.N) + (this.statsVariable2.Variance / this.statsVariable2.N));
            }
        }


        /// <summary>
        /// The half width of the confidence interval.  Interval = Mean +- HalfWidth
        /// </summary>
        public double HalfWidth
        {
            get
            {
                return CriticalValue() * StandardError;
            }
        }

        /// <summary>
        /// The mean difference (variable 2 - variable 1)
        /// </summary>
        public double Mean
        {
            get
            {
                return this.statsVariable2.Mean - this.statsVariable1.Mean;
            }
        }

        /// <summary>
        /// The lowerbound of the confidence interval
        /// Mean - halfwidth
        /// </summary>
        public double LowerBound
        {
            get
            {
                return this.Mean - this.HalfWidth;
            }
        }

        /// <summary>
        /// The upperbound of the confidence interval
        /// Mean + halfwidth
        /// </summary>
        public double UpperBound
        {
            get
            {
                return this.Mean + this.HalfWidth;
            }
        }

        /// <summary>
        /// Convervative: Assumes variances are NOT equal.  
        /// Returns min(N1 -1,, N2 -2)
        /// </summary>
        public int DegreesOfFreedom
        {
            get
            {
                return Math.Min(statsVariable1.N - 1, statsVariable2.N - 1);
            }
        }

        private double CriticalValue()
        {
            if (this.DegreesOfFreedom < 120)
            {
                return TTable()[this.level][this.DegreesOfFreedom];
            }
            else
            {
                var table = TTable();
                return table[this.level][table.Count -1];
            }
        }
    }
}
