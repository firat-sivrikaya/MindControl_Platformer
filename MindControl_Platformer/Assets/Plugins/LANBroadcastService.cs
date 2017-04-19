
// If you want to use this script, but want players to choose between creating/joining a server for themselves:
// When the player chooses to create a server, call the StartAnnounceBroadcasting()
// When the player chooses to join, call StartSearchBroadcasting(), but strip the part of sending out messages (see Update() method)

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

public class LANBroadcastService : MonoBehaviour
{
	public int portUDP = 30303;
	public  static String[] arrHostIP;
    public enum enuState { NotActive, Searching, Announcing }; // Definition of State Enumeration.
    private struct ReceivedMessage { public float fTime; public string strIP;} // Definition of a Received Message struct. This is the form in which we will store messages
    
    private string strMessage = ""; // A simple message string, that can be read by other objects (eg. NetworkController), to show what this object is doing.
    public enuState currentState = enuState.NotActive;
    private UdpClient objUDPClient; // The UDPClient we will use to send and receive messages
    private List<ReceivedMessage> lstReceivedMessages; // The list we store all received messages in, when searching
    public List<String> lstHostIP;
    
    private float fTimeLastMessageSent;
    public float fIntervalMessageSending = 1f; // The interval in seconds between the sending of messages
    public float fTimeMessagesLive = 3; // The time a message 'lives' in our list, before it gets deleted
    public float fTimeToSearch = 5; // The time the script will search, before deciding what to do
    public float fTimeSearchStarted;

    public string Message { get { return strMessage; } } // Property to read the strMessage

    void Start()
    {
        // Create our list
        lstReceivedMessages = new List<ReceivedMessage>();
        lstHostIP = new List<String>();
       
		
		Debug.Log(Network.player.ipAddress);
    }

    void Update()
    {
        // Check if we need to send messages and the interval has espired
		
        if ( currentState == enuState.Announcing && 
		(Time.time > fTimeLastMessageSent + fIntervalMessageSending))
        {
            // Determine out of our current state what the content of the message will be
            byte[] objByteMessageToSend = System.Text.Encoding.ASCII.GetBytes("s");
            // Send out the message
            //objUDPClient.Send(objByteMessageToSend, objByteMessageToSend.Length);
            objUDPClient.Send(objByteMessageToSend, objByteMessageToSend.Length,new IPEndPoint(IPAddress.Parse("192.168.1.255"),portUDP));
            // Restart the timer
            fTimeLastMessageSent = Time.time;
		}
            // Refresh the list of received messages (remove old messages)
        if (currentState == enuState.Searching&& 
		(Time.time > fTimeLastMessageSent + fIntervalMessageSending))
        {
                // This rather complex piece of code is needed to be able to loop through a list while deleting members of that same list
                
				Debug.Log(lstReceivedMessages.Count + " server");
				fTimeLastMessageSent = Time.time;
				bool bLoopedAll = false;
                while (!bLoopedAll && lstReceivedMessages.Count > 0)
                {
                    foreach (ReceivedMessage objMessage in lstReceivedMessages)
                    {
                        if (Time.time > objMessage.fTime + fTimeMessagesLive)
                        {
                            // If this message is too old, delete it and restart the foreach loop
                            lstReceivedMessages.Remove(objMessage);
                            lstHostIP.Remove(objMessage.strIP);
                            break;
                        }
                        // If this whas the last message, make sure we exit the while loop
                        if (lstReceivedMessages[lstReceivedMessages.Count - 1].Equals(objMessage))
						{
							bLoopedAll = true;
						}
                    }
					
                }
        }
		
		//if(Input.GetKeyUp("a"))
		//{
			//StopBroadCasting();
			//StartSearchBroadCasting();
		//}
		//
		//if(Input.GetKeyUp("b"))
		//{
			//StopBroadCasting();
			//StartAnnounceBroadCasting();
		//}
		//
		//if(Input.GetKeyUp("c"))
		//{
			//StopBroadCasting();
		//}
        
	
    }

