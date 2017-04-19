
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EmoTrainingPanel : MonoBehaviour
{


    public GUIStyle black_text;
    public GameObject EpocManager;
    public GameObject GUIControl;

    // id of the window in focus
    public int focusWindow;

    // var for enable label information
    //  if true : draw a label information : OnDrawLabelInformation(String message);
    //      call SendMessage on your script
    public bool test_enable;
    bool enable_LabelInformation;
    public String str_Information;
    private float time_count;
    private float timeCount = 0.0f;
    private float maxTime = 10.0f;
    bool isShowProgessBar = false;

    // Skin if need
    //		Skin1 for normal button + other elements
    //		Skin2 for dropdown
    public GUISkin gui_Skin1;
    public GUISkin gui_Skin2;

    // GridButton (for 5 tap)
    int int_GridButton;
    String[] arrayStr_GridButton;

    ///
    /// values for User tap
    private bool onChooseEpoc;
    private String epocCurrent = "None";
    private String[] epocArray;
    ///

    ///
    /// Values for Cognitiv
    String[] array_CogActionList;

    // Cognitiv Action window
    private String[] str_ActionChoose;

    private int choosedActionButton = 0;


    //

    // string Profile
    public String str_Profile;
    // array String List Profile
    public ArrayList arrayStr_Profile;

    private Vector2 vec2_Scoll;
    //
    private bool onListProfile = false;
    private int choosingProfileID;
    private bool onCreateProfile = false;
    private bool isLogin = false, isUploadProfile = false, isLoadProfile = false;

    private String newProfile = "";
    ///
    ///


    //GyroData Window
    public Texture2D imgRoundGyro1;
    public Texture2D imgRoundGyro2;
    public Texture2D imgHeadPosition;
    public int posX = 575;
    public int posY = 255;
    public int rad = 180;

    public float timeShowAccept = 3;
    public String currentStatus;
    public GUISkin gyroSkin;

    // Use this for initialization
    void Start()
    {
        epocArray = new String[4];
        epocArray[0] = "Headset 0";
        epocArray[1] = "Headset 1";
        epocArray[2] = "Headset 2";
        epocArray[3] = "Headset 3";

        ///
        ///
        //str_Profile = "Profile";
        str_Profile = "Profile";
        arrayStr_Profile = new ArrayList();
        // load list profile here
        /*
         * arrayStr_Profile = new String[30];
         * or use other values : just replace arrayStr_Profile by your var
         */
        LoadListProfile();  // load list profile on this function



        /// 5 tap of GridButton
        arrayStr_GridButton = new String[4];
        arrayStr_GridButton[0] = "User";
        arrayStr_GridButton[1] = "Facial Expression";
        arrayStr_GridButton[2] = "Mental Command";
        arrayStr_GridButton[3] = "GyroData";

        str_ActionChoose = new String[3];
        str_ActionChoose[0] = "Choose Action";
        str_ActionChoose[1] = "Choose Action";
        str_ActionChoose[2] = "Choose Action";

        /// define array CogActionList
        ///		14 for 14 Actions, the Neutral is "None"
        array_CogActionList = new String[14];
        array_CogActionList[0] = "None";
        array_CogActionList[1] = "Push";
        array_CogActionList[2] = "Pull";
        array_CogActionList[3] = "Lift";
        array_CogActionList[4] = "Drop";
        array_CogActionList[5] = "Left";
        array_CogActionList[6] = "Right";
        array_CogActionList[7] = "Rotate Left";
        array_CogActionList[8] = "Rotate Right";
        array_CogActionList[9] = "Rotate Clockwise";
        array_CogActionList[10] = "Rotate Counter Clockwise";
        array_CogActionList[11] = "Rotate Forwards";
        array_CogActionList[12] = "Rotate Reverse";
        array_CogActionList[13] = "Disappear";
    }


    void OnGUI()
    {
        showStatusBar();
        // draw label information
        if (enable_LabelInformation)
        {
            GUI.Label(new Rect(Screen.width - str_Information.Length * 6, Screen.height - 20, Screen.width, Screen.height), str_Information);
            if ((Time.time - time_count) > 5)
            {
                enable_LabelInformation = false;
            }
        }


        // first, Draw GridButton
        if (focusWindow == 100 || focusWindow == 101 || str_Profile == "Profile")
        {
            GUI.SelectionGrid(new Rect(50, 100, 700, 20), int_GridButton, arrayStr_GridButton, 4);
        }
        else
        {
            int_GridButton = GUI.SelectionGrid(new Rect(50, 100, 700, 20), int_GridButton, arrayStr_GridButton, 4);
        }

        //if (int_GridButton != 0)
        //{
        //    GUIControl.GetComponent("headsetGUI").active = false;
        //}
        //else GUIControl.GetComponent("headsetGUI").active = true;

        GameObject face = GameObject.Find("Face");
        GameObject camGo = GameObject.Find("Camera");
        Camera cam = (Camera)camGo.GetComponent(typeof(Camera));

        switch (int_GridButton)
        {
            case 0:
                face.SendMessage("Hide");
                cam.enabled = false;
                // on User tap
                GUI.Window(0, new Rect(50, 120, Screen.width - 100, Screen.height - 200), OnWindowUser, "", gui_Skin1.window);
                GUI.BringWindowToBack(0);
                GUI.Window(1, new Rect(100, 170, 200, 200), OnInsideWindowUser, "", gui_Skin1.window);
                break;
            case 1:
                face.SendMessage("Show");
                cam.enabled = false;
                // Expressiv tap
                GUI.Window(10, new Rect(50, 120, Screen.width - 100, Screen.height - 200), OnWindowExpressiv, "", gui_Skin1.window);
                GUI.BringWindowToBack(10);
                GUI.Window(11, new Rect(100, 170, 200, Screen.height - 300), OnWindowExpressivValues, "", gui_Skin1.window);
                GUI.Window(12, new Rect(370, 170, 200, Screen.height - 300), OnWindowExpressivFloatValues, "", gui_Skin1.window);

                break;
            case 2:
                face.SendMessage("Hide");
                cam.enabled = true;
                // Cognitiv
                //
                // we'll draw 3 windows
                //  1/ profile
                //      one button (button text is profile's name)
                //      click -> a popup show list of profiles
                //              on this popUp : button OK, button Cancel, Add, Save to file, Load from file
                //              when Add clicked, show a popUp to create new profile
                //  2/ Cognitiv Action
                //      4 level training = 4 buttons choose
                //      the first button is "Neutral", and user can't select it
                //      the 3 other default are "None" or "Choose Action"
                //      the third button can't be selected if the second is "none" and so on
                //          beside, there are "Train" and "Clear" Buttons
                //          show the value if need
                //  3/ the Cube behavior

                // first Draw base Window of Cognitiv
                GUI.Window(30, new Rect(50, 120, Screen.width - 100, Screen.height - 200), OnWindowCognitiv, "", gui_Skin1.window);
                GUI.BringWindowToBack(30);

                // draw window profile
                GUI.Window(31, new Rect(70, 140, 300, 40), OnWindowCognitivGroupProfile, "", gui_Skin1.window);

                // draw window Cognitiv Action
                GUI.Window(32, new Rect(70, 200, 300, 300), OnWindowCognitivGroupAction, "", gui_Skin1.window);


                // draw window Cube
                //GUI.Window(33, new Rect(400, 140, Screen.width - 480, Screen.height - 240), OnWindowCognitivGroupCube, "", gui_Skin1.window);



                break;
            case 3:
                face.SendMessage("Hide");
                cam.enabled = false;
                // GyroData
                GUI.Window(40, new Rect(50, 120, Screen.width - 100, Screen.height - 200), OnWindowGyroData, "", gui_Skin1.window);
                GUI.BringWindowToBack(40);

                GUI.Window(41, new Rect(100, 170, 200, Screen.height - 300), OnWindowGyroDataValues, "", gui_Skin1.window);


                break;
        }

        if (onListProfile)
        {
            ////choosingProfileID = 0;
            //focusWindow = 100;
            //GUI.Window(100, new Rect(350, 140, 250, 300), OnWindowCognitivListProfile, "", gui_Skin1.window);

            //GUI.BringWindowToFront(100);
            //GUI.FocusWindow(100);


        }
        if (onCreateProfile)
        {
            //newProfile = "";
            focusWindow = 101;
            GUI.Window(101, new Rect(350, 200, 200, 100), OnWindowCognitivCreateProfile, "", gui_Skin1.window);
            GUI.BringWindowToFront(101);
            GUI.FocusWindow(101);
        }

        if (isLogin)
        {
            focusWindow = 200;
            GUI.Window(200, new Rect(350, 200, 200, 200), loginWindow, "Login", gui_Skin1.window);
            GUI.BringWindowToFront(200);
            GUI.FocusWindow(200);
        }

        if (isUploadProfile)
        {
            focusWindow = 201;
            GUI.Window(201, new Rect(350, 200, 220, 400), UploadProfileWindow, "Login", gui_Skin1.window);
            GUI.BringWindowToFront(201);
            GUI.FocusWindow(201);
        }
        if (isLoadProfile)
        {
            focusWindow = 202;
            GUI.Window(202, new Rect(350, 200, 220, 400), LoadProfileWindow, "Load Profile", gui_Skin1.window);
            GUI.BringWindowToFront(202);
            GUI.FocusWindow(202);
        }

        //if(focusWindow != 100 && focusWindow != 101 )
        //{
        //    GUI.UnfocusWindow();
        //}
    }


    // Update is called once per frame
    void Update()
    {
        if (test_enable)
        {
            OnDrawLabelInformation("Thong tin hien len nam o day ne :D");
            test_enable = false;
        }

        if (EmoGyroData.headPosition != Status.Center)
        {
            timeShowAccept = 3;
            currentStatus = EmoGyroData.headPosition.ToString();
        }

        if ((Time.time - timeCount) > maxTime)
        {
            isShowProgessBar = false;
            focusWindow = 0;
        }

        posX = 575 + EmoGyroData.GyroX;
        posY = 255 + EmoGyroData.GyroY;


    }

    /// <summary>
    /// OnWindowUser
    /// on User tap : int_GridButton = 0
    /// </summary>
    void OnWindowUser(int ID)
    {
        if (focusWindow == 100 || focusWindow == 101) GUI.enabled = false;
        //return 0;
        if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 200, 100, 30), "Exit"))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Expressiv tap : int_GridButton = 1
    /// </summary>
    void OnWindowExpressiv(int ID)
    {
    }

    void showStatusBar()
    {
        if (GUI.Button(new Rect(Screen.width - 110, 10, 100, 30), "Exit"))
        {
            Application.Quit();
        }
        if (userCloudId == -1)
        {
            if (GUI.Button(new Rect(Screen.width - 220, 10, 100, 30), "Sign Up"))
            {
                Application.OpenURL("https://id.emotivcloud.com/eoidc/account/registration/");
            }
            if (GUI.Button(new Rect(Screen.width - 330, 10, 100, 30), "Sign In"))
            {
                isLogin = true;
            }
        }
        else
        {
            GUI.Label(new Rect(Screen.width - 440, 10, 100, 30), "Hi " + username, black_text);
            if (GUI.Button(new Rect(Screen.width - 330, 10, 100, 30), "Load Profile"))
            {
                isLoadProfile = true;
            }
            if (GUI.Button(new Rect(Screen.width - 220, 10, 100, 30), "Upload Data"))
            {
                isUploadProfile = true;
            }

        }
    }

    /// <summary>
    /// Cognitiv tap : int_GridButton = 3
    /// </summary>
    void OnWindowCognitiv(int ID)
    {
        if (focusWindow == 100 || focusWindow == 101) GUI.enabled = false;

        if (isShowProgessBar)
        {
            GUI.Label(new Rect(Screen.width - 190, Screen.height - 320, 50, 20), ((int)(100 * (Time.time - timeCount) / maxTime)).ToString() + " %", gui_Skin1.label);
            GUI.HorizontalSlider(new Rect(410, Screen.height - 310, Screen.width - 610, 20), 100 * (Time.time - timeCount) / maxTime, 0, 100);
        }

        //if (GUI.Button(new Rect(800, 500, 100, 30), "Exit"))
        //{
        //    Application.Quit();
        //}

    }

    /// <summary>
    /// Profile choosing in Cognitiv Window
    /// </summary>
    void OnWindowCognitivGroupProfile(int ID)
    {
        if (focusWindow == 100 || focusWindow == 101) GUI.enabled = false;
        GUI.Label(new Rect(10, 10, 60, 20), "Profile", gui_Skin1.label);

        //////////////////////////////////////////////////////////////////////////
        // should have a function get current profile here
        // str_Profile = Getcurrent 
        // or must call set current profile whenever change profile
        //////////////////////////////////////////////////////////////////////////

        if (GUI.Button(new Rect(80, 10, 140, 20), str_Profile, gui_Skin1.button) && !onListProfile && !onCreateProfile)
        {
        }
    }
		
    string username = "tungdo", password = "Password";
    int nProfile = 0;
    int userCloudId = -1;
    struct PROFILE
    {
        public int id;
        public string name;
    }
    List<PROFILE> profiles;
    void loginWindow(int id)
    {

        GUI.Label(new Rect(10, 20, 100, 30), "User Name");
        username = GUI.TextField(new Rect(10, 40, 180, 30), username);
        GUI.Label(new Rect(10, 90, 100, 30), "Password");
        password = GUI.PasswordField(new Rect(10, 110, 180, 30), password, '*');
        if (GUI.Button(new Rect(10, 150, 80, 30), "Cancel"))
        {
            isLogin = false;
        }
        if (GUI.Button(new Rect(100, 150, 80, 30), "Login"))
        {
            int connect_result = EmotivCloudClient.EC_Connect();
            if (connect_result == EmotivCloudClient.EC_OK)
            {
                var login_result = EmotivCloudClient.EC_Login(username, password);
                if (login_result == EmotivCloudClient.EC_OK)
                {
                    Debug.Log("login success");
                    EmotivCloudClient.EC_GetUserDetail(ref userCloudId);
                    Debug.Log("user cloud id: " + userCloudId.ToString());
                    nProfile = EmotivCloudClient.EC_GetAllProfileName(userCloudId);
                    Debug.Log("number profiles: " + nProfile.ToString());
                    profiles = new List<PROFILE>();
                    for (var i = 0; i < nProfile; i++)
                    {
                        //Debug.Log("delete profile: ");
                        //var _id = EmotivCloudClient.EC_ProfileIDAtIndex(userCloudId, i);
                        //EmotivCloudClient.EC_DeleteUserProfile(userCloudId, _id);

                        int ii = EmotivCloudClient.EC_ProfileIDAtIndex(userCloudId, i);
						EmotivCloudClient.Plugin_EC_ProfileNameAtIndex(userCloudId, 0);
                        PROFILE t = new PROFILE();
                        t.id = EmotivCloudClient.EC_ProfileIDAtIndex(userCloudId, i);
						t.name = EmotivCloudClient.Plugin_EC_ProfileNameAtIndex(userCloudId, i);
                        profiles.Add(t);
                        Debug.Log("profile name: " + profiles[i].name + " profile id: " + profiles[i].id.ToString());
                    }
                    isLogin = false;
                }

                else
                {
                    Debug.Log("cannot login, error: " + login_result.ToString());
                }
            }
            else
            {
                Debug.Log("cannot connect to emotiv cloud, error: " + connect_result.ToString());
            }
        }
        if (userCloudId != -1) //logged in
        {
            if (GUI.Button(new Rect(10, 150, 80, 30), "Load Profile"))
            {

            }
        }
    }
    string newProfileName = "";

    void LoadProfileWindow(int wid)
    {
        GUI.Label(new Rect(10, 20, 200, 30), "Select profile");
        vec2_Scoll = GUI.BeginScrollView(new Rect(10, 50, 200, 300), vec2_Scoll, new Rect(0, 0, 200, 300));
        int i = 0;
        foreach (PROFILE p in profiles)
        {
            if (GUI.Button(new Rect(0, 10 + i * 35, 200, 30), p.name))
            {
                EmotivCloudClient.EC_LoadUserProfile(userCloudId, EmoUserManagement.currentUser, p.id, -1);
                str_Profile = p.name;
                isLoadProfile = false;
            }
            i++;
        }
        GUI.EndScrollView();
        if(GUI.Button(new Rect(10, 350, 200, 30), "Close"))
        {
            isLoadProfile = false;
        }
    }
    void UploadProfileWindow(int wId)
    {
        GUI.Label(new Rect(10, 20, 200, 30), "Create new profile");
        newProfileName = GUI.TextField(new Rect(10, 40, 180, 30), newProfileName);
        if (GUI.Button(new Rect(10, 80, 90, 30), "Cancel"))
        {
            isUploadProfile = false;
        }
        if (GUI.Button(new Rect(120, 80, 80, 30), "Upload"))
        {
            int pid = EmotivCloudClient.EC_GetProfileId(userCloudId, newProfileName);
            if (pid >= 0)
            {
                EmotivCloudClient.EC_UpdateUserProfile(userCloudId, EmoUserManagement.currentUser, pid);
                //close upload window
                isUploadProfile = false;
            }
            else
            {
                EmotivCloudClient.EC_SaveUserProfile(userCloudId, EmoUserManagement.currentUser, newProfileName, EmotivCloudClient.profileFileType.TRAINING);
                int id = EmotivCloudClient.EC_GetProfileId(userCloudId, newProfileName);
                PROFILE t = new PROFILE();
                t.id = id;
                t.name = newProfileName;
                profiles.Add(t);
                //close upload window
                isUploadProfile = false;

            }
        }
        GUI.Label(new Rect(10, 110, 200, 30), "or select from your profiles");
        vec2_Scoll = GUI.BeginScrollView(new Rect(10, 130, 200, 300), vec2_Scoll, new Rect(0, 0, 200, 300));
        int _i = 0;
        foreach (PROFILE p in profiles)
        {

            if (GUI.Button(new Rect(0, 10 + _i * 35, 200, 30), p.name))
            {
                newProfileName = p.name;
            }
            _i++;
        }
        GUI.EndScrollView();
        

    }

    /// <summary>
    /// Create Profile
    /// </summary>
    void OnWindowCognitivCreateProfile(int ID)
    {
        /*
         * 
         */
        if (focusWindow != 101) GUI.enabled = false;

        newProfile = GUI.TextArea(new Rect(25, 20, 100, 30), newProfile);

        if (GUI.Button(new Rect(30, 60, 50, 30), "Add", gui_Skin1.button))
        {
            AddProfile();
            onCreateProfile = false;

            // here, we should call OnProfileClickOK
            //  or use the same-like to set the current profile = newProfile
            OnProfileClickOK(newProfile);
            focusWindow = 0;
        }

        if (GUI.Button(new Rect(120, 60, 50, 30), "Cancel", gui_Skin1.button))
        {
            onCreateProfile = false;
            focusWindow = 0;
        }
    }

    /// <summary>
    /// Cognitiv Group Action
    /// </summary>
    void OnWindowCognitivGroupAction(int ID)
    {
        if (focusWindow == 100 || focusWindow == 101) GUI.enabled = false;
        // draw 4 choosing main buttons
        GUI.Button(new Rect(20, 20, 100, 20), "Neutral", gui_Skin1.button);
        if (choosedActionButton == 0)
        {
            if (GUI.Button(new Rect(130, 20, 50, 20), "Train", gui_Skin1.button)) OnTrainClick(0, 0);
            if (GUI.Button(new Rect(190, 20, 50, 20), "Clear", gui_Skin1.button)) OnClearClick(0, 0);
        }

        if (GUI.Button(new Rect(20, 60, 100, 20), str_ActionChoose[0], gui_Skin1.button)) choosedActionButton = 1;
        if (choosedActionButton == 0)
        {
            if (str_ActionChoose[0] != "Choose Action")
            {
                for (int i = 0; i < 14; i++)
                {
                    if (str_ActionChoose[0] == array_CogActionList[i])
                    {
                        if (GUI.Button(new Rect(130, 60, 50, 20), "Train", gui_Skin1.button)) OnTrainClick(1, i);
                        if (GUI.Button(new Rect(190, 60, 50, 20), "Clear", gui_Skin1.button)) OnClearClick(1, i);
                        GUI.Label(new Rect(250, 60, 50, 20), EmoMentalCommand.MentalCommandActionPower[i].ToString(), gui_Skin1.label);
                        break;
                    }

                }

            }
        }

        if (str_ActionChoose[0] != "Choose Action")
        {
            if (GUI.Button(new Rect(20, 100, 100, 20), str_ActionChoose[1], gui_Skin1.button)) choosedActionButton = 2;
            if (choosedActionButton == 0)
            {
                if (str_ActionChoose[1] != "Choose Action")
                {
                    for (int j = 0; j < 14; j++)
                    {
                        if (str_ActionChoose[1] == array_CogActionList[j])
                        {
                            if (GUI.Button(new Rect(130, 100, 50, 20), "Train", gui_Skin1.button)) OnTrainClick(2, j);
                            if (GUI.Button(new Rect(190, 100, 50, 20), "Clear", gui_Skin1.button)) OnClearClick(2, j);
                            GUI.Label(new Rect(250, 100, 50, 20), EmoMentalCommand.MentalCommandActionPower[j].ToString(), gui_Skin1.label);
                            break;
                        }

                    }

                }
            }
        }

        if (str_ActionChoose[1] != "Choose Action" && str_ActionChoose[0] != "Choose Action")
        {
            if (GUI.Button(new Rect(20, 140, 100, 20), str_ActionChoose[2], gui_Skin1.button)) choosedActionButton = 3;
            if (choosedActionButton == 0)
            {
                if (str_ActionChoose[2] != "Choose Action")
                {
                    for (int k = 0; k < 14; k++)
                    {
                        if (str_ActionChoose[2] == array_CogActionList[k])
                        {
                            if (GUI.Button(new Rect(130, 140, 50, 20), "Train", gui_Skin1.button)) OnTrainClick(3, k);
                            if (GUI.Button(new Rect(190, 140, 50, 20), "Clear", gui_Skin1.button)) OnClearClick(3, k);
                            GUI.Label(new Rect(250, 140, 50, 20), EmoMentalCommand.MentalCommandActionPower[k].ToString(), gui_Skin1.label);
                            break;
                        }

                    }

                }
            }
        }

        // button Save to file
        if (GUI.Button(new Rect(20, 260, 150, 20), "Save to file", gui_Skin1.button))
        {
            OnSaveToFileClick();
            //focusWindow = 0;
        }


        // draw dropdown
        if (choosedActionButton != 0)
        {
            vec2_Scoll = GUI.BeginScrollView(new Rect(120, 20 + 40 * choosedActionButton, 125, 80), vec2_Scoll, new Rect(0, 0, 100, 280));

            for (int i = 0; i < 14; i++)
            {
                if (GUI.Button(new Rect(0, i * 20, 100, 20), array_CogActionList[i]))
                {
                    bool bl_test = true;
                    for (int j = 0; j < 3; j++)
                        if (array_CogActionList[i] == str_ActionChoose[j])
                            bl_test = false;
                    if (bl_test)
                    {
                        ChooseAction(choosedActionButton - 1, i);
                        choosedActionButton = 0;
                    }

                }
            }

            GUI.EndScrollView();
        }

        if (str_ActionChoose[0] == "Choose Action")
        {
            str_ActionChoose[1] = "Choose Action";
            str_ActionChoose[2] = "Choose Action";
        }

        if (str_ActionChoose[1] == "Choose Action")
        {
            str_ActionChoose[2] = "Choose Action";
        }
        //if(Input.GetMouseButtonUp(KeyCode.Mouse0))
        //{
        //    //if(GUI.Input.mousePosition
        //}
    }

    /// <summary>
    /// draw the 3d Cube
    /// </summary>
    void OnWindowCognitivGroupCube(int ID)
    {
        if (focusWindow == 100 || focusWindow == 101) GUI.enabled = false;

    }

    /// <summary>
    /// GyroData tap : int_GridButton = 4
    /// </summary>
    void OnWindowGyroData(int ID)
    {
        if (focusWindow == 100 || focusWindow == 101) GUI.enabled = false;
        GUI.DrawTexture(new Rect(400, 80, 380, 380), imgRoundGyro1);
        GUI.DrawTexture(new Rect(460, 140, 260, 260), imgRoundGyro2);
        GUI.DrawTexture(new Rect(posX, posY, 30, 30), imgHeadPosition);


        if (timeShowAccept > 0)
        {

            GUI.Label(new Rect(560, 250, 150, 50), currentStatus.ToUpper(), gyroSkin.label);
            timeShowAccept -= Time.deltaTime;
        }

        //if (GUI.Button(new Rect(800, 500, 100, 30), "Exit"))
        //{
        //    Application.Quit();
        //}

    }

    /// <summary>
    /// set str_Information = message and enable_LabelInformation = true
    /// after 5s, close it
    /// </summary>
    /// <param name="message"></param>
    public void OnDrawLabelInformation(String message)
    {
        str_Information = message;
        enable_LabelInformation = true;
        time_count = Time.time;
    }

    /// <summary>
    /// set the current profile to "str" profile
    /// </summary>
    /// <param name="str"></param>
    void OnProfileClickOK(String str)
    {
        //str_Profile = newProfile;
    }

    /// <summary>
    /// When the button Add profile inside list profile window is click
    /// show the create profile window
    /// </summary>
    void OnAddProfileClick()
    {

    }

    /// <summary>
    /// When button Save to file inside "list profile" window is clicked
    /// </summary>
    void OnSaveToFileClick()
    {
        EpocManager.SendMessage("SaveCurrentProfile");
        EpocManager.SendMessage("SaveProfilesToFile");
    }

    /// <summary>
    /// When button "Load from file" inside "list profile" window is clicked
    /// </summary>
    void OnLoadFromFileClick()
    {
        EpocManager.SendMessage("LoadProfilesFromFile");
        LoadListProfile();
    }

    /// <summary>
    /// Add Profile : add the newProfile to the list profile
    /// when "Add" button inside Creat Profile window is clicked
    /// </summary>
    void AddProfile()
    {
        //arrayStr_Profile.Add(newProfile);
        EpocManager.SendMessage("AddNewProfile", newProfile);
        // reload List Profile
        //reset all action off

        str_ActionChoose[0] = "Choose Action";
        LoadListProfile();
    }

    /// <summary>
    /// set action for the posAction Button = actionID
    /// </summary>
    /// <param name="possAction"></param>
    /// <param name="actionID"></param>
    void ChooseAction(int posAction, int actionID)
    {
        // set str_ActionChoose
        if (actionID != 0) str_ActionChoose[posAction] = array_CogActionList[actionID];
        else str_ActionChoose[posAction] = "Choose Action";


        // recheck enable, disable Actions
        for (int l = 1; l < 14; l++)
        {
            //EnableMentalCommandAction
            EmoMentalCommand.EnableMentalCommandAction(EmoMentalCommand.MentalCommandActionList[l], false);
        }

        for (int i = 1; i < 14; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (str_ActionChoose[j] == array_CogActionList[i])
                {
                    EmoMentalCommand.EnableMentalCommandAction(EmoMentalCommand.MentalCommandActionList[i], true);
                }
            }
        }

        EmoMentalCommand.EnableMentalCommandActionsList();
    }

    /// <summary>
    /// when the button Train of posActionButton is clicked
    /// </summary>
    /// <param name="posActionButton"></param>
    void OnTrainClick(int posActionButton, int ActionID)
    {
        focusWindow = 100;
        if (!isShowProgessBar)
        {
            timeCount = Time.time;

            isShowProgessBar = true;
        }
        //StartTrainingCognitiv
        EmoMentalCommand.StartTrainingMentalCommand(EmoMentalCommand.MentalCommandActionList[ActionID]);
        Debug.Log(ActionID);
    }

    /// <summary>
    /// when the button Clear of posActionButton is clicked
    /// </summary>
    /// <param name="posActionButton"></param>
    void OnClearClick(int posActionButton, int ActionID)
    {
        EdkDll.IEE_MentalCommandSetTrainingAction((uint)EmoUserManagement.currentUser, EmoMentalCommand.MentalCommandActionList[ActionID]);

        EdkDll.IEE_MentalCommandSetTrainingControl((uint)EmoUserManagement.currentUser, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);

    }

    /// <summary>
    /// load list profile from Etek
    /// this function called when Start and after Add Profile
    /// </summary>
    void LoadListProfile()
    {
        // load list profile into arrayStr_Profile
        if (arrayStr_Profile.Count != 0) arrayStr_Profile.Clear();        
    }

    /// <summary>
    /// draw a small window inside window tap user
    /// </summary>
    void OnInsideWindowUser(int ID)
    {
        if (focusWindow == 100 || focusWindow == 101) GUI.enabled = false;


        GUI.Label(new Rect(20, 20, 100, 20), "Current Headset", gui_Skin1.label);
        if (GUI.Button(new Rect(20, 60, 100, 20), epocCurrent, gui_Skin1.button))
        {
            onChooseEpoc = true;
        }

        if (str_Profile != "Profile" && epocCurrent != "None")
        {
            GUI.Label(new Rect(20, 90, 100, 20), "Profile", gui_Skin1.label);
            if (GUI.Button(new Rect(20, 120, 100, 20), str_Profile, gui_Skin1.button))
            {
                onListProfile = true;
            }
        }

        if (onChooseEpoc)
        {
            vec2_Scoll = GUI.BeginScrollView(new Rect(20, 80, 125, 100), vec2_Scoll, new Rect(0, 0, 100, EmoUserManagement.numUser * 20));
            for (int i = 0; i < EmoUserManagement.numUser; i++)
            {
                if (GUI.Button(new Rect(0, i * 20, 100, 20), epocArray[i], gui_Skin1.button))
                {
                    ChooseEpoc(i);
                    //onListProfile = true;
                    onChooseEpoc = false;
                }
            }
            GUI.EndScrollView();
        }
    }

    /// <summary>
    /// choose EpocI
    /// </summary>
    /// <param name="EpocID"></param>
    void ChooseEpoc(int EpocID)
    {
        epocCurrent = epocArray[EpocID];
        if (newProfile == "")
        {
            newProfile = "Local Profile";
            str_Profile = "Local Profile";
        }
        
        AddProfile();
        OnProfileClickOK(newProfile);
    }

    /// <summary>
    /// show all Experssiv Values (only bool type)
    /// </summary>
    /// <param name="ID"></param>
    void OnWindowExpressivValues(int ID)
    {
        GUI.Label(new Rect(20, 50, 100, 20), "blink", gui_Skin1.label);
        GUI.Label(new Rect(140, 50, 50, 20), EmoFacialExpression.isBlink.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 80, 100, 20), "left wink", gui_Skin1.label);
        GUI.Label(new Rect(140, 80, 50, 20), EmoFacialExpression.isLeftWink.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 110, 100, 20), "right wink", gui_Skin1.label);
        GUI.Label(new Rect(140, 110, 50, 20), EmoFacialExpression.isRightWink.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 140, 100, 20), "Eyes Open", gui_Skin1.label);
        GUI.Label(new Rect(140, 140, 50, 20), EmoFacialExpression.isEyesOpen.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 170, 100, 20), "Looking Up", gui_Skin1.label);
        GUI.Label(new Rect(140, 170, 50, 20), EmoFacialExpression.isLookingUp.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 200, 100, 20), "Looking Down", gui_Skin1.label);
        GUI.Label(new Rect(140, 200, 50, 20), EmoFacialExpression.isLookingDown.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 230, 100, 20), "Looking Left", gui_Skin1.label);
        GUI.Label(new Rect(140, 230, 50, 20), EmoFacialExpression.isLookingLeft.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 260, 100, 20), "Looking Right", gui_Skin1.label);
        GUI.Label(new Rect(140, 260, 50, 20), EmoFacialExpression.isLookingRight.ToString(), gui_Skin1.label);


        //        EmoEngine engine = EmoEngine.Instance;
        //public static Boolean isBlink = false;
        //public static Boolean isLeftWink = false;
        //public static Boolean isRightWink = false;
        //public static Boolean isEyesOpen = false;
        //public static Boolean isLookingUp = false;
        //public static Boolean isLookingDown = false;
        //public static Boolean isLookingLeft = false;
        //public static Boolean isLookingRight = false;
        //public static float eyelidStateLeft = 0.0f;
        //public static float eyelidStateRight = 0.0f;
        //public static float eyeLocationX = 0.0f;
        //public static float eyeLocationY = 0.0f;
        //public static float eyebrowExtent = 0.0f;
        //public static float smileExtent = 0.0f;
        //public static float clenchExtent = 0.0f;
        //public static float upperFacePower = 0.0f;
        //public static float lowerFacePower = 0.0f;
    }

    /// <summary>
    /// show all Experssiv float type Values
    /// </summary>
    /// <param name="ID"></param>
    void OnWindowExpressivFloatValues(int ID)
    {
        GUI.Label(new Rect(20, 50, 100, 20), "Eyelid State Left", gui_Skin1.label);
        GUI.Label(new Rect(140, 50, 50, 20), EmoFacialExpression.eyelidStateLeft.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 80, 100, 20), "Eyelid State Right", gui_Skin1.label);
        GUI.Label(new Rect(140, 80, 50, 20), EmoFacialExpression.eyelidStateRight.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 110, 100, 20), "Eyes Location X", gui_Skin1.label);
        GUI.Label(new Rect(140, 110, 50, 20), EmoFacialExpression.eyeLocationX.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 140, 100, 20), "Eyes Location Y", gui_Skin1.label);
        GUI.Label(new Rect(140, 140, 50, 20), EmoFacialExpression.eyeLocationY.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 170, 100, 20), "Eyes Brow Extent", gui_Skin1.label);
        GUI.Label(new Rect(140, 170, 50, 20), EmoFacialExpression.eyebrowExtent.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 200, 100, 20), "Smile Extent", gui_Skin1.label);
        GUI.Label(new Rect(140, 200, 50, 20), EmoFacialExpression.smileExtent.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 230, 100, 20), "Clench Extent", gui_Skin1.label);
        GUI.Label(new Rect(140, 230, 50, 20), EmoFacialExpression.clenchExtent.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 260, 100, 20), "Upper Face Power", gui_Skin1.label);
        GUI.Label(new Rect(140, 260, 50, 20), EmoFacialExpression.upperFacePower.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 290, 100, 20), "Lower Face Power", gui_Skin1.label);
        GUI.Label(new Rect(140, 290, 50, 20), EmoFacialExpression.lowerFacePower.ToString(), gui_Skin1.label);
    }

    /// <summary>
    /// show all GyroData Values
    /// </summary>
    /// <param name="ID"></param>
    void OnWindowGyroDataValues(int ID)
    {
        GUI.Label(new Rect(20, 50, 100, 20), "Gyro X", gui_Skin1.label);
        GUI.Label(new Rect(140, 50, 50, 20), EmoGyroData.GyroX.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 80, 100, 20), "Gyro Y", gui_Skin1.label);
        GUI.Label(new Rect(140, 80, 50, 20), EmoGyroData.GyroY.ToString(), gui_Skin1.label);

        GUI.Label(new Rect(20, 120, 100, 20), "Status", gui_Skin1.label);
        GUI.Label(new Rect(140, 120, 50, 20), EmoGyroData.headPosition.ToString(), gui_Skin1.label);

    }

}
