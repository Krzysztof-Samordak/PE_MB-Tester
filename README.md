# PE_MB-Tester
The PE_MB Tester is a Windows desktop application for testing of USB hub controller and power limiter located on PCBA. The application needs the test hardware (10034463 PE_MB TESTER).  The program at first step detect the USB hub controller located on PCBA and components connected to it: – on PCBA - network adapter and sound controller  placed in tester’s hardware - the USB flash disk. The second step is to control DC Electronic Load and perform load test to verify power limiter  functionality.

The app is creating new log file every time it  is run.

In config file you can change the VID and name of discovering device.
If changing for example flashdisk inside 10034463 PE_MB TESTER, firstly check its VID and name in log file and then change its value in config file.

Remember to copy the ThreeShapeKtEL30000.exe to the release folder.

If you want to change electronic load device, you have to change its VISA adress in the config. 
You can also change described below test parameters in log file:
			"ElMinTestLimit" value="1.18"		-	current limit (min value)	
			"ElMaxTestLimit" value="1.24"		-	current limit (max value)
			"ElStartCurrent" value="1.17"		-	current value, from which the test will be started
			"ElMaxCurrent" value="1.245"		-	current value, at which the test will be stopped
			"ElCurrentIncreasement" value="0.001"	-	current increasement value
			"ElCheckVoltageAfterTest" value="False"	-	(optional) check if voltage returns just after turning off the input of EL


