using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HW2Travel
{
    class TravelAgency
    {
        private int currentPrice, previousPrice;
        private static Random rng = new Random(); // To generate random numbers
        private bool promotionFound = false;
        private string airlineName, agencyName;
        public TravelAgency()
        {
            previousPrice = 0;
            currentPrice = 0;
            airlineName = "";
            agencyName = "";
        }


        public void runAgency()//run for multithreading
        {
            for(int i = 0; i < 10; i++)
            { 
               
                int seats = Math.Abs(currentPrice - previousPrice);//find the diffrence between prices for seat amount
                OrderObject order = new OrderObject(Thread.CurrentThread.Name,generateCard(),airlineName,seats);
                string convert = order.encoder();//convert order to string
                Monitor.Enter(Program.mainBuffer);
                try
                {
                    string input = Thread.CurrentThread.Name + " " + currentPrice;
                   Program.mainBuffer.setOneCell(convert, rng.Next(0,3));//places order into one of the buffer spots randomly
                    Console.WriteLine("Agency{0} sent order of " + convert,Thread.CurrentThread.Name);
                }
                finally
                {
                    Monitor.Exit(Program.mainBuffer);
                }
                //Console.WriteLine("Agency{0} at {1}", Thread.CurrentThread.Name,i);
            }
            Console.WriteLine("Agency{0} is finished", Thread.CurrentThread.Name);
        }

        public void orderTickets(int price, string airline)
        {
            try
            {
                Monitor.Enter(this);//checking if anything is writing to this object, 
            }
            catch (Exception e)
            {
                Console.WriteLine("HALP");
            }
            previousPrice = currentPrice;//getting old price into the object
            currentPrice = price;//saving new price
            airlineName = airline;
            //Console.WriteLine("New Price {0} found in Airline {1} from TravelAgency{2}!", currentPrice, airlineName, Thread.CurrentThread.Name);
            promotionFound = true;

            Monitor.Exit(this);
        }

        private int generateCard()
        {
            return rng.Next(3750, 7250);//will generate a card number between 3750 - 7250, 
            //proper card numbers are between 4000 and 7000
        }
    }
}
