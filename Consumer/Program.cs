using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Util;
using System;
using System.Text;

namespace Consumer
{
    /*
     Author: Alex Mahomana
     Cell  : 072 071 1650
     Email : alex.prijojo.prince@gmail.com

     App   : This is a console App to Listen & Receive Messages via RabbitMQ
         */
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

             String _queueName = "TestQueue1";//Convert.ToString(args[0]);

               
             var _Factory = new ConnectionFactory() { HostName = "localhost" };
            using (var _connection = _Factory.CreateConnection())
            {
                using (var _channel = _connection.CreateModel())
                {
                                      
                    _channel.QueueDeclare(_queueName, false, false, false, null);

                    var _consumer = new  QueueingBasicConsumer(_channel);

                    _channel.BasicConsume(_queueName, true, _consumer);
                

                    while(true)
                    {
                        var ea =  (BasicDeliverEventArgs) _consumer.Queue.Dequeue();
                        var _body = ea.Body;

                        var _message = Encoding.UTF8.GetString(_body);
                        var _msgArr = _message.Split(",");
                        var _name = _msgArr[1];
                        Console.WriteLine("Hello {0}, I am your father!", _name);

                    }
                   
                }
            }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }
    }
}
