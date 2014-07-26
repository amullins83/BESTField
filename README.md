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

2. Use the `FieldCommunicatorFactory` to create the communicator from IPAddresses:

    var factory = new FieldCommunicatorFactory();
    var communicator =
        factory.CreateFromIPAddresses(IPAddress.Parse("192.168.1.1"));

You can pass in multiple addresses. Only the first 4 will be used, as the Field
Control software only allows 4 devices to be on the same network.

    var communicator =
        factory.CreateFromIPAddresses(address1, address2, address3, address4);

##Testing

 - Add the TestBestCommunicator project to your solution
 - Use Nuget to install dependencies (NUnit, Should.Fluent)
 - Open the Test Explorer (TEST > Windows > Test Explorer) or hit [key:Ctrl+r],[key:a]
 - Please add new unit tests when implementing new features

##Contributors

 - Austin Mullins
 - Add your name here
