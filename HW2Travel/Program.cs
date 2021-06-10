using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HW2Travel
{
    class Program
    {
        public static MultiCellBuffer mainBuffer = new MultiCellBuffer();

        static void Main(string[] args)
        {
            Airline airline1 = new Airline();
            Airline airline2 = new Airline();

            //setting up travelAgency Threads
            TravelAgency travelAgency = new TravelAgency();
            airline1.promo += travelAgency.orderTickets;
            airline2.promo += travelAgency.orderTickets;//having the travel agency subscribe to the promo event
            Thread agency0 = new Thread(new ThreadStart(travelAgency.runAgency));
            agency0.Name = "T0";
            agency0.Start();
            TravelAgency travelAgency1 = new TravelAgency();
            airline1.promo += travelAgency1.orderTickets;
            airline2.promo += travelAgency1.orderTickets;//having the travel agency subscribe to the promo event
            Thread agency1 = new Thread(new ThreadStart(travelAgency1.runAgency));
            agency1.Name = "T1";
            agency1.Start();
            TravelAgency travelAgency2 = new TravelAgency();
            airline1.promo += travelAgency2.orderTickets;
            airline2.promo += travelAgency2.orderTickets;//having the travel agency subscribe to the promo event
            Thread agency2 = new Thread(new ThreadStart(travelAgency2.runAgency));
            agency2.Name = "T2";
            agency2.Start();
            TravelAgency travelAgency3 = new TravelAgency();
            airline1.promo += travelAgency3.orderTickets;
            airline2.promo += travelAgency3.orderTickets;//having the travel agency subscribe to the promo event
            Thread agency3 = new Thread(new ThreadStart(travelAgency3.runAgency));
            agency3.Name = "T3";
            agency3.Start();
            TravelAgency travelAgency4 = new TravelAgency();
            airline1.promo += travelAgency4.orderTickets;
            airline2.promo += travelAgency4.orderTickets;//having the travel agency subscribe to the promo event
            Thread agency4 = new Thread(new ThreadStart(travelAgency4.runAgency));
            agency4.Name = "T4";
            agency4.Start();

            Thread airThread1 = new Thread(new ThreadStart(airline1.runAirline));
            airThread1.Name = "A0";
            airThread1.Start();
            Thread airThread2 = new Thread(new ThreadStart(airline2.runAirline));
            airThread2.Name = "A1";
            airThread2.Start();


            airThread1.Join();
            airThread2.Join();
            Console.WriteLine("AirlineDone");
            try
            {
                agency0.Join();
                agency1.Join();
                agency2.Join();
                agency3.Join();
                agency4.Join();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("End of Program");
           // airThread1.Join();
            //airThread2.Join();
            Console.WriteLine("End of Program");
        }
    }
}
