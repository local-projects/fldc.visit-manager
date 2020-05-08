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
	- [ofApp](#ofapp)
	- [Drawing](#drawing)
		- [Canvas Manager](#canvas)
		- [Stroke](#stroke)
		- [Brush::Stroke](#brush)
	- [UI](#ui)
		- [Info / Copy](#info)
		- [Hand Animation](#hand)
		- [Buttons](#button)
	- [Utils](#utils)
- [Config File](#config)

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
FLDCVisitManager.Backend:
<ul>
	<li>handels all end points for the collectoion point communication</li>
	<li>end points for takeaway tables and unity gallery- collectabile assets for lamp</li>
	<li>end points for pairing the ticketing system to lamp</li>
</ul>
FLDCVisitManager.CMSDataLayar handles all communication with the CMS - squixed CMS. 
In order to update squidex configurations and credentials see [config file](#config).
FLDCVisitManager.DataLayer handles all communication with the DB - SQL server.
In order to update connections string see [config file](#config).
