namespace Filtering;

public class SignalGenerator
{
    public static double[] GenerateSineWave(double amplitude, double frequency, double sampleRate, double duration)
    {
        int sampleCount = (int)(sampleRate * duration);
        double[] sineWave = new double[sampleCount];

        for (int i = 0; i < sampleCount; i++)
        {
            double t = i / sampleRate;
            sineWave[i] = amplitude * Math.Sin(2 * Math.PI * frequency * t);
        }

        return sineWave;
    }
}