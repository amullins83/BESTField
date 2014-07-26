#BEST Field Communicator

BEST Robotics Competition tournament software module for communicating with field hardware

##Summary

 - Communicates with up to four fields simultaneously
 - Fast UDP connections
 - Parses field state data delivered in XML format
 - Returns state data in easy-to-use objects

##Install

 - Add the BESTFieldCommunicator project to your tournament software solution
 - Add a reference to this project in your main project

##Use

1. If your main project namespace is not called **BEST2014**, use it. You also need System.Net:

    using BEST2014;
    using System.Net;

2. Create your field objects using ID's 1 through 4 and the IP addresses of your field modules:

    Field field1 = new Field(1, IPAddress.Parse("192.168.1.199"));

3. Instantiate your FieldCommunicator and add your fields to it:

    FieldCommunicator fc = new FieldCommunicator();
    fc.AddField(field1);

4. Query the state of your field:

    FieldState state = fc.QueryField(1);

5. Check to see if a valid state was returned:

    if(state.IsConfigured)
    {
        // Valid data
    }
    else
    {
        // Something went wrong
    }

6. Get tie-breaker results

   int blueRank = state.Blue.Rank;
   bool greenSwitch = state.Green.IsOn;

##Testing

 - Add the TestBestCommunicator project to your solution
 - Use Nuget to install dependencies (NUnit, Should.Fluent)
 - Open the Test Explorer (TEST > Windows > Test Explorer) or hit [key:Ctrl+r],[key:a]
 - Please add new unit tests when implementing new features

##Contributors

 - Austin Mullins
 - Add your name here
