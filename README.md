VNES - VIRTUAL NETWORK EQUIPMENT SIMULATOR
==========================================
Virtual Network Equipment Simulator is a tool that allows you to simulate 
various network equipments (using IP) behaviours throught a network running 
in a single computer. This tool can be usefull in scenarious where you have 
multiple "dumb" hardware equipments in your network and you want to simulate 
the load and behaviour of a service managing them.

This tool comprises 3 independent sub-tools:
1 - Virtual Network Equipment Simulator (VNES)
2 - Virtual Network Equipment Simulator Manager (VNESM)
3 - Virtual Service Tester (VST)

VNES
----
The VNES represents an individual equipment simulator. This tool makes use
of SharpPcap which depends on WinPcap. This tool can be run as a standalone
but is specially suited to run in VNESM. The most common scenario VNESM manages
multiple instances of VNES. VNESBase is the base class that should 
be extended to implement specific network equipment. 

VNES Manager
-----
This tool is used to launch and manage instances of VNES. It also configures
VNESs on theirs startup.

VNES Service Tester
-------------------
This is a windows service which exists for demonstranting a service handling 
all VNESs. This should be the real service handling the simulated VNESs 
equipments. This kind of service is the one that is beeing performance tested.