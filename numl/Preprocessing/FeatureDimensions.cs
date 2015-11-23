﻿using numl.Math.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace numl.Preprocessing
{
    /// <summary>
    /// Feature Dimensions class
    /// <para>Used to automatically engineer new features and add model complexity for non-linear features.</para>
    /// </summary>
    public static class FeatureDimensions
    {
        /// <summary>
        /// Adds a specified number of polynomial features to the given Vector.
        /// </summary>
        /// <param name="x">Training / Testing record</param>
        /// <param name="polynomialFeatures">Number of polynomial features to add</param>
        /// <returns></returns>
        public static Vector IncreaseDimensions(this Vector x, int polynomialFeatures)
        {
            if (polynomialFeatures == 0)
                return x;

            Vector xtemp = x.Copy();
            int maxCols = xtemp.Length;
            for (int j = 0; j < maxCols - 1; j++)
            {
                for (int k = 1; k <= polynomialFeatures; k++)
                {
                    for (int m = 1; m <= k; m++)
                    {
                        double v = (System.Math.Pow(xtemp[j], (double)(k - m)) * System.Math.Pow(xtemp[j + 1], (double)m));
                        xtemp = xtemp.Insert(xtemp.Length - 1, v);
                    }
                }
            }
            return xtemp;
        }

        /// <summary>
        /// Adds a specified number of polynomial features to the given Matrix.
        /// </summary>
        /// <param name="x">Training set</param>
        /// <param name="polynomialFeatures">Number of polynomial features to add</param>
        /// <returns></returns>
        public static Matrix IncreaseDimensions(Matrix x, int polynomialFeatures)
        {
            if (polynomialFeatures == 0)
                return x;

            Matrix Xtemp = x.Copy();
            int maxCols = Xtemp.Cols;
            for (int j = 0; j < maxCols - 1; j++)
            {
                for (int k = 1; k <= polynomialFeatures; k++)
                {
                    for (int m = 1; m <= k; m++)
                    {
                        Vector v = (Xtemp[j, VectorType.Col].ToVector() ^ (double)(k - m)) * (Xtemp[j + 1, VectorType.Col] ^ (double)m).ToVector();
                        Xtemp = Xtemp.Insert(v, Xtemp.Cols - 1, VectorType.Col);
                    }
                }
            }
            return Xtemp;
        }
    }
}