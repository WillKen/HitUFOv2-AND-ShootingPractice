using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class UserInterface : MonoBehaviour
{
	public director _director;
    public bool start=true;
    void Start()
	{
		_director = director.getInstance();

    }
    
	private void OnGUI()
	{
        if (start)
        {
            if (GUI.Button(new Rect(0.5f * Screen.width-75, 0.5f * Screen.height - 55, 150, 50), "运动学运动"))
            {
                start = false;
                _director.currentController.factory.physics = false;
                _director.currentController.factory.enabled = true;
            }
            if (GUI.Button(new Rect(0.5f * Screen.width - 75, 0.5f * Screen.height+5, 150, 50), "物理运动"))
            {
                start = false;
                _director.currentController.factory.physics = true;
                _director.currentController.factory.enabled = true;
            }
        }
        
        
        int my_round = _director.currentController.factory.round;
		if (my_round >10)
		{
			GUIStyle style1 = new GUIStyle();
			style1.normal.background = null;
			style1.normal.background = null;
			style1.normal.textColor = Color.red;
			style1.fontSize = 60;
			string ending_score = "Final Score: " + _director.currentController.factory.score.ToString();
			GUI.Label(new Rect(Screen.width*0.5f-150, Screen.width*0.5f-150, 300, 300), ending_score,style1);
            if (GUI.Button(new Rect(0.7f * Screen.width, 0.7f * Screen.height, 150, 35), "重新开始"))
            {
				EditorSceneManager.LoadScene (0);
            }
        }
		else
		{
            string round = my_round.ToString();
			round = "Round: " + round;
			GUIStyle style2 = new GUIStyle();
			style2.normal.background = null;
			style2.normal.textColor = Color.cyan;
			style2.fontSize = 25;
			GUI.Label(new Rect(5, 10, 150, 35), round, style2);
			string score = _director.currentController.factory.score.ToString();
			score = "Score:" + score;
			GUI.Label(new Rect(5, 40, 150, 35), score, style2);
			GUIStyle style3 = new GUIStyle();
			style3.normal.background = null;
			style3.normal.textColor = Color.cyan;
			style3.fontSize = 20;
			GUI.Label(new Rect(5, 70, 150, 35), "White UFO for 1 point(s)", style3);
			GUI.Label(new Rect(5, 95, 150, 35), "Gray  UFO for 2 point(s)", style3);
			GUI.Label(new Rect(5, 120, 150, 35), "Black UFO for 3 point(s)", style3);

		}

    }
}
