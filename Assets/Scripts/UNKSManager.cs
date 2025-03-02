using UnityEngine;

public class UNKSManager : MonoBehaviour
{
    [SerializeField] Transform satellite;
    [SerializeField] float satellite_speed = 0.1f;
    [SerializeField] Transform radar;
    [SerializeField] LayerMask axis_layer;

    private float dx, radar_status = 2, radar_pos;

    private Quaternion rot = Quaternion.identity;
    private float radar_speed;

    void Update()
    {   
        satellite.position = new Vector3(Mathf.Sin(Time.time * satellite_speed) * 4, satellite.position.y, satellite.position.z);

        radar.localRotation = Quaternion.Slerp(radar.localRotation, rot, Time.deltaTime * radar_speed);

        if(Quaternion.Angle(radar.localRotation, Quaternion.Euler(0, 40, 0)) < 0.5f)
            radar_pos = 4;

        if(Quaternion.Angle(radar.localRotation, Quaternion.Euler(0, -40, 0)) < 0.5f)
            radar_pos = 3;

        if(Physics.Raycast(radar.position, radar.forward, out RaycastHit hit, axis_layer))
            dx = Vector3.Distance(hit.point + Vector3.forward * 0.5f, satellite.position);
    }

    public float[] MoveStop()
    {
        radar_speed = 0;
        radar_status = 2;

        return new float[4] {dx, radar_status, radar_pos, Time.time};
    }

    public float[] MoveLeft(float speed)
    {
        rot = Quaternion.Euler(0, -40, 0);
        radar_speed = speed;

        radar_pos = 1;
        radar_status = 3;

        return new float[4] {dx, radar_status, radar_pos, Time.time};
    }

    public float[] MoveRight(float speed)
    {
        rot = Quaternion.Euler(0, 40, 0);
        radar_speed = speed;

        radar_pos = 2;
        radar_status = 3;

        return new float[4] {dx, radar_status, radar_pos, Time.time};
    }

    public float[] GetStatus()
    {
        return new float[4] {dx, radar_status, radar_pos, Time.time};
    }
}
