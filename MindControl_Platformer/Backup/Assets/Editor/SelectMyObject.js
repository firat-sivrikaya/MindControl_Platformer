
@MenuItem ("Tools/Select My Object")
static function SelectMyObject() {
    var obj = GameObject.Find("Game_Manager");
    EditorGUIUtility.PingObject(obj);
    Selection.activeGameObject = obj;
}


