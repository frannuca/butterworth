using MathNet.Numerics.LinearAlgebra;

namespace Filtering;

public static class IFilter
{
    public static Vector<double>  BandPassFilter(double flow, double fhigh, double fs, int order, double[] signal)
    {
        var Nyquist_F = fs / 2.0;
        var IBI = new IIR_ButterworthFilter();
       var coeff_final = IBI.Lp2bp(flow / Nyquist_F, fhigh / Nyquist_F, order);

       var stability_check = IBI.Check_stability_iir(coeff_final);
       if (!stability_check)
       {
           throw new Exception("The filter is unstable");
       }

       return  Vector<double>.Build.DenseOfArray(IBI.Filter_Data(coeff_final, signal));
    }
    
    public static Vector<double> LowPassFilter(double fcut, double fs, int order, double[] signal)
    {
        var Nyquist_F = fs / 2.0;
        var IBI = new IIR_ButterworthFilter();
        var coeff_final = IBI.Lp2lp(fcut / Nyquist_F, order);

        var stability_check = IBI.Check_stability_iir(coeff_final);
        if (!stability_check)
        {
            throw new Exception("The filter is unstable");
        }

        return Vector<double>.Build.DenseOfArray(IBI.Filter_Data(coeff_final, signal));
    }
}