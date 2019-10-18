using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace actions_Adapters
{
    public interface UFO_action
    {
        void SetSpeed(int speed);
        void Start();
        void Running(bool run);
        GameObject getUFO();
        void setUFO(GameObject ufo);
        void Update();
    }

    //Actions（HitUFO -v1）
    public class actions : ScriptableObject, UFO_action
    {
        public director director;
        public GameObject ufo;
        Vector3 start;
        Vector3 end;
        public int speed = 5;
        public bool running = true;
        public int recordType;
        
		public void SetSpeed(int speed)
        {
            this.speed = speed;
        }
        public void Running(bool run)
        {
            this.running = run;
        }
        public GameObject getUFO()
        {
            return this.ufo;
        }
        public void setUFO(GameObject ufo)
        {
            this.ufo = ufo;
        }

        public void Start()
        {
            director = director.getInstance();
            start = new Vector3(Random.Range(-6, 6), Random.Range(-6, 6), 0);
            if (start.x < 10 && start.x > -10)
                start.x *= 5;
            if (start.y < 10 && start.y > -10)
                start.y *= 5;
            end = new Vector3(-start.x, -start.y, 0);
            ufo.transform.position = start;
            foreach (Transform child in ufo.transform)
            {
                child.gameObject.GetComponent<MeshRenderer>().material = Material.Instantiate(Resources.Load("Prefabs/body", typeof(Material))) as Material;
            }
            int typeOfUFO = Random.Range(1, 4);
            recordType = typeOfUFO;
            switch (typeOfUFO)
            {
                case 1:
                    //ufo.tag = "easy";
                    //ufo.GetComponent<MeshRenderer>().material =Material.Instantiate(Resources.Load("Prefabs/easy", typeof(Material))) as Material;
                    ufo.GetComponent<MeshRenderer>().material.color = Color.white;
                    break;
                case 2:
                    //ufo.tag = "middle";
                    //ufo.GetComponent<MeshRenderer>().material = Material.Instantiate(Resources.Load("Prefabs/middle", typeof(Material))) as Material;
                    ufo.GetComponent<MeshRenderer>().material.color = Color.gray;
                    break;
                case 3:
                    //ufo.tag="tough";
                    //ufo.GetComponent<MeshRenderer>().material = Material.Instantiate(Resources.Load("Prefabs/tough", typeof(Material))) as Material;
                    ufo.GetComponent<MeshRenderer>().material.color = Color.black;
                    break;
                default:
                    break;
            }
        }

        public void Update()
        {
            if (running)
            {
                ufo.transform.position = Vector3.MoveTowards(ufo.transform.position, end, speed * Time.deltaTime);
                if (ufo.transform.position == end)
                {
                    this.director.currentController.factory.miss(this.ufo);
                }
            }
        }

    }


    //physics actions
    public class physics_actions : ScriptableObject, UFO_action
    {
        public director director;
        public GameObject ufo;
        Vector3 start;
        Vector3 end;
        public int speed = 5;
        public bool running = true;
        int framecount = 0;
        public int recordType;

		public void SetSpeed(int speed)
		{
			this.speed = speed;
		}
		public void Running(bool run)
		{
			this.running = run;
		}
		public GameObject getUFO()
		{
			return this.ufo;
		}
		public void setUFO(GameObject ufo)
		{
			this.ufo = ufo;
		}

        public void Start()
        {
            //Debug.Log("phy");
            director = director.getInstance();
            if (ufo.GetComponent<Rigidbody>() == null)
                ufo.AddComponent<Rigidbody>();
            start = new Vector3(Random.Range(-6, 6), Random.Range(-6, 6), 0);
            if (start.x < 10 && start.x > -10)
                start.x *= 5;
            if (start.y < 10 && start.y > -10)
                start.y *= 5;
			end = new Vector3(-start.x, -start.y, 0);
            ufo.transform.position = start;
            foreach (Transform child in ufo.transform)
            {
                child.gameObject.GetComponent<MeshRenderer>().material = Material.Instantiate(Resources.Load("Prefabs/tough", typeof(Material))) as Material;
            }
            int typeOfUFO = Random.Range(1, 4);
            recordType = typeOfUFO;
            switch (typeOfUFO)
            {
                case 1:
                    //ufo.tag = "easy";
                    //ufo.GetComponent<MeshRenderer>().material =Material.Instantiate(Resources.Load("Prefabs/easy", typeof(Material))) as Material;
                    ufo.GetComponent<MeshRenderer>().material.color = Color.white;
                    break;
                case 2:
                    //ufo.tag = "middle";
                    //ufo.GetComponent<MeshRenderer>().material = Material.Instantiate(Resources.Load("Prefabs/middle", typeof(Material))) as Material;
                    ufo.GetComponent<MeshRenderer>().material.color = Color.gray;
                    break;
                case 3:
                    //ufo.tag="tough";
                    //ufo.GetComponent<MeshRenderer>().material = Material.Instantiate(Resources.Load("Prefabs/tough", typeof(Material))) as Material;
                    ufo.GetComponent<MeshRenderer>().material.color = Color.black;
                    break;
                default:
                    break;
            }
            Rigidbody rigit = ufo.GetComponent<Rigidbody>();
            rigit.velocity = (end - start) * speed * Random.Range(0.001f, 0.06f);
			rigit.useGravity = false;
        }

        public void Update()
        {
            framecount++;
            if (framecount > 1000)
                this.director.currentController.factory.miss(this.ufo);

            Rigidbody rigit = ufo.GetComponent<Rigidbody>();
            if (running == false)
            {
                rigit.velocity = Vector3.zero;
                framecount = 0;
            }
            if (ufo.transform.position.x < -100 || ufo.transform.position.x > 100 || ufo.transform.position.x < -100 || ufo.transform.position.x > 100 || ufo.transform.position.x < -100 || ufo.transform.position.x > 100)
			{
                rigit.velocity = Vector3.zero;
                this.director.currentController.factory.miss(this.ufo);
            }
        }
    }
}
