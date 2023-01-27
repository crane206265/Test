using UnityEngine;
using Unity.MLAgents;

public class RocketSetting : MonoBehaviour
{
    public GameObject RocketAgent;
    public GameObject Goal;
    public GameObject Mass1;
    public GameObject Mass2;

    Vector3 areaInitPos;
    Vector3 rocketInitPos;
    Quaternion rocketInitRot;

    EnvironmentParameters m_TesetParams;

    private Transform AreaTrans;
    private Transform RocketTrans;
    private Transform GoalTrans;
    private Transform Mass1Trans;
    private Transform Mass2Trans;

    private Rigidbody RocketAgent_Rigidbody;
    private Rigidbody Mass1_Rigidbody;
    private Rigidbody Mass2_Rigidbody;

    Vector3 mass1InitPos;
    Vector3 mass2InitPos;

    void Start()
    {
        Debug.Log(m_TesetParams);

        AreaTrans = gameObject.transform;
        RocketTrans = RocketAgent.transform;
        GoalTrans = Goal.transform;
        Mass1Trans = Mass1.transform;
        Mass2Trans = Mass2.transform;

        areaInitPos = AreaTrans.position;
        rocketInitPos = RocketTrans.position;
        rocketInitRot = RocketTrans.rotation;

        RocketAgent_Rigidbody = RocketAgent.GetComponent<Rigidbody>();
        Mass1_Rigidbody = Mass1.GetComponent<Rigidbody>();
        Mass2_Rigidbody = Mass2.GetComponent<Rigidbody>();
    }

    public void AreaSetting()
    {
        RocketAgent_Rigidbody.velocity = Vector3.zero;
        RocketAgent_Rigidbody.angularVelocity = Vector3.zero;
        Mass1_Rigidbody.velocity = Vector3.zero;
        Mass1_Rigidbody.angularVelocity = Vector3.zero;
        Mass2_Rigidbody.velocity = Vector3.zero;
        Mass2_Rigidbody.angularVelocity = Vector3.zero;

        RocketTrans.position = rocketInitPos;
        RocketTrans.rotation = rocketInitRot;

        GoalTrans.position = areaInitPos + new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
        Mass1Trans.position = areaInitPos + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        Mass2Trans.position = areaInitPos + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));

        mass1InitPos = Mass1Trans.position;
        mass2InitPos = Mass2Trans.position;
    }

    void Update()
    {
        Mass1Trans.position = mass1InitPos;
        Mass2Trans.position = mass2InitPos;
    }
}
