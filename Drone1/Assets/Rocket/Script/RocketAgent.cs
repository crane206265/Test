using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using PA_DronePack;


public class RocketAgent : Agent
{
    private PA_DroneController dcoScript;

    public RocketSetting area;
    public GameObject goal;
    public GameObject mass1;
    public GameObject mass2;

    float preDist;

    private Transform agentTrans;
    private Transform goalTrans;
    private Transform mass1Trans;
    private Transform mass2Trans;

    private Rigidbody agent_Rigidbody;
    private Rigidbody mass1_Rigidbody;
    private Rigidbody mass2_Rigidbody;

    public override void Initialize()
    {
        dcoScript = gameObject.GetComponent<PA_DroneController>();

        agentTrans = gameObject.transform;
        goalTrans = goal.transform;
        mass1Trans = mass1.transform;
        mass2Trans = mass2.transform;

        agent_Rigidbody = gameObject.GetComponent<Rigidbody>();
        mass1_Rigidbody = mass1.GetComponent<Rigidbody>();
        mass2_Rigidbody = mass2.GetComponent<Rigidbody>();

        Academy.Instance.AgentPreStep += WaitTimeInference;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(agentTrans.position - goalTrans.position);

        sensor.AddObservation(agent_Rigidbody.velocity);

        sensor.AddObservation(agent_Rigidbody.angularVelocity);
    }

    bool isCollided = false;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Mass1")
        {
            isCollided = true;
            Debug.Log("Mass1");
        }
        else if(collision.gameObject.name == "Mass2")
        {
            isCollided = true;
            Debug.Log("Mass2");
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        AddReward(-0.01f);

        var actions = actionBuffers.ContinuousActions;

        float moveX = Mathf.Clamp(actions[0], -1, 1f);
        float moveY = Mathf.Clamp(actions[1], -1, 1f);
        float moveZ = Mathf.Clamp(actions[2], -1, 1f);

        dcoScript.DriveInput(moveX);
        dcoScript.StrafeInput(moveY);
        dcoScript.LiftInput(moveZ);

        float goaldistance = Vector3.Magnitude(goalTrans.position - agentTrans.position);
        //float mass1distance = Vector3.Magnitude(mass1Trans.position - agentTrans.position);
        //float mass2distance = Vector3.Magnitude(mass2Trans.position - agentTrans.position);

        //float mass1radius = mass1Trans.scale/2;
        //float mass2radius = mass2Trans.scale/2;

        if(goaldistance <= 0.5f)
        {
            SetReward(1f);
            EndEpisode();
        }
        else if(goaldistance > 30f | isCollided)
        {
            SetReward(-1f);
            EndEpisode();
        }
        else
        {
            float reward = preDist - goaldistance;
            SetReward(reward);
            preDist = goaldistance;   
        }
    }

    public override void OnEpisodeBegin()
    {
        area.AreaSetting();

        preDist = Vector3.Magnitude(goalTrans.position - agentTrans.position);    
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
        continuousActionsOut[2] = Input.GetAxis("Mouse ScrollWheel");
    }

    public float DecisionWaitingTime = 5f;
    float m_currentTime = 0f;

    public void WaitTimeInference(int action)
    {
        if(Academy.Instance.IsCommunicatorOn)
        {
            RequestDecision();
        }
        else
        {
            if(m_currentTime >= DecisionWaitingTime)
            {
                m_currentTime = 0f;
                RequestDecision();
            }
            else
            {
                m_currentTime += Time.fixedDeltaTime;
            }
        }
    }

}
