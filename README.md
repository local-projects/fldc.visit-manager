# FLDC Admin website and server
***
<a name ="intro"></a>
# Intro
This applicaion contains a react front end application for the admin website and a .net core 3.1 web api backend for handeling storing visitor data, communication between collection points and lamps and an end point for Unity gallery and Takeaway tables.
Front end react applications for FLDC admins includes:
<ul>
  <li>1. Web page for navigating between all web resources avilable for the admin</li>
  <li>2. Camera\Lamp usage page</li>
  <li>3. Lamp status page that handles Lamp battery usage and lamp firmware</li>
  <li>3. HOPE CONTRIBUTION approval page</li>
  <li>4. Users\Groups page </li>
  </ul>
  
- [Intro](#intro)
	- [Table of Contents](#toc)
	- [Specs](#specs)
- [Structure](#structure)
	- [FLDCVisitManager.Backend](#FLDCVisitManager.Backend)
	- [FLDCVisitManager.CMSDataLayar](#FLDCVisitManager.CMSDataLayar)
	- [FLDCVisitManager.DataLayer](#FLDCVisitManager.DataLayer)
- [Config File](#config)
- [Build steps](#build)

<a name="specs"></a>
# specs

react web application for the frontend
.net core web api with tcp connections for the backend
Hosted on IIS server

.net add ons :

        AutoMapper
        MessagePack vr 1.9.3
        SignalR.Protocols.MessagePack
        SignalR
        
npm add ons:

        @microsoft/signalr
        @microsoft/signalr-protocol-msgpack

<a name="structure"></a>
# structure

The App is divided to 4 small projects.
<a name="FLDCVisitManager.Backend"></a>
FLDCVisitManager.Backend:
<ul>
	<li>handels all end points for the collectoion point communication</li>
	<li>end points for takeaway tables and unity gallery- collectabile assets for lamp</li>
	<li>end points for pairing the ticketing system to lamp</li>
</ul>
<a name="FLDCVisitManager.CMSDataLayar"></a>
FLDCVisitManager.CMSDataLayar handles all communication with the CMS - squixed CMS. 
In order to update squidex configurations and credentials see [config file](#config).
<a name="FLDCVisitManager.DataLayer"></a>
FLDCVisitManager.DataLayer handles all communication with the DB - SQL server.
In order to update connections string see [config file](#config).

<a name="config"></a>
## Config File

- **File type :** json
- **Location :** ~\FLDCVisitManager.Backend\appsettings.json
- **Function :** Configures all third parties and their relating credentials. (squidex and SQL DB)

<a name="build"></a>
## Build steps
<ul>
	<li>Make sure .net core framework 3.1</li>
	<li>Restore nuget packages</li>
	<li>Open cmd: <br/>
		1. cd FLDCVisitManager.Backend\backendclient <br/>
		2. npm i <br/>
	</li>
	<li>Run project</li>
</ul>
