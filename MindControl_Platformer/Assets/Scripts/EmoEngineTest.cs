using UnityEngine;
using System.Collections;

public class EmoEngineTest : MonoBehaviour
{
	// Use this for initialization
	void Start () 
    {
        //enable MentalCommand action
        EmoMentalCommand.EnableMentalCommandAction(EdkDll.IEE_MentalCommandAction_t.MC_LIFT, true);
        EmoMentalCommand.EnableMentalCommandAction(EdkDll.IEE_MentalCommandAction_t.MC_PUSH, true);
        EmoMentalCommand.EnableMentalCommandAction(EdkDll.IEE_MentalCommandAction_t.MC_PULL, true);
        EmoMentalCommand.EnableMentalCommandActionsList();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyUp(KeyCode.P)) { EmoMentalCommand.StartTrainingMentalCommand(EdkDll.IEE_MentalCommandAction_t.MC_NEUTRAL); }
        if (Input.GetKeyUp(KeyCode.O)) { EmoMentalCommand.StartTrainingMentalCommand(EdkDll.IEE_MentalCommandAction_t.MC_LIFT); }
        if (Input.GetKeyUp(KeyCode.I)) { EmoMentalCommand.StartTrainingMentalCommand(EdkDll.IEE_MentalCommandAction_t.MC_PUSH); }
        if (Input.GetKeyUp(KeyCode.U)) { EmoMentalCommand.StartTrainingMentalCommand(EdkDll.IEE_MentalCommandAction_t.MC_PULL); }
             
	}  
	
}

