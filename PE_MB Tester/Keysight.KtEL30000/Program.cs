/// <copyright>3Shape A/S</copyright>

using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Ivi.Driver;
using Ivi.Visa;
using Ivi.Visa.FormattedIO;
using Keysight.KtEL30000;

namespace ThreeShape.KtEL30000_connect
{
    /// <summary>
    /// KtEL30000 IVI.NET Driver C# Example Program
    ///
    /// Creates a driver object, reads a few Identity interface properties, and checks the instrument error queue.
    /// May include additional instrument specific functionality.
    ///
    /// See driver help topic "Programming with the IVI.NET Driver in Various Development Environments"
    /// for additional programming information.
    ///
    /// Runs in simulation mode without an instrument.
    ///
    /// Requires a reference to the driver's .NET assembly.
    ///
    /// </summary>
    class Program
    {
        public static int Main(string[] args)
        {
            const int connected = 111;
            const int fail = -1;
            const int divider = 1000;
            const double maxCurrent = 1.3;
            const double currentIncreasement = 0.01;
            const string enableInput = "INP ON, ";
            const string disableInput = "INP OFF, ";
            const string identify = "*IDN?";
            const string setCCMode = "FUNC CURR, ";
            const string checkMode = "FUNC? ";
            const string CCMode = "CURR";

            string command;
            string visaAdress = "";
            string ELName = "";
            string externalCommand = "";
            int returnValue = fail;

            Console.WriteLine("KtEL30000 control app");
            Console.WriteLine();

            // Edit the initialization options as needed.
            var options = "QueryInstrStatus=false, Simulate=false, DriverSetup= Trace=false";
            var idquery = true;
            var reset = true;
            visaAdress = "USB0::0x2A8D::0x3702::MY60260113::0::INSTR";

            //Variables needed for measurement
            string channelList = "(@1)";

            // Create a connection (session) to the instrument
            IMessageBasedSession session;



            try
            {
                session = GlobalResourceManager.Open(visaAdress) as IMessageBasedSession;

            }
            catch (NativeVisaException visaException)
            {
                Console.WriteLine("Couldn't connect. Error is:\r\n{0}\r\nPress any key to exit...", visaException);
                Console.ReadKey();
                return fail;
            }


            MessageBasedFormattedIO formattedIO = new MessageBasedFormattedIO(session);

            // For Serial and TCP/IP socket connections enable the read Termination Character, or read's will timeout
            if (session.ResourceName.Contains("ASRL") || session.ResourceName.Contains("SOCKET"))
                session.TerminationCharacterEnabled = true;

            // Send the *IDN? and read the response as strings
            formattedIO.WriteLine(identify);
            string idnResponse = formattedIO.ReadLine();





            Console.WriteLine("*IDN? returned: {0}", idnResponse);
            command = setCCMode;
            formattedIO.WriteLine(command);
            command = checkMode;
            formattedIO.WriteLine(command);
            idnResponse = formattedIO.ReadLine();
            Console.WriteLine(idnResponse);

            Console.WriteLine("{0} returned: {1}", command, idnResponse);
            if(idnResponse.Contains(CCMode))
            {
                Console.WriteLine("OK");
            }
            /*
                        if (args.Length == 3)
                        {
                            resourceName = args[0];
                            ElName = args[1];
                            externalCommand = args[2];
                            try
                                {
                                    session = GlobalResourceManager.Open(resourceName) as IMessageBasedSession;

                                }
                            catch (NativeVisaException visaException)
                            {
                                Console.WriteLine("Couldn't connect. Error is:\r\n{0}\r\nPress any key to exit...", visaException);
                                Console.ReadKey();
                                return fail;
                            }
                            MessageBasedFormattedIO formattedIO = new MessageBasedFormattedIO(session);
                            string ELResponse = "";

                            // For Serial and TCP/IP socket connections enable the read Termination Character, or read's will timeout
                            if (session.ResourceName.Contains("ASRL") || session.ResourceName.Contains("SOCKET"))
                            {
                                session.TerminationCharacterEnabled = true;
                            }
                            if (externalCommand == "connect")
                            {
                                // Send the *IDN? and read the response as strings
                                command = identify;
                                formattedIO.WriteLine(command);
                                ELResponse = formattedIO.ReadLine();
                                Console.WriteLine("{0} returned: {1}", command, ELResponse);

                                if (ELResponse.Contains(ELName))
                                {
                                    returnValue = connected;
                                }
                            }else if (externalCommand == "test")
                            {
                                command = setCCMode;
                                formattedIO.WriteLine(command);

                                ELResponse = formattedIO.ReadLine();
                                Console.WriteLine("{0} returned: {1}", command, ELResponse);
                            }
                        }
            */
            // Close the connection to the instrument
            session.Dispose();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            return returnValue;
        }
    }
}



            /*











                // Edit resourceName and options as needed.  resourceName is ignored if option Simulate=true
                // For this example, resourceName may be a VISA address(e.g. "TCPIP0::<IP_Address>::INSTR") or a VISA alias.
                // For more information on using VISA aliases, refer to the Keysight IO Libraries Connection Expert documentation.

                if (args.Length == 2)
        {
            resourceName = args[0];
            command = args[1];

            // Edit the initialization options as needed.
            var options = "QueryInstrStatus=false, Simulate=false, DriverSetup= Trace=false";
            var idquery = true;
            var reset = true;

            //Variables needed for measurement
            string channelList = "(@1)";

            // Chceck connection with instrument and return connected value
            if (command == "connect")
            {
                try
                {
                    // Call driver constructor with options.  'using' block calls driver.Close() when exiting.
                    using (var driver = new KtEL30000(resourceName, idquery, reset, options))
                    {
                        Console.WriteLine("Driver Initialized");

                        // Print a few IIviDriverIdentity properties
                        Console.WriteLine("Identifier:  {0}", driver.Identity.Identifier);
                        Console.WriteLine("Revision:    {0}", driver.Identity.Revision);
                        Console.WriteLine("Vendor:      {0}", driver.Identity.Vendor);
                        Console.WriteLine("Description: {0}", driver.Identity.Description);
                        Console.WriteLine("Model:       {0}", driver.Identity.InstrumentModel);
                        Console.WriteLine("FirmwareRev: {0}", driver.Identity.InstrumentFirmwareRevision);
                        Console.WriteLine("Serial #:    {0}", driver.System.SerialNumber);
                        Console.WriteLine("\nSimulate:    {0}\n", driver.DriverOperation.Simulate);

                        // Check instrument for errors
                        ErrorQueryResult result;
                        Console.WriteLine();
                        do
                        {
                            result = driver.Utility.ErrorQuery();
                            Console.WriteLine("ErrorQuery: {0}, {1}", result.Code, result.Message);
                        } while (result.Code != 0);

                        returnValue = connected;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }

            // Connect to the instrument and perform test sequence, return power limiter activate current
            else if (command == "test")
            {
                try
                {
                    //Set the test start current
                    double startCurrent = 1.1;
                    double currentMeasurement = fail;

                    // Call driver constructor with options.  'using' block calls driver.Close() when exiting.
                    using (var driver = new KtEL30000(resourceName, idquery, reset, options))
                    {
                        Console.WriteLine("Driver Initialized");

                        //Change to Constant Current mode and check
                        driver.Operation.ConfigureMode(OperationMode.ConstantCurrent, channelList);
                        System.Threading.Thread.Sleep(10);
                        if (driver.Operation.GetConfigurationMode(channelList)[0] == OperationMode.ConstantCurrent)
                        {
                            // sets the current and check
                            driver.Operation.ConstantCurrent.SetCurrent(startCurrent, channelList);
                            Console.WriteLine("sets current to {0}A", startCurrent);
                            System.Threading.Thread.Sleep(10);
                            if (driver.Operation.ConstantCurrent.GetCurrent(channelList)[0] == startCurrent)
                            {
                                // Initiates the trigger measurement.
                                //driver.Acquisition.Initiate(channelList);
                                //Console.WriteLine("Initiated the trigger measurement.");

                                //enable output and check
                                driver.IOControl.SetEnabled(true, channelList);
                                System.Threading.Thread.Sleep(10);
                                if (driver.IOControl.GetEnabled(channelList)[0] == true)
                                {
                                    //Set current measurement enabled and check
                                    driver.Measurement.SetCurrentMeasurementEnabled(true, channelList);
                                    System.Threading.Thread.Sleep(10);
                                    if (driver.Measurement.GetCurrentMeasurementEnabled(channelList)[0] == true)
                                    {
                                        //Measure current and increase
                                        currentMeasurement = driver.Measurement.Measure(MeasurementFunction.Current, channelList)[0];
                                        //check if output is still enabled and the current is below max value
                                        while (currentMeasurement <= maxCurrent && currentMeasurement >= startCurrent)
                                        {
                                            //convert to int to return (returned value has to be divide by divider value)
                                            returnValue = (int)(currentMeasurement * divider);
                                            //increase set current value by currentIncreasement value and then read real current value
                                            driver.Operation.ConstantCurrent.SetCurrent(currentMeasurement + currentIncreasement,
                                                channelList);
                                            System.Threading.Thread.Sleep(10);
                                            currentMeasurement = driver.Measurement.Measure(MeasurementFunction.Current,
                                                channelList)[0];
                                        }
                                        //Abort the pending triggered measurements.
                                        //driver.Acquisition.Abort(channelList);
                                        //Console.WriteLine("Aborted the pending triggered measurements");

                                        // Check instrument for errors
                                        ErrorQueryResult result;
                                        Console.WriteLine();
                                        do
                                        {
                                            result = driver.Utility.ErrorQuery();
                                            Console.WriteLine("ErrorQuery: {0}, {1}", result.Code, result.Message);
                                        } while (result.Code != 0);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }
            */

