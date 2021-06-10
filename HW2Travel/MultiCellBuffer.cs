using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HW2Travel
{
    /*
     * MultiCellBuffer class is used for the communication between the travel agencies(clients) and the Airlines(server): 
     * This class has n data cells, you can set n=3 for this project. 
     * The number of cells available must be less than (<) the max number of travel agencies in your experiment. 
     * A setOneCell and getOneCell methods can be defined to write data into and to read data from one of the available cells.
     * MultiCellBuffer keep orders to both Airlines. 
     * Each Airlinesreads orders intended to that Airline only. If an Airline reads and order intended to the other Airline, 
     * order will not be read from the multi-cell buffer.
     */
    
    class MultiCellBuffer
    {
        private string[] buffer = new string[3];
        private bool writable = true;

        public void setOneCell(string value, int index)
        {
            while(!writable)
            {
                try
                {
                    Monitor.Wait(this);
                }
                catch(Exception e) { Console.WriteLine(e); }
            }
            buffer[index] = value;
            writable = false;
            Monitor.PulseAll(this);
        }

        public string getOneCell(int index)
        {
            string output = "";
            while(writable)
            {
                try
                {
                    Monitor.Wait(this);
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
            output = buffer[index];
            writable = true;
            Monitor.PulseAll(this);
            return output;
        }
    }
}
