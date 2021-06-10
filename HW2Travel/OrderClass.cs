using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2Travel
{
    /*
     * OrderClass is a class that contains at least the following private data members:
     *      senderId: the identity of the sender(Travel Agent ID), you can use thread name or thread id;
     *      cardNo: an integer that represents a credit card number;
     *      receiverID: the identity of the receiver, you can use thread name or a unique name that defined for an Airline;
     *      amount: an integer that represents the number of seats to order;
     * You must use public methods to set and get the private data members. 
     * You must decide if these methods need to be synchronized.
     * The instances created from this class are of the OrderObjects.
     */
    class OrderObject
    {
        private string senderId;
        private int cardNo;
        private string receiverID;
        private int amount;

        //default constructor
        public OrderObject()
        {
            senderId = "";
            cardNo = 0;
            receiverID = "";
            amount = 0;
        }

        //overloaded constructor
        public OrderObject(string sender, int card, string receiver, int amount)
        {
            senderId = sender;
            cardNo = card;
            receiverID = receiver;
            this.amount = amount;
        }


        /*
         * Encoder is aclass or a method in a class: The Encoder class will convert an OrderObject into a string. 
         * You can choose any way to encode the values into a string, 
         * as long as you can decode the string to the original order object. 
         * You can use a class or a method to implement the Encoder.
         */
        public string encoder()
        {
            string encode = senderId + "," + cardNo + "," + receiverID + "," + amount;
            //Console.WriteLine(encode);
            return encode;
        }

        public void decode(string coded)
        {
            string[] order = coded.Split(',');//split up the order 
            senderId = order[0];
            cardNo = Int32.Parse(order[1]);
            receiverID = order[2];
            amount = Int32.Parse(order[3]);

        }


        //getters
        public string getSenderId()
        {
            return senderId;
        }

        public int getCardNo()
        {
            return cardNo;
        }

        public string getReciverID()
        {
            return receiverID;
        }

        public int getAmount()
        {
            return amount;
        }

        //setters
        public void setSenderID(string newID)
        {
            senderId = newID;
        }

        public void setCardNo(int newCard)
        {
            cardNo = newCard;
        }

        public void setReciverID(string newID)
        {
            receiverID = newID;
        }

        public void setAmount(int newAmt)
        {
            amount = newAmt;
        }
    }
}