using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentModel : Agent
{
   
    private Rigidbody rbody;
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private Transform target;
    void Start()
    {
        this.rbody = this.GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = new Vector3(0, 0.5f, 0);
        this.rbody.velocity = Vector3.zero;
        this.rbody.angularVelocity = Vector3.zero;

        this.target.transform.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.target.localPosition);
        sensor.AddObservation(this.rbody.velocity.x);
        sensor.AddObservation(this.rbody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var h = actions.ContinuousActions[0];
        var v = actions.ContinuousActions[1];
        var dir = new Vector3(h, 0, v);
        this.rbody.AddForce(dir * this.moveForce);

        var distance = Vector3.Distance(this.target.localPosition,this.transform.localPosition);
        if(distance < 1.5f)
        {
            this.AddReward(1);
            this.EndEpisode();
        }
        else if(this.transform.localPosition.y < -0.5f)
        {
            this.EndEpisode();
        }


    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.ContinuousActions;
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
    }
}