    // Method to start an Asynchronous receive procedure. The UDPClient is told to start receiving.
    // When it received something, the UDPClient is told to call the EndAsyncReceive() method.
    private void BeginAsyncReceive()
    {
        objUDPClient.BeginReceive(new AsyncCallback(EndAsyncReceive), null);
    }
    // Callback method from the UDPClient.
    // This is called when the asynchronous receive procedure received a message
    private void EndAsyncReceive(IAsyncResult objResult)
    {
        // Create an empty EndPoint, that will be filled by the UDPClient, holding information about the sender
        IPEndPoint objSendersIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        // Read the message
        byte[] objByteMessage = objUDPClient.EndReceive(objResult, ref objSendersIPEndPoint);
        // If the received message has content and it was not sent by ourselves...
        //if ((objByteMessage.Length > 0 ) &&
        //    !objSendersIPEndPoint.Address.ToString().Equals(Network.player.ipAddress))
        if (objByteMessage.Length > 0 ) 
        {
            // Translate message to string
            Debug.Log("ok");
          
            // Create a ReceivedMessage struct to store this message in the list
           
            //if(strReceivedMessage == "s")
            String ip = objSendersIPEndPoint.Address.ToString();
            ReceivedMessage rv = new ReceivedMessage();
            rv.fTime = Time.time;
            rv.strIP = ip;
            if (!lstHostIP.Contains(ip))
            {
                lstHostIP.Add(ip);
                lstReceivedMessages.Add(rv);
               
            }
            
        }
        // Check if we're still searching and if so, restart the receive procedure
        if (currentState == enuState.Searching) BeginAsyncReceive();
    }
    // Method to start this object announcing this is a server, used by the script itself
    private void StartAnnouncing()
    {
        currentState = enuState.Announcing;
        Debug.Log( "Announcing we are a server...");
    }
    // Method to stop this object announcing this is a server, used by the script itself
    private void StopAnnouncing()
    {
        currentState = enuState.NotActive;
        Debug.Log("Announcements stopped.");
    }
    // Method to start this object searching for LAN Broadcast messages sent by players, used by the script itself
    private void StartSearching()
    {
        lstReceivedMessages.Clear();
        BeginAsyncReceive();
        fTimeSearchStarted = Time.time;
        currentState = enuState.Searching;
        Debug.Log("Searching for other players...");
    }
    // Method to stop this object searching for LAN Broadcast messages sent by players, used by the script itself
    private void StopSearching()
    {
        currentState = enuState.NotActive;
        Debug.Log("Search stopped.");
    }

    // Method to be called by some other object (eg. a NetworkController) to start a broadcast search
    // It takes two delegates; the first for when this object finds a server that can be connected to, 
    // the second for when this player is determined to start a server itself.
    public void StartSearchBroadCasting()
    {
       
        // Start a broadcasting session (this basically prepares the UDPClient)
        StartBroadcastingSession();
        // Start a search
        StartSearching();
    }
    // Method to be called by some other object (eg. a NetworkController) to start a broadcast announcement. Announcement means; tell everyone you have a server.
    public void StartAnnounceBroadCasting()
    {
        // Start a broadcasting session (this basically prepares the UDPClient)
        StartBroadcastingSession();
        // Start an announcement
        StartAnnouncing();
    }
    // Method to start a general broadcast session. It prepares the object to do broadcasting work. Used by the script itself.
    private void StartBroadcastingSession()
    {
        // If the previous broadcast session was for some reason not closed, close it now
        if (currentState != enuState.NotActive) StopBroadCasting();
        // Create the client
        //objUDPClient = new UdpClient("192.168.1.47",portUDP);
       //objUDPClient = new UdpClient(new IPEndPoint(IPAddress.Broadcast,portUDP));
        objUDPClient = new UdpClient(portUDP);
        objUDPClient.EnableBroadcast = true;
        // Reset sending timer
        fTimeLastMessageSent = Time.time;
    }
    // Method to be called by some other object (eg. a NetworkController) to stop this object doing any broadcast work and free resources.
    // Must be called before the game quits!
    public void StopBroadCasting()
    {
        if (currentState == enuState.Searching) StopSearching();
        else if (currentState == enuState.Announcing) StopAnnouncing();
        if (objUDPClient != null)
        {
            objUDPClient.Close();
            objUDPClient = null;
        }
    }

    public void GetArrayHostIP()
    {
        arrHostIP = lstHostIP.ToArray();
    }
   
	
}
