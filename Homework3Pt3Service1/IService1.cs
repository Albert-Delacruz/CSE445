using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Homework3Pt3Service1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(
            UriTemplate = "GetData/{value}"
            )]
        string GetData(string value);
        // TODO: Add your service operations here

        [OperationContract]
        [WebInvoke(Method = "GET",
     ResponseFormat = WebMessageFormat.Json, UriTemplate = "getForcast/{lat}/{lon}"
            )
            ]
        forcast getForcast(string lat, string lon);

        [OperationContract]
        [WebInvoke(Method = "GET",
     ResponseFormat = WebMessageFormat.Json, UriTemplate = "getSolar/{lat}/{lon}"
            )]
        solarInfo getSolar(string lat, string lon);
    }

    [DataContract]
    public class forcast
    {
        [DataMember]
        public Day[] day { get; set; }
        [DataMember]
        public string bestDay { get; set; }
    }
   [DataContract]
    public class Day
    {
        [DataMember]
        public string weatherType { get; set; }
        [DataMember]
        public float avgTemp { get; set; }
        [DataMember]
        public float windSpeed { get; set; }
        [DataMember]
        public int rainChance { get; set; }
        [DataMember]
        public string date { get; set; }
    }
    [DataContract]
    public class solarInfo
    {
        [DataMember]
        public string getSolar { get; set; }
        [DataMember]
        public float[] minimumSolarPercentages { get; set; }
        [DataMember]
        public float[] daysWithoutSun { get; set; }
        [DataMember]
        public Average averages { get; set; }
    }

    [DataContract]
    public class Average
    {
        [DataMember]
        public float minSolarPercentage { get; set; }
        [DataMember]
        public float dayWithoutSun { get; set; }
    }
}
