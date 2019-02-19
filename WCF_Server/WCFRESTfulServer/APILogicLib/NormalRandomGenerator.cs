using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib
{
    class NormalRandomGenerator
    {
        private int Min { get; set; }
        private int Max { get; set; }
        public double StandardDeviation { get; set; }
        public double Mean { get; set; }

        private double _maxToGenerateForProbability;
        private double _minToGenerateForProbability;

        private Random _rGen = new Random();

        // Key is x, value is p(x)
        private Dictionary<int, double> probabilities = new Dictionary<int, double>();

        public NormalRandomGenerator(int min, int max)
        {
            this.Min = min;
            this.Max = max;

            // Assume random normal distribution from [min..max]
            // Calculate mean. For [4 .. 6] the mean is 5.
            this.Mean = ((max - min) / 2) + min;

            // Calculate standard deviation
            double xMinusMyuSquaredSum = 0;
            for (int i = min; i < max; i++)
            {
                xMinusMyuSquaredSum += Math.Pow(i - this.Mean, 2);
            }

            this.StandardDeviation = Math.Sqrt(xMinusMyuSquaredSum / (max - min + 1));
            // Flat, uniform distros tend to have a stdev that's too high; for example,
            // for 1-10, stdev is 3, meaning the ranges are 68% in 2-8, and 95% in -1 to 11...
            // So we cut this down to create better statistical variation. We now
            // get numbers like: 1dev=68%, 2dev=95%, 3dev=99% (+= 1%). w00t!
            this.StandardDeviation *= (0.5);

            for (int i = min; i < max; i++)
            {
                probabilities[i] = calculatePdf(i);
                // Eg. if we have: 1 (20%), 2 (60%), 3 (20%), we want to see
                // 1 (20), 2 (80), 3 (100)

                // Avoid index out of range exception
                if (i - 1 >= min)
                {
                    probabilities[i] += probabilities[i - 1];
                }
            }

            this._minToGenerateForProbability = this.probabilities.Values.Min();
            this._maxToGenerateForProbability = this.probabilities.Values.Max();
        }

        public double calculatePdf(int x)
        {
            // Formula from Wikipedia: http://en.wikipedia.org/wiki/Normal_distribution
            // f(x) = e ^ [-(x-myu)^2 / 2*sigma^2]
            //        -------------------------
            //         root(2 * pi * sigma^2)

            double negativeXMinusMyuSquared = -(x - this.Mean) * (x - this.Mean);
            double variance = StandardDeviation * StandardDeviation;
            double twoSigmaSquared = 2 * variance;
            double twoPiSigmaSquared = Math.PI * twoSigmaSquared;

            double eExponent = negativeXMinusMyuSquared / twoSigmaSquared;
            double top = Math.Pow(Math.E, eExponent);
            double bottom = Math.Sqrt(twoPiSigmaSquared);

            return top / bottom;
        }

        public int Next()
        {
            // map [0..1] to [minToGenerateForProbability .. maxToGenerateForProbability]
            // If we have a negative (eg. [-50 to 100]), generate [0 to 150] and subtract 50 to get [-50 to 100]
            double pickedProb = this._rGen.NextDouble() * (this._maxToGenerateForProbability - this._minToGenerateForProbability);
            pickedProb -= this._minToGenerateForProbability;

            for (int i = this.Min; i < this.Max; i++)
            {
                if (pickedProb <= this.probabilities[i])
                {
                    return i;
                }
            }

            throw new InvalidOperationException("Internal error: your algorithm is flawed, young Jedi.");
        }
    }
}
