using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikusClientServer
{
    class InputDataHandler
    {
        Dictionary<string, List<double>> axisData = new Dictionary<string, List<double>>();
        int maxDataCount;
        public InputDataHandler(int max, params string[] axisNames)
        {
            maxDataCount = max;
            for (int i = 0; i < axisNames.Length; i++)
            {
                axisData.Add(axisNames[i],new List<double>());
            }
        }
        public void reciveData(string whichAxis, double data)
        {
            axisData[whichAxis].Add(data);
            if(axisData[whichAxis].Count > maxDataCount)
            {
                axisData[whichAxis].RemoveAt(0);
            }
        }
        public double[] getAxisData(string axis)
        {
            return axisData[axis].ToArray();
        }
    }
}
