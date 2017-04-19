using UnityEngine;
using System.Collections;

public class GUI_MentalCommand : MonoBehaviour
{
    private int curAction = 0;
    private Vector2 vec2_Scroll;
    private string[] array_CogActionList;
    private bool trainClearShow = true;

    void Start()
    {
        array_CogActionList = new string[14];
        array_CogActionList[0] = "Neutral";
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
        if (trainClearShow)
        {
            if (GUI.Button(new Rect(130 + 230, 20, 50, 20), "Train")) OnTrainClick(curAction);
            if (GUI.Button(new Rect(190 + 230, 20, 50, 20), "Clear")) OnClearClick(curAction);
        }
        if (GUI.Button(new Rect(250 + 230, 20, 80, 20), "Deactive")) OnDeactiveClick(curAction);

        vec2_Scroll = GUI.BeginScrollView(new Rect(120, 20, 225, 80), vec2_Scroll, new Rect(0, 0, 100, 280));

        for (int i = 0; i < 14; i++)
        {
            if (GUI.Button(new Rect(0, i * 20, 210, 20), array_CogActionList[i]))
            {
                curAction = i;
                EmoMentalCommand.EnableMentalCommandAction(EmoMentalCommand.MentalCommandActionList[i], true);
                EmoMentalCommand.EnableMentalCommandActionsList();
                trainClearShow = true;
            }
        }

        GUI.EndScrollView();
    }

    private void OnDeactiveClick(int ActionID)
    {
        EmoMentalCommand.EnableMentalCommandAction(EmoMentalCommand.MentalCommandActionList[ActionID], false);
        EmoMentalCommand.EnableMentalCommandActionsList();
        Debug.Log("Deactive " + array_CogActionList[ActionID]);
        trainClearShow = false;
    }

    void OnTrainClick(int ActionID)
    {
        EmoMentalCommand.StartTrainingMentalCommand(EmoMentalCommand.MentalCommandActionList[ActionID]);
    }

    void OnClearClick(int ActionID)
    {
        EdkDll.IEE_MentalCommandSetTrainingAction((uint)EmoUserManagement.currentUser, EmoMentalCommand.MentalCommandActionList[ActionID]);
        EdkDll.IEE_MentalCommandSetTrainingControl((uint)EmoUserManagement.currentUser, EdkDll.IEE_MentalCommandTrainingControl_t.MC_ERASE);
        Debug.Log("Clear " + array_CogActionList[ActionID]);
    }
}
