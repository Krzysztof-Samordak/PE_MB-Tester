/// <copyright>3Shape A/S</copyright>

using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Ivi.Driver;
using Ivi.Visa;
using Ivi.Visa.FormattedIO;

namespace ThreeShape.KtEL30000_connect
{
    /// <summary>
    ///
    /// KtEL30000 IVI.NET Driver C# Windows Console Application
    /// for performing power limiter test
    ///
    /// </summary>
    class Program
    {
        public static int Main(string[] args)
        {
            //Default test parameters
            double startCurrent = 1.17;    //current value from which test is started
            double maxCurrent = 1.241;    //current value to which test is performing
            double currentIncreasement = 0.001;  //current increasement value
            double TurnOffVoltageLimit = 0.5;

            //Auxiliary variables
            const int connected = 111;
            const int fail = -1;
            const int limiterAutoResetFail = -2;
            const int divider = 1000;
            int returnValue = fail;
            const double currentErrorValue = 0.00001;
            double setCurrentValue;
            double zeroCurrent = 0.01;
            double result = fail;
            double[] tmp = new double[3];
            string visaAdress = ""; //VISA adress of DC Electronic Load
            string command = "";
            bool checkVoltageAfterTest = true;
            bool zeroVoltage = false;
            double measuredVoltage;

            //Variables needed for measurement
            string idnResponse;
            int MeasurementMilisecondsDelay = 200;  //delay between measurements
            int InputTurnOffMilisecondsDelay = 500;  //delay after turning input off

            //Crate a message model for communication with DC Electronic Load
            MessageBasedFormattedIO formattedIO;

            // Create a connection (session) to the instrument
            IMessageBasedSession session;

            // KtEL30000 Commands
            const string enableInput = "INP ON, ";
            const string disableInput = "INP OFF, ";
            const string identify = "*IDN?";
            const string setCCMode = "FUNC CURR, ";
            const string checkMode = "FUNC? ";
            const string CCMode = "CURR";
            const string readCurrent = "CURR?";
            const string readRealVoltage = "MEAS:VOLT?";
            const string readRealCurrent = "MEAS:CURR?";
            string setCurrent(double currentValue)
            {
                return "CURR " + currentValue;
            }

            //Main program part:

            Console.WriteLine("\nKtEL30000 control app\n");

            if (args.Length >= 2)
            {
                visaAdress = args[0];
                command = args[1];
                if (args[1] == "connect" || args[1] == "test")
                {
                    if (args.Length == 6)
                    {
                        bool isNumeric = double.TryParse(args[2], out tmp[0]) && double.TryParse(args[3], out tmp[1])
                            && double.TryParse(args[4], out tmp[2]) && bool.TryParse(args[5], out checkVoltageAfterTest); ;
                        if (isNumeric && tmp[0] >= 0 && tmp[1] >= 0 && tmp[2] > 0)
                        {
                            startCurrent = tmp[0];
                            maxCurrent = tmp[1];
                            currentIncreasement = tmp[2];
                        }
                        else
                        {
                            Console.WriteLine("Arguments args[2,3] must be >= 0 and args[4] must be > 0 and args[5] must be bool type!!!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Argument args[1] must be 'connect' or 'test' !!!");
                }
                try
                {
                    session = GlobalResourceManager.Open(visaAdress) as IMessageBasedSession;
                }
                catch (NativeVisaException visaException)
                {
                    Console.WriteLine("Couldn't connect. Error is:\r\n{0}\r\nPress any key to exit...", visaException);
                    return fail;
                }

                formattedIO = new MessageBasedFormattedIO(session);

                // For Serial and TCP/IP socket connections enable the read Termination Character, or read's will timeout
                if (session.ResourceName.Contains("ASRL") || session.ResourceName.Contains("SOCKET"))
                    session.TerminationCharacterEnabled = true;

                if (command == "connect")
                {
                    // Send the identify command and read the response as strings
                    command = identify;
                    formattedIO.WriteLine(command);
                    idnResponse = formattedIO.ReadLine();
                    Console.WriteLine("Command {0} returned: {1}", command, idnResponse);
                    returnValue = connected;

                }else if (command == "test")
                {
                    //Start test sequence: Set the CC mode, then set the current value and read it (check the execution of the command),
                    //enable Input, in for loop: keep increasing current and reading actual value of current until
                    //the power limiter is activated or max current is reached

                    setCurrentValue = startCurrent;
                    command = setCCMode;
                    formattedIO.WriteLine(command);
                    Console.WriteLine("Write command: {0}", command);

                    command = checkMode;
                    formattedIO.WriteLine(command);
                    idnResponse = formattedIO.ReadLine();
                    Console.Write("Command: {0} returned: {1}", command, idnResponse);
                    if (idnResponse.Contains(CCMode))
                    {
                        Console.Write("OK\n");

                        //Start with zero current
                        command = setCurrent(zeroCurrent);
                        formattedIO.WriteLine(command);
                        Console.WriteLine("\nWrite command: {0}", command);

                        command = readCurrent;
                        formattedIO.WriteLine(command);
                        idnResponse = formattedIO.ReadLine();
                        result = Convert.ToDouble(idnResponse);
                        Console.Write("Command: {0} returned: {1}", command, result);
                        if ((result.ToString()).Contains(zeroCurrent.ToString()))
                        {
                            Console.Write("\nOK\n");
                            command = enableInput;
                            formattedIO.WriteLine(command);
                            Console.WriteLine("\nWrite command: {0}", command);

                            for (setCurrentValue = startCurrent; setCurrentValue <= maxCurrent + currentErrorValue;
                                setCurrentValue += currentIncreasement)
                            {
                                command = setCurrent(setCurrentValue);
                                formattedIO.WriteLine(command);
                                Console.WriteLine("\nWrite command: {0}", command);

                                command = readCurrent;
                                formattedIO.WriteLine(command);
                                idnResponse = formattedIO.ReadLine();
                                Console.Write("Command: {0} returned: {1}", command, idnResponse);
                                if (idnResponse.Contains(setCurrentValue.ToString()))
                                {
                                    Console.Write("OK\n");
                                }
                                System.Threading.Thread.Sleep(MeasurementMilisecondsDelay);
                                command = readRealCurrent;
                                formattedIO.WriteLine(command);
                                idnResponse = formattedIO.ReadLine();
                                result = Convert.ToDouble(idnResponse);
                                Console.Write("\nCommand: {0} returned: {1}", command, result);
                                if (result < setCurrentValue - currentIncreasement) //subtract currentIncreasement value from
                                                                                    //setCurrentValue to include measurement accuracy error
                                {
                                    Console.WriteLine(" - Power limiter activated!!!");
                                    break;
                                }
                            }

                            command = disableInput;
                            formattedIO.WriteLine(command);

                            Console.WriteLine("\n\nWrite command: {0}", command);
                            if (!checkVoltageAfterTest)
                            {
                                zeroVoltage = true;
                            }else
                            {
                                System.Threading.Thread.Sleep(InputTurnOffMilisecondsDelay);
                                command = readRealVoltage;
                                formattedIO.WriteLine(command);
                                idnResponse = formattedIO.ReadLine();

                                Console.Write("\nCommand: {0} returned: {1}", command, idnResponse);

                                measuredVoltage = Convert.ToDouble(idnResponse);

                                Console.WriteLine("Voltage after turning input off: {0}V", measuredVoltage);
                                Console.WriteLine(String.Format("\n{0," + (Console.WindowWidth / 2) + "}", "###########################"));
                                Console.WriteLine(String.Format("\n{0," + (Console.WindowWidth / 2 - 8 ) +
                                    "}", "Voltage: " + Math.Round(measuredVoltage, 2) + "V"));
                                if (measuredVoltage < TurnOffVoltageLimit || setCurrentValue > maxCurrent)
                                {
                                    zeroVoltage = true;
                                }
                            }
                            if(zeroVoltage)
                            {
                                returnValue = Convert.ToInt32(setCurrentValue * divider);
                            }else
                            {
                                returnValue = limiterAutoResetFail;
                            }
                            Console.WriteLine(String.Format("\n{0," + (Console.WindowWidth / 2) + "}", "###########################"));
                            Console.WriteLine(String.Format("\n{0," + (Console.WindowWidth / 2 - 4) +
                                "}", "Test Result: " + Math.Round(setCurrentValue, 3) + "A"));
                            Console.WriteLine(String.Format("\n{0," + (Console.WindowWidth / 2) + "}", "###########################"));
                        }
                    }
                }
                session.Dispose();
            }
            else
            {
                Console.WriteLine("You have to provide 2 or 6 arguments: string visaAdress, string command," +
                    "double startCurrent, double maxCurrent, double currentIncreasement, bool checkVoltageAfterTest");
            }
            Console.WriteLine("\nReturn value: {0}\n", returnValue);

            return returnValue;
        }
    }
}