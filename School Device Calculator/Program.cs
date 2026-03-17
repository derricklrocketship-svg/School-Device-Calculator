namespace School_Device_Calculator
{
    internal class Program
    {


        static List<Device> devices = new List<Device>();

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

        

        }

        //Declares verables that are gana be used in OneDevice
        class Device
        {
            public string Name;
            public int Category;
            public double Cost;
        }


        //Main OneDivice for
        public static string OneDevice()
        {
            // Beginse the OneDevice buy asking for the name / cost / and the category it belongs to
            Console.WriteLine("--------Device Input--------");

            Console.WriteLine("\nPlease enter the device name:");
            string name = Console.ReadLine();

            double cost;
            Console.WriteLine("\nEnter device cost:");

            // cheaks if the input is a double and if it is 0 or over and 6000 or under
            while(true)
            {
                if (double.TryParse(Console.ReadLine(), out cost))
                {
                    if (cost >= 0 && cost <= 900000)
                    {
                        //if betwen the numbers and its a double then brake loop
                        break;
                    }
                    else
                    {
                        //if not betwen thouse numbers than give them this error
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Value Is To Low Or High");
                        Console.ResetColor();
                    }
                }
                else
                {

                    //tell the user if the value is to high or low
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Value Is Not Representing Cost");
                    Console.ResetColor();
                }

            }
            

            Console.WriteLine("\nPlease choose the category:\n\r1) Laptop\n\r2) Desktop\n\r3) Other");
            

            int category;
            //makes sure that the user enters the corect number 1,2 or 3
            while (true)
            {
                


                // tryparse and prase are diffrent
                // i lernt that try pares is for converting a string from console.readling to a int but it only works on valid input and will crash if the input is not valid
                //tryparse works the same like parse but if the user inputs a string it will retern a false insted of a true
                
                //cheacs if the input is valid int not a string
                if (int.TryParse(Console.ReadLine(), out category))
                {


                    if (category >= 1 && category <= 3)
                    {
                        //if the user enters the valid number then break the loop which is the while(true) loop


                        break;
                    }
                    else
                    {
                        //but if the user does not enter the valid number then give them this error that is red and since there is no break the loop continuse

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: Please enter 1, 2, or 3 only.");
                        Console.ResetColor();
                    }
                }
                else
                {

                    //if the input is a string like "h"
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: You Have Enterd Words Please Enter A Number 1,2 Or 3.");
                    Console.ResetColor();
                }


                
                
              
                
            }       

            //whatns the user enters the device infomation it will add a new device in the device list and write a Device added successfully to the user

            Device newDevice = new Device();
            newDevice.Name = name;
            newDevice.Cost = cost;
            newDevice.Category = category;

            devices.Add(newDevice);
            //returns this string to the user after it is doen
            return "Device Added Successfully!";
        }























    }
}
