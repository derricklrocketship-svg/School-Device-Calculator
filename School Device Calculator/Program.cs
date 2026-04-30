using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace School_Device_Calculator
{
    internal class Program
    {



        //Global variable
        //for the dispaly

        public static int Laptops = 0;
        public static int Desktop = 0;
        public static int Other = 0;

        public static string deviceslip = "";
        //for expenceve device
        public static string topdevice = "";

        public static int deviceamount = 0;

        public static double devicepricecount = 0;

        public static double topDevicevalue = 0;



        //Costant variable

        static readonly int[] MAXMINCOST = { 0, 1000000 };
        static readonly int[] MAXMINAMOUNT = { 0, 10000 };





        static void Main(string[] args)
        {
            //The starter words for my school device calculator
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   _____      _                 _   _____             _             _____      _            _       _             \r\n  / ____|    | |               | | |  __ \\           (_)           / ____|    | |          | |     | |            \r\n | (___   ___| |__   ___   ___ | | | |  | | _____   ___  ___ ___  | |     __ _| | ___ _   _| | __ _| |_ ___  _ __ \r\n  \\___ \\ / __| '_ \\ / _ \\ / _ \\| | | |  | |/ _ \\ \\ / / |/ __/ _ \\ | |    / _` | |/ __| | | | |/ _` | __/ _ \\| '__|\r\n  ____) | (__| | | | (_) | (_) | | | |__| |  __/\\ V /| | (_|  __/ | |___| (_| | | (__| |_| | | (_| | || (_) | |   \r\n |_____/ \\___|_| |_|\\___/ \\___/|_| |_____/ \\___| \\_/ |_|\\___\\___|  \\_____\\__,_|_|\\___|\\__,_|_|\\__,_|\\__\\___/|_|   \r\n                                                                                                                  \r\n                                                                                                                  ");
            Console.ResetColor();
            //intro for the app explaning what it does
            Console.WriteLine("\n\nWelcome to the School Device Calculator.\n\rThis program helps schools calculate the insurance value of their smart devices.\n\rYou will be asked to enter the device name, number of devices, cost per device, and the device category.\n\rThe program will apply the insurance discount rules, calculate the total insured value, and show how the\n\rdevice value decreases by 5% each month over six months. \n\r Press [Enter] to coninue");
            Console.ReadLine();
            Console.Clear();


            //if the user wants to continuse they press y
            char continueInput = 'y';
            while (continueInput == 'y' || continueInput.Equals('y'))
            {

                Console.WriteLine(OneDevice());

                Console.WriteLine("\n\nDo you want to enter another Device? (y/n)");



                continueInput = Console.ReadLine()[0];
                Console.Clear();


            }



            //for displaying each device info
            Console.WriteLine(deviceslip);


            //for displaying summury
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n================Full Summery================");
            Console.ResetColor();
            Console.WriteLine($"The number of Laptop: {Laptops}");
            Console.WriteLine($"The number of Desktop: {Desktop}");
            Console.WriteLine($"The number of other devices: {Other}");

            Console.WriteLine($"\nThe most expensive device - {topdevice} @ {topDevicevalue:C}");
            Console.WriteLine($"\nThe total value for insurance: {devicepricecount:C}");
            Console.WriteLine($"\nDevice Enterd: {deviceamount}");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n============================================");
            Console.ResetColor();










        }



        //Main OneDivice for
        public static string OneDevice()
        {


            // Beginse the OneDevice buy asking for the name / cost / and the category it belongs to

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--------Device Input--------");
            Console.ResetColor();

            List<string> QUESTIONS = new List<string> { "\nPlease Enter The Device Name:", "\nPlease Enter The Device Category:", "\nPlease Enter The Device Cost:", "\nPlease Enter The Device Amount:" };



            string name = CheckName(QUESTIONS[0]);



            double cost = Checkdouble(MAXMINCOST[0], MAXMINCOST[1], QUESTIONS[2]);



            int amount = CheckInt(MAXMINAMOUNT[0], MAXMINAMOUNT[1], QUESTIONS[3]);



            
            string category = CheckCategory(QUESTIONS[1]);




            //this is just to call them so thy can be saved in the deviceslip
            double totelCost = CalculateDiscount(cost, amount);
            double[] monthlyValues = MonthInturance(cost, amount);

            //if the device has a hire value than the topDevicevalue than replace it
            if (totelCost > topDevicevalue)
            {
                topDevicevalue = totelCost;
                topdevice = name;

            }

            //saves the device in deviceslip for display
            deviceslip += DeviceSlip(name, cost, amount, category, totelCost, monthlyValues);

            devicepricecount = devicepricecount + totelCost;

            deviceamount++;
            //returns this string to the user after it is doen     
            return "Device Added Successfully!";
        }


        //all will be stored in the deviseSlip private variable
        //device, double totelValue, double[] monthlyValue

        private static string DeviceSlip(string name, double cost, int amount, string category, double totelCost, double[] monthlyValues)
        {

            //makes the locol variable with nothing stored inside
            string deviceslip = "";

            deviceslip += "\n>>>>>>>>>>>>>>>>>>Device<<<<<<<<<<<<<<<<<<";

            //stors the (display the name of the device)
            deviceslip += $"\n{name}";
            //stores the (display the amount of this device)
            deviceslip += $"\nTotal cost for {amount} x {name} device is = {totelCost:C}";

            deviceslip += "\nMonth          Value Lost";
            //this code helps print the length of the 5 arrays that have been created
            for (int i = 0; i < monthlyValues.Length; i++)
            {
                //the month value i + 1 print out the the month first month becouse i itself starts at 0 not 1
                // and the monthlyvalue i print the value for that month
                //stores the (display the 5 month and the discount


                deviceslip += $"\n{i + 1}           {monthlyValues[i]:C}";
            }


            deviceslip += $"\nCATEGORY: {category}";


            deviceslip += "\n.........................................";



            return deviceslip;
        }




        // this method is for calculating the cost of the devices if it is over 6 devices and if not it passes through
        static double CalculateDiscount(double cost, int amount)
        {
            //declaring the variable
            double totelCost = 0;

            // this variable says that the amount of devices are 6 they get the same price
            int fullPrice = Math.Min(amount, 6);

            //this variable -6 in the device amount if there is more than 6 than it will take 6 away and discount only the ones that are left
            int discountPrice = Math.Max(0, amount - 6);

            //the vode takes the full price and cost and times it togather
            double fullPriceTotel = fullPrice * cost;

            //this variable takes the discount and cost which i placed it in bracket with 0.9 so it gives it a 10% discount that is called discountTotel
            double discountTotel = discountPrice * (cost * 0.9);


            // this code adds the fullPriceTotel and discountTotel that gives it the totelCost changing it from 0 to the new double
            totelCost = fullPriceTotel + discountTotel;



            return totelCost;


        }

        //method for the 6 month calculations
        //the double[] is so it can return a stored value
        static double[] MonthInturance(double cost, int amount)
        {
            //declares the valuble and the new double is that it make 6 new arrays that it can store
            double[] monthlyValues = new double[6];
            // this aculy took time for me and its job is to call the totelCost from calculateDiscount method
            double totelCost = CalculateDiscount(cost, amount);
            // just declares the totelCost as currentCost
            double currentCost = totelCost;

            // the for is for each month which is 6 month
            for (int month = 0; month < 6; month++)
            {
                // times all 6 months by o.95
                currentCost *= 0.95;
                //storse the currentCost in monthlyValues
                monthlyValues[month] = currentCost;

            }


            // returns the monthlyValuse
            return monthlyValues;
        }

        //checks the int's
        static int CheckInt(int min, int max, string ask)
        {
            //makes sure that the user enters the corect number 1,2 or 3
            while (true)
            {


                Console.WriteLine(ask);
                int intInput;
                string input = Console.ReadLine();
                // tryparse and prase are diffrent
                // i lernt that try pares is for converting a string from console.readling to a int but it only works on valid input and will crash if the input is not valid
                //tryparse works the same like parse but if the user inputs a string it will retern a false insted of a true

                //cheacs if the input is valid int not a string
                if (int.TryParse(input, out intInput))
                {


                    if (intInput >= min && intInput <= max)
                    {
                        //if the user enters the valid number then break the loop which is the while(true) loop

                        return intInput;

                    }

                }
                else
                {

                    //if the input is a string like "h"
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: Invalid number. Please enter {min} - {max} only.");
                    Console.ResetColor();
                }






            }
        }

        //checks the double's
        static double Checkdouble(int min, int max, string ask)
        {
            //makes sure that the user enters the corect number 1,2 or 3
            while (true)
            {

                Console.WriteLine(ask);
                int intInput;
                string input = Console.ReadLine();
                // tryparse and prase are diffrent
                // i lernt that try pares is for converting a string from console.readling to a int but it only works on valid input and will crash if the input is not valid
                //tryparse works the same like parse but if the user inputs a string it will retern a false insted of a true

                //cheacs if the input is valid int not a string
                if (int.TryParse(input, out intInput))
                {


                    if (intInput >= min && intInput <= max)
                    {
                        //if the user enters the valid number then break the loop which is the while(true) loop

                        return intInput;

                    }


                }
                else
                {

                    //if the input is a string like "h"
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: Invalid number. Please enter {min} - {max} only.");
                    Console.ResetColor();
                }






            }
        }

        //Check if a name is lowercase and convert to title case if nacesary


        static string CheckName(string ask)
        {
            string name;

            while (true)
            {
                Console.WriteLine(ask);
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Please Enter A Valid Device Name");
                    Console.ResetColor();

                    continue;
                }

              
                if (!Regex.IsMatch(name, @"^[a-zA-Z0-9\s]+$"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Please Enter A Device Name");
                    Console.ResetColor();
                    continue;

                }

                break;
            }

            // formatting AFTER validation
            name = Regex.Replace(name, @"\s+", " ").Trim();
            name = char.ToUpper(name[0]) + name.Substring(1);

            return name;
        }

        static string CheckCategory(string ask)
        {

            string category;

            //makes sure that the user enters the corract input
            while (true)
            {


                string input = Console.ReadLine();

                if (input == "1")
                {
                    category = "Laptop";
                    Laptops++;

                    return category;
                }
                else if (input == "2")
                {
                    category = "Desktop";
                    Desktop++;

                    return category;
                }
                else if (input == "3")
                {
                    category = "Other";
                    Other++;

                    return category;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Please Enter 1,2 Or 3");
                    Console.ResetColor();
                }
            }

        }






    }
}