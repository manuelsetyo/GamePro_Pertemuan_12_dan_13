using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : AgentBehavior {
    bool activeStreing;
    Quaternion lastRotation;

    public override void Update()
    {
        if (Vector3.Distance(target.transform.position, this.gameObject.transform.position) < 10f) {
            activeStreing = true;
            this.gameObject.transform.LookAt(target.transform);
            lastRotation = this.gameObject.transform.localRotation;
        } else {
            this.gameObject.transform.localRotation = lastRotation;
            activeStreing = false;
        }

        base.Update();
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        if (activeStreing) {
            steering.linear = target.transform.position - transform.position;
            steering.linear.Normalize();
            steering.linear = steering.linear * agent.maxAccel;
        }

        return steering;
    }
}
