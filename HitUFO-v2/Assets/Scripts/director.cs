using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class director : System.Object
{
	private static director _instance;
	public FirstController currentController { get; set; }
	public static director getInstance()
	{
		if (_instance == null)
		{
			_instance = new director();
		}
		return _instance;
	}
}
