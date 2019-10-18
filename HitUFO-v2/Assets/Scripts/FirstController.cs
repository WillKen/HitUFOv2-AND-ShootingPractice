using UnityEngine;

public class FirstController : MonoBehaviour
{
	public UFOfactory factory;
	public director director;
	private GameObject myUFOfactory;
	public GameObject _camera;

	void Awake()
	{
		Random.InitState((int)System.DateTime.Now.Ticks);
		myUFOfactory = new GameObject("UFOfactory");
		myUFOfactory.AddComponent<UFOfactory>();
		director = director.getInstance();
		factory = Singleton<UFOfactory>.Instance;
        factory.enabled = false;
		director.currentController = this;
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{

			Vector3 mp = Input.mousePosition;
			Camera c;
			if (_camera != null) c = _camera.GetComponent<Camera>();
			else c = Camera.main;
			Ray ray = c.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				director.currentController.factory.hitted(hit.transform.gameObject);
			}
		}
	}

}
