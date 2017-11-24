/// Author: Samuel Arzt
/// Date: March

#region Includes
using System;
#endregion

/// <summary>
/// Static class for different Math operations and constants.
/// </summary>
namespace MathNN
{


    public static class MathHelper
    {
        #region Methods
        #region Activation Functions

        public static double SigmoidFunction(double xValue)
        {
            if (xValue > 10) return 1.0;
            else if (xValue < -10) return 0.0;
            else return 1.0 / (1.0 + Math.Exp(-xValue));
        }

     
        public static double TanHFunction(double xValue)
        {
            if (xValue > 10) return 1.0;
            else if (xValue < -10) return -1.0;
            else return Math.Tanh(xValue);
        }

 
        public static double SoftSignFunction(double xValue)
        {
            return xValue / (1 + Math.Abs(xValue));
        }
        #endregion
        #endregion
    }
}

