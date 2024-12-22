
using Filtering;

//Directory where to save the pre-filt and post-filt data
string dir = @"/Users/fran/Downloads";
try
{
    //Set the current directory.
    Directory.SetCurrentDirectory(dir);
}

catch (DirectoryNotFoundException e)
{
    Console.WriteLine("The specified directory does not exist. {0}", e);
}

//Build the signal to be used to test the filter
int order_filt = 4;
int length_test_signal = 3000;
double[] test_signal = new double[length_test_signal];
double[] time = new double[length_test_signal];
double freq_data_I = 1.0/40;
double freq_data_II = 1.0/250;

double f1 = freq_data_II - 1.0/500; //High Pass
double f2 = freq_data_II + 1.0/500; //Low Pass
double sf = 1.0;

double[] output_filt_signal = new double[length_test_signal];

for (int kk = 0; kk < length_test_signal; kk++)
{
    time[kk] = (double)kk / sf;
    test_signal[kk] = Math.Sin(2 * Math.PI * time[kk] * freq_data_I) + Math.Sin(2 * Math.PI * time[kk] * freq_data_II);
}

using (StreamWriter sw = new StreamWriter("Pre_Filtered_data.txt"))
{
    for (int hh = 0; hh < length_test_signal; hh++)
    {
        sw.WriteLine(Convert.ToString(test_signal[hh]));
    }
}

using (StreamWriter sw = new StreamWriter("Time_domain.txt"))
{
    for (int hh = 0; hh < length_test_signal; hh++)
    {
        sw.WriteLine(Convert.ToString(time[hh]));
    }
}

var filtered = IFilter.BandPassFilter(f1,f2,sf,order_filt,test_signal);

using (StreamWriter sw = new StreamWriter("Filtered_dataBP.txt"))
{
    for (int hh = 0; hh < length_test_signal; hh++)
    {
        sw.WriteLine(Convert.ToString(filtered[hh]));
    }
}


var filteredLP = IFilter.LowPassFilter(f2,sf,order_filt,test_signal)  - IFilter.LowPassFilter(f1,sf,order_filt,test_signal);
using (StreamWriter sw = new StreamWriter("Filtered_dataLP.txt"))
{
    for (int hh = 0; hh < length_test_signal; hh++)
    {
        sw.WriteLine(Convert.ToString(filteredLP[hh]));
    }
}