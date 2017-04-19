var screenX : int;
var screenY : int;
var a : float;
var b : float;
var angle  : float;
var target : Transform;
var player : Transform;
var camera2 : Camera;
var screenTarget:Vector3;
var preAngle: float;
var visible = false; 

function Start()
{
	screenX = Screen.width - 20;
	screenY = Screen.height - 20;
	a = 0.09*0.09;
	b = 0.06*0.06;
	Disable();
	//FindNextTarget();
}

function SetTarget(t : Transform)
{
	target = t;
	Debug.Log(t.gameObject.name);
}

function Disable()
{
	visible = false;
	gameObject.GetComponent.<Renderer>().enabled = false;
}

function FindNextTarget()
{
	visible = true;
	Debug.Log("SHIT");
	//Mui Ten Den Manh Headset gan nhat
	var gos = GameObject.FindGameObjectsWithTag("PartOfEpoc");
	var p = GameObject.Find("Player");
	var min = 0;
	var tf : Transform;
	for (var go : GameObject in gos)
	{
		if (min == 0 || min > Vector3.Distance(go.transform.position, p.transform.position))
		{
			min = Vector3.Distance(go.transform.position, p.transform.position);
			tf  = go.transform;
			Debug.Log(tf.gameObject.name);
		}
	}
	if (min != 0)
	{
		SetTarget(tf);
	} else Disable();
	//////////////////////////////////
}


function FixedUpdate ()
{
	if (!visible) 
		return;
		
	screenTarget = camera2.WorldToViewportPoint(target.position);
		
	var x = (screenTarget.x - 0.5);
	var y = screenTarget.y - 0.5;
	
	angle = Mathf.Atan(y/x)*Mathf.Rad2Deg;
	
	if( (x*screenTarget.z) < 0 )
		angle += 180;
	var angleRotate = angle - preAngle;
	preAngle = angle;
		
	
	var r2 = (a*b/(a* Mathf.Pow(Mathf.Tan(angle*Mathf.Deg2Rad ),2) + b))*(1 + Mathf.Pow(Mathf.Tan(angle*Mathf.Deg2Rad ),2));
	
	transform.localPosition.x = Mathf.Sqrt(r2)*Mathf.Cos(angle*Mathf.Deg2Rad);
	transform.localPosition.y = Mathf.Sqrt(r2)*Mathf.Sin(angle*Mathf.Deg2Rad);
	transform.Rotate(Vector3.up, angleRotate,Space.Self);
	
	if( Vector3.Distance(player.position,target.position) < 8 )
		gameObject.GetComponent.<Renderer>().enabled = false;
	else
		gameObject.GetComponent.<Renderer>().enabled = true;
	
}


