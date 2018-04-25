using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherMeasure
{
    class WeatherData : SingleTone<WeatherData> //
    {
        string temp;
        string humidity;
        string pressure;
        public WeatherData()
        {
            temp =     "-99999";
            humidity = "-99999";
            pressure = "-99999";
            
        }
        public delegate void MethodContainer();
        public event MethodContainer measurementsChanged;
        public string getTemperature()
        {
            return temp;
        }
        public string getHumidity()
        {
            return humidity;
        }
        public string getPressure()
        {
            return pressure;
        }
        public void refreshData(bool manually)
        {
            string old_temp = temp;
            string old_humidity = humidity;
            string old_pressure = pressure;
            string jsonSrtingData = WebHelper.GetHtml("http://api.wunderground.com/api/eccc00d10fcceeb2/geolookup/conditions/lang:RU/q/55.65346668,37.51875022.json");
            JObject WeathData = JObject.Parse(jsonSrtingData);
            temp = (string)WeathData["current_observation"]["temp_c"]; //температура
            temp = temp.Contains("-") ? temp + " °C" : "+" + temp + " °C"; //если температура плюсовая, то надо добавить плюс в строку, иначе оставляем
            humidity = ((string)WeathData["current_observation"]["relative_humidity"]).Replace("%"," %"); //влажность в процентах
            string pressure_inch = (string)WeathData["current_observation"]["pressure_in"]; //давление в дюймах рт.ст.
            pressure = ((Int32)(Double.Parse(pressure_inch, CultureInfo.InvariantCulture )* 25.4)).ToString()+ " мм рт.ст."; 
            if(temp != old_temp || humidity != old_humidity || pressure != old_pressure)  //если у нас меняется хоть один из показателей, значит произошло событие "данные обновились"
            {
                this.measurementsChanged();
                if (manually)
                {
                    MessageBox.Show("Данные источника обновились.");
                }

            }
            else
            {
                if (manually)
                {
                    MessageBox.Show("Данные источника не изменились.");
                }
            }
        }
    }
}
