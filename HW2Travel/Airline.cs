using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HW2Travel
{
    class Airline
    {
        private static Random rng = new Random(); // To generate random numbers
        private int price;
        private int p = 0;
        private string airlineName;

        public delegate void promotion(int price, string airlineName);//follows logic of travel agency
        public event promotion promo;//triggers travel agencys subscribed to this value

        public Airline()
        {
            price = 100;
            airlineName = "";
        }

        public void runAirline()//run for multithreading
        {
            airlineName = Thread.CurrentThread.Name;
            while(p<5)
            {
                Thread.Sleep(100);
                pricingModel();//increment price
                string buff = "-1";
                //get first order cell
                Monitor.Enter(Program.mainBuffer);
                try
                {
                    buff = Program.mainBuffer.getOneCell(0);
                    Console.WriteLine("Airline got order " + buff);
                }
                finally
                {
                    Monitor.Exit(Program.mainBuffer);

                }

                if(buff != null)//something in the buffer
                {
                    //string[] order = buff.Split(',');//split up the order to see if its the right airline
                    OrderObject order = new OrderObject();
                    order.decode(buff);//get all of the encoded information into the buffer
                    if (order.getReciverID() != Thread.CurrentThread.Name)//try again in new cell
                    {
                        Monitor.Enter(Program.mainBuffer);
                        try
                        {
                            buff = Program.mainBuffer.getOneCell(1);
                            Console.WriteLine("Airline got " + buff);
                        }
                        finally
                        {
                            Monitor.Exit(Program.mainBuffer);

                        }
                        if (buff != null)//something in the buffer
                        {
                            order.decode(buff);//get all of the encoded information into the buffer
                            if (order.getReciverID() != Thread.CurrentThread.Name)//try again in new cell
                            {
                                Monitor.Enter(Program.mainBuffer);
                                try
                                {
                                    buff = Program.mainBuffer.getOneCell(2);
                                    Console.WriteLine("Airline got " + buff);
                                }
                                finally
                                {
                                    Monitor.Exit(Program.mainBuffer);

                                }

                                if (buff != null)//something in the buffer
                                {
                                    order.decode(buff);//get all of the encoded information into the buffer
                                    if (order.getReciverID() != Thread.CurrentThread.Name)//try again in new cell
                                    {
                                        //there is nothing to do
                                    }
                                    else//create order thread for index 2
                                    {
                                        OrderProccessing order1 = new OrderProccessing(order, price);
                                        Thread processer = new Thread(new ThreadStart(order1.orderThread));
                                        processer.Start();
                                    }
                                }
                                else//there is nothing in any buffer slot
                                {
                                    Console.WriteLine("no order found");
                                }

                            }
                            else //make order thread for index 1
                            {
                                OrderProccessing order1 = new OrderProccessing(order, price);
                                Thread processer = new Thread(new ThreadStart(order1.orderThread));
                                processer.Start();
                            }

                        }
                       
                    }
                    else//make order thread for index 0
                    {
                        OrderProccessing order1 = new OrderProccessing(order, price);
                        Thread processer = new Thread(new ThreadStart(order1.orderThread));
                        processer.Start();
                    }
                }
                //p++;//increment p
            }

            Console.WriteLine("Airline{0} is finished", Thread.CurrentThread.Name);
        }

        /*
        * PricingModel: It can be a class or a method in Airlineclass. It decides the price of a seat. 
        * It can increase price or decrease the price. You must define a mathematical model (random function is fine) 
        * to determine the price based on the order received within a given time period 
        * and the number of seats available in the Airline in the same time period. 
        * You can use a hard-coded table of the price in each week day. 
        * However, you must make sure that your model will allow the price goes up some time 
        * and goes down some other time. 
        */
        private void pricingModel()
        {
            int newPrice = rng.Next(50, 150);
            //Console.WriteLine("Airline{0} current price: {1}", Thread.CurrentThread.Name, price);
            if (newPrice < price)//discount found
            {
                //emit promotional event
                if (promo != null)
                {
                    promo(newPrice, airlineName);//update travel agencies
                }
                p++;//airlines only update so much
                Console.WriteLine("Airline{0} promo price: {1}", Thread.CurrentThread.Name, price);
            }
            price = newPrice;//set price
        }
    }

    /*
     * OrderProcessing is a class or a method in a class on the supplier’s(airline) side. 
     * Whenever an order needs to be processed, 
     * a new thread is instantiated from this class (or method) to process the order. 
     * It will check the validity of the credit card number. 
     * You can define your credit card format, for example, the credit card number from the travel agencies 
     * must be a number registered to the Airline, or a number between two given numbers(e.g., between 5000 and 7000). 
     * Each OrderProcessing thread will calculate the total amount of charge, e.g., unitPrice*NoOfSeats+ Tax.
     * If the credit card is invalid, order will be declined.
     */
    class OrderProccessing
    {
        OrderObject order;
        int price;
        public OrderProccessing()
        {
            order = new OrderObject();
            price = 0;
        }

        public OrderProccessing(OrderObject newOrder, int price)
        {
            order = newOrder;
            this.price = price;
        }

        public void orderThread()//This thread determines what the order is correct and make a final price
        {
            int cardNo = order.getCardNo();
            int seats = order.getAmount();
            if(cardNo > 7000 || cardNo < 4000)//proper card numbers between 4000 and 7000
            {
                Console.WriteLine("Improper Card number!\n");
                
            }
            else
            {
                //print the order as processed, price is = unitPrice*amount + tax; tax = price*amount*.08
                double finalPrice = price * order.getAmount() + price * order.getAmount() * .08;
                Console.WriteLine("Order for {0} on Airline {1}, final price is: {2}\n", order.getSenderId(), order.getReciverID(), finalPrice);
            }
        }
    }
}
