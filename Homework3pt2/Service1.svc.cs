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

namespace Homework3pt2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(string value1, string value2)
        {
            Double valueOne = Double.Parse(value1);
            Double valueTwo = Double.Parse(value2);

            return (valueOne + valueTwo).ToString();
        }
        public string SolarIntesity(string latatude, string longitude)
        {
            //string lat = latatude.ToString();
            //string lon = longitude.ToString();
            //place latatude and longitude at end of api call to get solar data from power.larc.nasa.gov
            //string url = @"https://power.larc.nasa.gov/cgi-bin/v1/DataAccess.py?&request=execute&identifier=SinglePoint&parameters=DNR,DNR_MAX,DNR_MIN,DIFF_MAX,DIFF_MIN,DIFF,KT,ALLSKY_SFC_SW_DWN&userCommunity=SSE&tempAverage=CLIMATOLOGY&outputList=JSON&lat=" + latatude + "&lon=" + longitude;
            //@"https://power.larc.nasa.gov/cgi-bin/v1/DataAccess.py?&request=execute&identifier=SinglePoint&parameters=SI_EF_MIN_TILTED_SURFACE,SI_EF_MAX_TILTED_SURFACE,SI_EF_TILTED_SURFACE,DNR,DNR_MAX,DNR_MIN,DIFF_MAX,DIFF_MIN,DIFF,KT,ALLSKY_SFC_SW_DWN&userCommunity=SSE&tempAverage=CLIMATOLOGY&outputList=JSON&lat=33.0000&lon=-111.0000";
            //string url = "https://power.larc.nasa.gov/cgi-bin/v1/DataAccess.py?&request=execute&identifier=SinglePoint&parameters=DNR&userCommunity=SSE&tempAverage=CLIMATOLOGY&outputList=JSON&lat=" + latatude + "&lon="+longitude;
            string url = "https://power.larc.nasa.gov/cgi-bin/v1/DataAccess.py?&request=execute&identifier=SinglePoint&parameters=EQVLNT_NO_SUN_BLACKDAYS_MONTH,INSOL_MIN_CONSEC_MONTH&userCommunity=SSE&tempAverage=CLIMATOLOGY&outputList=ASCII&lat=33.0000&lon=-111.0000";
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

            Rootobject rootObject = JsonConvert.DeserializeObject<Rootobject>(readResponse);
            Console.WriteLine(rootObject.ToString());

            readResponse = rootObject.features[0].properties.parameter.DNR._1.ToString();
            float average = (rootObject.features[0].properties.parameter.DNR._1 + rootObject.features[0].properties.parameter.DNR._2 + rootObject.features[0].properties.parameter.DNR._3
                + rootObject.features[0].properties.parameter.DNR._4 + rootObject.features[0].properties.parameter.DNR._5 + rootObject.features[0].properties.parameter.DNR._6
                + rootObject.features[0].properties.parameter.DNR._7 + rootObject.features[0].properties.parameter.DNR._8 + rootObject.features[0].properties.parameter.DNR._9
                + rootObject.features[0].properties.parameter.DNR._10 + rootObject.features[0].properties.parameter.DNR._11 + rootObject.features[0].properties.parameter.DNR._12
                + rootObject.features[0].properties.parameter.DNR._13) / 13;//find the average of each monthly DNR

            readResponse = average.ToString();

            return readResponse;//return string of average radiation
        }

        public class Rootobject
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
            public string json { get; set; }
        }

        public class Parameterinformation
        {
            public DNR DNR { get; set; }
        }

        public class DNR
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
            public DNR1 DNR { get; set; }
        }

        public class DNR1//Direct Normal Radiation
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






}
