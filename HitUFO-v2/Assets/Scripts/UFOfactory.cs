using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using actions_Adapters;

public class UFOfactory : MonoBehaviour
{
	public List<GameObject> used;
	public List<GameObject> notUsed;
	public List<actions> actions;
    public List<physics_actions> physics_actions;
    public List<UFO_action> Act;
    public int round = 0;
	public int score = 0;
    public bool physics = true;

	private void Start()
	{
		used = new List<GameObject>();
		notUsed = new List<GameObject>();
		actions = new List<actions>();
        physics_actions = new List<physics_actions>();
        Act = new List<UFO_action>();

        for (int i = 0; i < 10; i++)
		{
			notUsed.Add(Object.Instantiate(Resources.Load("Prefabs/UFO1", typeof(GameObject)), new Vector3(0, -20, 0), Quaternion.identity, null) as GameObject);
			actions.Add(ScriptableObject.CreateInstance<actions>());
            physics_actions.Add(ScriptableObject.CreateInstance<physics_actions>());
        }
        if (physics)
        {
            for (int i = 0; i < 10; i++)
            {
                Act.Add(physics_actions[i]);
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                Act.Add(actions[i]);
            }
        }
        for (int i = 0; i < 10; i++)
		{
            Act[i].setUFO(notUsed[i]);
            Act[i].Start();
        }
	}

	public void Update(){
		for (int i = 0; i < 10; i++)
		{
			Act[i].Update();
		}
		if (notUsed.Count == 10)
		{
			round += 1;
			if (round <= 10)
				newRound(round);
		}	
	}


	public void hitted(GameObject g)
	{
		if (g.gameObject.GetComponent<MeshRenderer>().material.color==Color.white) {
			//Debug.Log ("1");
			score += 1;
		} else if (g.gameObject.GetComponent<MeshRenderer>().material.color==Color.gray) {
			//Debug.Log ("2");
			score += 2;
		} else if (g.gameObject.GetComponent<MeshRenderer>().material.color==Color.black) {
			//Debug.Log ("3");
			score += 3;
		}
		this.used.Remove(g);
		g.transform.position = new Vector3(0, -20, 0);
		for(int i = 0; i < 10; i++)
		{
			if (Act[i].getUFO() == g)
				Act [i].Running (false);
		}
		this.notUsed.Add(g);
	}
	public void miss(GameObject g)
	{
		this.used.Remove(g);
		g.transform.position = new Vector3(0, -20, 0);
		for (int i = 0; i < 10; i++)
		{
			if (Act[i].getUFO() == g)
				Act [i].Running (false);
		}
		this.notUsed.Add(g);
	}

	public void newRound(int round)
	{
		for(int i = 0; i < 10; i++)
		{
			used.Add(notUsed[0]);
			notUsed.Remove(notUsed[0]);
			Act[i].SetSpeed(round + 2);
			Act[i].Start();
			Act [i].Running (true);
		}
	}
}
