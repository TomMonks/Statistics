using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Descriptive
{
    public enum ConfidenceLevel
    {
        Ninety = 0,
        NinetyFive = 1,
        NinetyNine = 2

    }

    /// <summary>
    /// Constructs a 100%(1-alpha) confidence interval using the standard normal distribution
    /// Assumes large sample size (> 30 data points);
    /// </summary>
    public class ConfidenceIntervalStandardNormal
    {
        protected BasicStatistics statistics;
        protected ConfidenceLevel level;

        /// <summary>
        /// Standard error of the mean
        /// </summary>
        public double StandardError
        {
            get
            {
                return this.statistics.StdDev / Math.Sqrt(statistics.N);
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
        /// The lowerbound of the confidence interval
        /// Mean - halfwidth
        /// </summary>
        public double LowerBound
        {
            get
            {
                return this.statistics.Mean - this.HalfWidth;
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
                return this.statistics.Mean + this.HalfWidth;
            }
        }

        /// <summary>
        /// Constructor: assumes 95% confidence level
        /// </summary>
        /// <param name="statistics">Basic statistics for data</param>
        public ConfidenceIntervalStandardNormal(BasicStatistics statistics)
        {
            this.statistics = statistics;
            this.level = ConfidenceLevel.NinetyFive;
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="statistics">Basic statistics for data</param>
        /// <param name="level">Chosen confidence level</param>
        public ConfidenceIntervalStandardNormal(BasicStatistics statistics, ConfidenceLevel level)
        {
            this.statistics = statistics;
            this.level = level;
        }

        private double CriticalValue()
        {
            if (level == ConfidenceLevel.Ninety)
            {
                return 1.64;
            }
            else if (level == ConfidenceLevel.NinetyFive)
            {
                return 1.96;
            }
            else
            {
                return 2.58;
            }
        }

        public override string ToString()
        {
            return String.Format("({0} to {1})", LowerBound, UpperBound);
        }
        
        
    }
}
