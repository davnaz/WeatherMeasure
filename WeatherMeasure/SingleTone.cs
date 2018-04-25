using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMeasure
{
    public class SingleTone<T> where T : class, new()
    {
        static protected T _instance = null;
        static public T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();

                return _instance;
            }
        }
    }
}
