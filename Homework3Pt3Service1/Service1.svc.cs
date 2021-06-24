using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;

namespace Homework3Pt3Service1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        //returns complex json with weather and best day to install
        public forcast getForcast(string lat, string lon)
        {
            //get json of daily forcast
            string url = "https://api.openweathermap.org/data/2.5/onecall?lat=" + lat + "&lon=" + lon + "&exclude=minutely,hourly&appid="+ APIKeys.APIkey;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = webRequest.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string readResponse = reader.ReadToEnd();//single string with JSON
            response.Close();

            Rootobject root = JsonConvert.DeserializeObject<Rootobject>(readResponse);

            forcast future = new forcast();
            future.day = new Day[root.daily.Length];//day is same length as 
            string testString = "";
            Day bestDay = new Day();//saving data about the best day
            int dayIndex = 0;//index of best day
            for (int i = 0; i < root.daily.Length; i++)
            {
                testString = "Day: " + i + "\n";
                future.day[i] = new Day();
                future.day[i].weatherType = root.daily[i].weather[0].description;
                future.day[i].avgTemp = (root.daily[i].temp.max + root.daily[i].temp.min) / 2;
                future.day[i].windSpeed = root.daily[i].wind_speed;
                future.day[i].rainChance = root.daily[i].pop;
                System.DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                date = date.AddSeconds(root.daily[i].dt).ToLocalTime();//convert from unix time to human readable time
                future.day[i].date = date.ToString();
                //the best day prioritizes low rain, low wind, and temp closet to 75 degress
                if (i == 0)
                {
                    bestDay = future.day[i];//set the best day to the first day of the week to check against the others
                    dayIndex = 0;
                }
                if (future.day[i].rainChance < bestDay.rainChance)//lower chance of rain found
                {
                    bestDay = future.day[i];
                    dayIndex = i;
                }
                else if (future.day[i].windSpeed < bestDay.windSpeed)//lower windspeed found
                {
                    bestDay = future.day[i];
                    dayIndex = i;
                }
                else if (future.day[i].avgTemp < bestDay.avgTemp && bestDay.avgTemp > 75)//average temp lower than previous day and better than 75
                {
                    bestDay = future.day[i];
                    dayIndex = i;
                }
                else if (future.day[i].avgTemp > bestDay.avgTemp && bestDay.avgTemp <= 75)//average temp higher than previous day and better than 75
                {
                    bestDay = future.day[i];
                    dayIndex = i;
                }

            }
            future.bestDay = future.day[dayIndex].date.ToString();

            //System.DateTime datetime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //datetime = datetime.AddSeconds(root.daily[0].dt).ToLocalTime();//convert from unix time to human readable time

            return future;
        }


        public solarInfo getSolar(string lat, string lon)
        {
            string url = "https://power.larc.nasa.gov/cgi-bin/v1/DataAccess.py?&request=execute&identifier=SinglePoint&parameters=EQVLNT_NO_SUN_BLACKDAYS_MONTH,INSOL_MIN_CONSEC_MONTH&userCommunity=SSE&tempAverage=CLIMATOLOGY&outputList=ASCII&lat="+ lat + "&lon=" + lon;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = webRequest.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string readResponse = reader.ReadToEnd();//single string with JSON
            response.Close();

            //replace numbered values to values that can be used as variables
            readResponse = readResponse.Replace("\"1\"", "\"_1\"");
            readResponse = readResponse.Replace("\"2\"", "\"_2\"");
            readResponse = readResponse.Replace("\"3\"", "\"_3\"");
            readResponse = readResponse.Replace("\"4\"", "\"_4\"");
            readResponse = readResponse.Replace("\"5\"", "\"_5\"");
            readResponse = readResponse.Replace("\"6\"", "\"_6\"");
            readResponse = readResponse.Replace("\"7\"", "\"_7\"");
            readResponse = readResponse.Replace("\"8\"", "\"_8\"");
            readResponse = readResponse.Replace("\"9\"", "\"_9\"");
            readResponse = readResponse.Replace("\"10\"", "\"_10\"");
            readResponse = readResponse.Replace("\"11\"", "\"_11\"");
            readResponse = readResponse.Replace("\"12\"", "\"_12\"");
            readResponse = readResponse.Replace("\"13\"", "\"_13\"");

            Rootobject2 rootObject = JsonConvert.DeserializeObject<Rootobject2>(readResponse);

            //determine wether to install solar or not

            solarInfo info = new solarInfo();
            info.minimumSolarPercentages = new float[12];
            info.daysWithoutSun = new float[12];
            info.averages = new Average();


            info.minimumSolarPercentages[0] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._1;
            info.minimumSolarPercentages[1] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._2;
            info.minimumSolarPercentages[2] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._3;
            info.minimumSolarPercentages[3] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._4;
            info.minimumSolarPercentages[4] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._5;
            info.minimumSolarPercentages[5] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._6;
            info.minimumSolarPercentages[6] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._7;
            info.minimumSolarPercentages[7] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._8;
            info.minimumSolarPercentages[8] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._9;
            info.minimumSolarPercentages[9] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._10;
            info.minimumSolarPercentages[10] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._11;
            info.minimumSolarPercentages[11] = rootObject.features[0].properties.parameter.INSOL_MIN_CONSEC_MONTH._12;
            

            info.daysWithoutSun[0] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._1;
            info.daysWithoutSun[1] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._2;
            info.daysWithoutSun[2] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._3;
            info.daysWithoutSun[3] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._4;
            info.daysWithoutSun[4] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._5;
            info.daysWithoutSun[5] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._6;
            info.daysWithoutSun[6] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._7;
            info.daysWithoutSun[7] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._8;
            info.daysWithoutSun[8] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._9;
            info.daysWithoutSun[9] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._10;
            info.daysWithoutSun[10] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._11;
            info.daysWithoutSun[11] = rootObject.features[0].properties.parameter.EQVLNT_NO_SUN_BLACKDAYS_MONTH._12;
            float minPercentAvg = 0;
            float noSunAvg = 0;
            for (int i = 0; i < info.minimumSolarPercentages.Length; i++)
            {
                minPercentAvg += info.minimumSolarPercentages[i];
                noSunAvg += info.daysWithoutSun[i];
            }
            info.averages.dayWithoutSun = noSunAvg / 12;
            info.averages.minSolarPercentage = minPercentAvg / 12;

            if(info.averages.minSolarPercentage > 50)//more than 50% of days with sun
            {
                info.getSolar = "Get Solar";
            }
            else
            {
                info.getSolar = "Do not get Solar";
            }


                return info;
        }
    }


    public class Rootobject
    {
        public int lat { get; set; }
        public int lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public Current current { get; set; }
        public Daily[] daily { get; set; }
    }

    public class Current
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public float temp { get; set; }
        public float feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float dew_point { get; set; }
        public float uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public float wind_speed { get; set; }
        public int wind_deg { get; set; }
        public float wind_gust { get; set; }
        public Weather[] weather { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Daily
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public Temp temp { get; set; }
        public Feels_Like feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float dew_point { get; set; }
        public float wind_speed { get; set; }
        public int wind_deg { get; set; }
        public Weather1[] weather { get; set; }
        public int clouds { get; set; }
        public int pop { get; set; }
        public float uvi { get; set; }
    }

    public class Temp
    {
        public float day { get; set; }
        public float min { get; set; }
        public float max { get; set; }
        public float night { get; set; }
        public float eve { get; set; }
        public float morn { get; set; }
    }

    public class Feels_Like
    {
        public float day { get; set; }
        public float night { get; set; }
        public float eve { get; set; }
        public float morn { get; set; }
    }

    public class Weather1
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }


    public class Rootobject2
    {
        public Feature[] features { get; set; }
        public Header header { get; set; }
        public object[] messages { get; set; }
        public Outputs outputs { get; set; }
        public Parameterinformation parameterInformation { get; set; }
        public object[][] time { get; set; }
        public string type { get; set; }
    }

    public class Header
    {
        public string api_version { get; set; }
        public string fillValue { get; set; }
        public string range { get; set; }
        public string title { get; set; }
    }

    public class Outputs
    {
        public string ascii { get; set; }
    }

    public class Parameterinformation
    {
        public EQVLNT_NO_SUN_BLACKDAYS_MONTH EQVLNT_NO_SUN_BLACKDAYS_MONTH { get; set; }
        public INSOL_MIN_CONSEC_MONTH INSOL_MIN_CONSEC_MONTH { get; set; }
    }

    public class EQVLNT_NO_SUN_BLACKDAYS_MONTH
    {
        public string longname { get; set; }
        public string units { get; set; }
    }

    public class INSOL_MIN_CONSEC_MONTH
    {
        public string longname { get; set; }
        public string units { get; set; }
    }

    public class Feature
    {
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
        public string type { get; set; }
    }

    public class Geometry
    {
        public float[] coordinates { get; set; }
        public string type { get; set; }
    }

    public class Properties
    {
        public Parameter parameter { get; set; }
    }

    public class Parameter
    {
        public EQVLNT_NO_SUN_BLACKDAYS_MONTH1 EQVLNT_NO_SUN_BLACKDAYS_MONTH { get; set; }
        public INSOL_MIN_CONSEC_MONTH1 INSOL_MIN_CONSEC_MONTH { get; set; }
    }

    public class EQVLNT_NO_SUN_BLACKDAYS_MONTH1
    {
        public float _1 { get; set; }
        public float _10 { get; set; }
        public float _11 { get; set; }
        public float _12 { get; set; }
        public float _13 { get; set; }
        public float _2 { get; set; }
        public float _3 { get; set; }
        public float _4 { get; set; }
        public float _5 { get; set; }
        public float _6 { get; set; }
        public float _7 { get; set; }
        public float _8 { get; set; }
        public float _9 { get; set; }
    }

    public class INSOL_MIN_CONSEC_MONTH1
    {
        public float _1 { get; set; }
        public float _10 { get; set; }
        public float _11 { get; set; }
        public float _12 { get; set; }
        public float _13 { get; set; }
        public float _2 { get; set; }
        public float _3 { get; set; }
        public float _4 { get; set; }
        public float _5 { get; set; }
        public float _6 { get; set; }
        public float _7 { get; set; }
        public float _8 { get; set; }
        public float _9 { get; set; }
    }


}
