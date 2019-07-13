using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationOfQueueBasedParking
{
    class Parking
    {
        static int carNumber = FormParking.carNumber;
        public static SimpleQueue simpleQueue = new SimpleQueue(carNumber);
        public static PriorityQueue priorityQueue = new PriorityQueue(carNumber);

        public void AddCarToParking()
        {
            Random randomProcessingTime = new Random();

            for (int i = 0; i < carNumber; i++)
            {
                Car a = new Car();
                a.ProcessingTime = randomProcessingTime.Next(10, 300);
                a.Number = i + 1;
                simpleQueue.Insert(a);
            }
        }

        public string ListCarParking()
        {
            string strList = "";
            foreach (Car a in simpleQueue.Peek())
            {
                strList += "" + a.Number + " Processing Time Of The Numbered Vehicle : " + a.ProcessingTime + Environment.NewLine;
            }
            return strList;
        }

        public string RemoveCarParking()
        {
            int passingTime = 0, i = 1;
            string strList = "";
            foreach (Car a in simpleQueue.Peek())
            {
                passingTime += a.ProcessingTime;
                strList += i++ + "nd car from exit the parking lot --> " + "Stay in the queue time of the number " + a.Number + " vehicle"+ passingTime + Environment.NewLine;
                simpleQueue.Remove();
            }
            return strList;
        }

        public void AddToPriorityCar()
        {
            Car b = new Car();
            foreach (Car a in simpleQueue.Peek())
            {
                b = a;
                priorityQueue.Insert(b);
            }
        }

        public string ListPriorityCar()
        {
            string strList = "";
            foreach (Car a in priorityQueue.Peek())
            {
                strList += "" +  "Processing time of vehicle number : " + a.Number + a.ProcessingTime + Environment.NewLine;
            }
            return strList;
        }

        public string PriorityCarDelete()
        {
            int passingTime = 0, i = 1;
            string strList = "";
            foreach (Car a in priorityQueue.Peek())
            {
                passingTime += a.ProcessingTime;
                strList += i++ + "nd car from exit the parking lot --> " + "Stay in the queue time of the number : "  + a.Number + passingTime + Environment.NewLine;
            }
            foreach (Car b in priorityQueue.Peek())
            {
                priorityQueue.Remove();
            }
            return strList;
        }

        public int AverageProcessingTimeCalculation()
        {
            int total = 0, avg = 0;
            foreach (Car a in simpleQueue.Peek())
            {
                total += a.ProcessingTime;
            }
            avg = total / carNumber;
            return avg;
        }

        public string CalculateEarnings()
        {
            double[,] array = new double[carNumber, 3];
            int passingTime = 0, i = 0, carCounter = 1;
            double difference = 0, percentage = 0;
            string strDifferencePercentage = "";

            foreach (Car a in simpleQueue.Peek())
            {
                passingTime += a.ProcessingTime;
                array[i, 0] = carCounter;
                array[i, 1] = passingTime;
                i++;
                carCounter++;
            }

            passingTime = 0;
            foreach (Car b in priorityQueue.Peek())
            {
                for (i = 0; i < carNumber; i++)
                {
                    if (b.Number == array[i, 0])
                    {
                        passingTime += b.ProcessingTime;
                        array[i, 2] = passingTime;
                    }
                }
            }

            for (i = 0; i < carNumber; i++)
            {
                if (array[i, 1] > array[i, 2])
                {
                    difference = array[i, 1] - array[i, 2];
                    percentage = ((array[i, 1] - array[i, 2]) / array[i, 2]) * 100;
                    percentage = Math.Round(percentage, 2);
                    strDifferencePercentage += "" + " Vehicle number" + array[i, 0] + " --> Waited " + difference + " seconds less. There is " + percentage + " % gain from time." + Environment.NewLine;
                }
            }
            if (strDifferencePercentage == "")
            {
                strDifferencePercentage = "No time-saving vehicles.";
            }
            return strDifferencePercentage;
        }
    }
}
