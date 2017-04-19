using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public class EmoEngineInst : MonoBehaviour 
{
    //----------------------------------------
    EmoEngine engine = EmoEngine.Instance;
    //----------------------------------------
    public static int[] Cq;
    public static int nChan;
    public static bool IsStarted = false;
    public static int numOfGoodContacts = 0;
	private bool lock_device = false;
    //----------------------------------------
   
    void Start()
    {
        Debug.Log("emoengine started");
        if (!IsStarted)
        {
            Cq = new int[18];
			
            engine.EmoEngineConnected +=
                 new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
				 
            engine.EmoEngineDisconnected +=
                new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
				
            engine.EmoEngineEmoStateUpdated +=
                new EmoEngine.EmoEngineEmoStateUpdatedEventHandler(engine_EmoEngineEmoStateUpdated);
				
            engine.Connect(); 
            //engine.RemoteConnect("127.0.0.1",1726);
			
            IsStarted = true;
            Debug.Log(IsStarted);
        }  
    }
	
    void Stop()
    {
        engine.Disconnect();
    }
	
	static void keyHandler(ConsoleKey key)
	{}
	
    void Update()
    {
        try
        {
            engine.ProcessEvents(10);
#if UNITY_STANDALONE_OSX || UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID
			int number_insight = EdkDll.Plugin_IEE_GetInsightDeviceCount();
			int number_epoc_plus = EdkDll.Plugin_IEE_GetEpocPlusDeviceCount();
			if(number_insight + number_epoc_plus > 0) {
				if(!lock_device) {
					lock_device = true;
					if(number_insight > 0) {
						for(int i = 0; i < number_insight; i++) {
							Int32 state = -1;
							EdkDll.Plugin_IEE_GetInsightDeviceState(out state, i);
                            Debug.Log(state);
							if(state == 1) {
							EdkDll.Plugin_IEE_ConnectInsightDevice(i);
								return;
							}
						}
						EdkDll.Plugin_IEE_ConnectInsightDevice(0);
					}
					else {
						for(int i = 0; i < number_epoc_plus; i++) {
							Int32 state = -1;
							EdkDll.Plugin_IEE_GetEpocPlusDeviceState(out state, i);
							if(state == 1) {
								EdkDll.Plugin_IEE_ConnectEpocPlusDevice(i);
								return;
							}
						}
						EdkDll.Plugin_IEE_ConnectEpocPlusDevice(0);
						}
					}
				}
						else
							lock_device = false;
#endif
        }
        catch (EmoEngineException e)
        {
            Console.WriteLine("{0}", e.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("{0}", e.ToString());
        }
    }
	
    static void engine_EmoEngineEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
    {
        EmoState es = e.emoState;
        Int32 numCqChan = es.GetNumContactQualityChannels();
        EdkDll.IEE_EEG_ContactQuality_t[] cq = es.GetContactQualityFromAllChannels();
        //Debug.Log(es.GetContactQualityFromAllChannels());
        nChan = numCqChan;
        int temp = 0;
        for (Int32 i = 0; i < numCqChan; ++i)
        {
            if (cq[i] != es.GetContactQuality(i))
            {
                throw new Exception();
            }
           
            switch (cq[i])
            {
                case EdkDll.IEE_EEG_ContactQuality_t.IEEG_CQ_NO_SIGNAL:
                    Cq[i] = 0;
                    Debug.Log("no signal");
                    break;
                case EdkDll.IEE_EEG_ContactQuality_t.IEEG_CQ_VERY_BAD:
                    Cq[i] = 1;
                    Debug.Log("very bad");
                    break;
                case EdkDll.IEE_EEG_ContactQuality_t.IEEG_CQ_POOR:
                    Cq[i] = 2;
                    Debug.Log("poor");
                    break;
                case EdkDll.IEE_EEG_ContactQuality_t.IEEG_CQ_FAIR:
                    Cq[i] = 3;
                    Debug.Log("fair");
                    break;
                case EdkDll.IEE_EEG_ContactQuality_t.IEEG_CQ_GOOD:
                    temp++;
                    Cq[i] = 4;
                    Debug.Log("good");
                    break;
            }
        }
		
        numOfGoodContacts    = temp;
        Int32 chargeLevel    = 0;
        Int32 maxChargeLevel = 0;
        es.GetBatteryChargeLevel(out chargeLevel, out maxChargeLevel);

        EdkDll.IEE_SignalStrength_t signalStrength = es.GetWirelessSignalStatus();
        if (signalStrength == EdkDll.IEE_SignalStrength_t.NO_SIG)
        {
            for (Int32 i = 0; i < numCqChan; ++i)
            {
                Cq[i] = 0;
            }
        }
    }
	
    static void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
    {       
    }
	
    static void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
    {       
    }
}
