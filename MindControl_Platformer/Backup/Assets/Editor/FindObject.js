
class FindObject extends EditorWindow
{
	static var ObjectName:String = "";
   // add menu item
   @MenuItem ("Tools/FindObject")
   
   static function Init ()
   {
      // Get existing open window or if none, make a new one:
      var window : FindObject = EditorWindow.GetWindow(FindObject);
      window.Show ();
   }
   
   function OnGUI ()
   {
      GUILayout.BeginHorizontal();
      ObjectName = EditorGUI.TextField(Rect(10,20,150,20),ObjectName);
		
      if(GUI.Button(Rect(170,20,40,20),">>>"))
      {
		 if(ObjectName != "")
			SelectObject(ObjectName);
      }
      
    
      GUILayout.EndHorizontal();
   }
   
   
   
   
   static function SelectObject(name:String) 
   {
		var obj = GameObject.Find(name);
		if(obj)
		{
			EditorGUIUtility.PingObject(obj);
			Selection.activeGameObject = obj;
		}
	}

} 