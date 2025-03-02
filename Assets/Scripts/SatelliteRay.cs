using UnityEngine;

public class RaycastReflection : MonoBehaviour
{
    [SerializeField] int refl_count = 2;

	private LineRenderer line;

	private Ray ray;
	private RaycastHit hit;
	private Vector3 ray_dir;
    private int point_count;

	void Start()
	{
		line = GetComponent<LineRenderer>();
	}

	void Update()
	{
		ray = new Ray(transform.position, transform.forward);

		Debug.DrawRay(transform.position, transform.forward * 100, Color.magenta);

		point_count = refl_count;
		line.positionCount = point_count;
		line.SetPosition(0, transform.position);

		for(int i = 0; i <= refl_count; i++)
		{
			if(Physics.Raycast(ray, out hit, 100))
			{
				if(hit.collider.name == "Hobot")
					print("hit radar!");

				if(i == 0)
					ray_dir = Vector3.Reflect(hit.point, hit.normal);
				else
					ray_dir = Vector3.Reflect(ray_dir, hit.normal);

				ray = new Ray(hit.point, ray_dir);

				Debug.DrawRay(hit.point, ray_dir * 100, Color.magenta);

				line.positionCount = ++point_count;    
				line.SetPosition(i + 1, hit.point);
			}
			else
			{
				if(i == 0)
				{
					line.positionCount = ++point_count;    
					line.SetPosition(i + 1, transform.position - Vector3.forward * 10);
				}
			}
		}
	}
}