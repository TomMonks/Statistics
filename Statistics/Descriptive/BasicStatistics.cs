using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace Statistics.Descriptive
{
    public class BasicStatistics
    {
        protected List<double> data;

        protected double mean;
        protected double stdev;
                
        #region Properties


        /// <summary>
        /// The arithmetic average of the data points
        /// </summary>
        public double Mean
        {
            get
            {
                if (0 == mean)
                {
                    mean = CalculateMean();
                   
                }

                return mean;
            }
        }


        /// <summary>
        /// The standard deviation of the data points
        /// </summary>
        public double StdDev
        {
            get
            {
                if (0 == stdev)
                {
                    stdev = CalculateStandardDeviation();
                }

                return stdev;
            }
        }

        /// <summary>
        /// The variance of the datapoints
        /// </summary>
        public double Variance
        {
            get
            {
                if (0 == stdev)
                {
                    stdev = CalculateStandardDeviation();
                }

                return stdev * stdev;
            }
        }


        /// <summary>
        /// Median of the data points
        /// </summary>
        public double Median
        {
            get
            {
                int mid = this.data.Count / 2;
                return data[mid -1];
            }
        }


        /// <summary>
        /// Inter-quartile range od data points
        /// </summary>
        public double IQR
        {
            get
            {
                return Percentile(0.75) - Percentile(0.25);
            }
        }


        /// <summary>
        /// Minimum value of the data points
        /// </summary>
        public double Minimum
        {
            get
            {
                return data[0];
            }
        }


        /// <summary>
        /// Maximum value of the data points
        /// </summary>
        public double Maximum
        {
            get
            {
                return this.data[this.data.Count - 1];
            }
        }

        /// <summary>
        /// The number of data points
        /// </summary>
        public int N
        {
            get
            {
                return this.data.Count;
            }
        }
       
        #endregion

        public BasicStatistics(List<double> data)
        {
            this.data = data;
            this.data.Sort();
        }

        public BasicStatistics(DataTable data, int index = 0)
        {
            this.data = data.AsEnumerable().Select(x => Convert.ToDouble(x[index])).ToList();
            this.data.Sort();
        }

        /// <summary>
        /// Calculate the mean value of the data 
        /// </summary>
        /// <returns>The mean value of data</returns>
        protected double CalculateMean(){

            double runningTotal = 0;

            foreach (double value in this.data)
            {
                runningTotal += value;
            }

            return runningTotal / data.Count;
        }

        protected double CalculateStandardDeviation()
        {

            double runningTotal = 0;

            foreach (double value in this.data)
            {
                runningTotal += (value - Mean) * (value - this.Mean);
            }

            return Math.Sqrt(runningTotal / (data.Count - 1));
        }


        /// <summary>
        /// A percentile of the distribution
        /// </summary>
        /// <param name="p">A value between 0 and 1 e.g. 0.95 gives the 95th pecentile</param>
        /// <returns></returns>
        public double Percentile(double p)
        {
            if (p > 1 || p < 0)
            {
                throw new ArgumentOutOfRangeException("Percentile parameter must be between 0 and 1");
            }
            else if (this.N < 5)
            {
                throw new ArithmeticException("Percentiles can only be calculated when there are at least 5 data points");
            }
            else
            {
                int point = (int)Math.Round(this.data.Count * p, MidpointRounding.AwayFromZero);

                return this.data[point - 1];
            }
        }

    }
}
