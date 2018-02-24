using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

//该脚本是控制游戏物体到达目标位置
public class MySeek:Action//调用行为树控制
{
	//public float speed;
	public SharedFloat sharedSpeed;
	public Transform target;//达到的目标位置
	//public float arriveDistance=0.1f;
	public SharedFloat shareArriveDistance=0.1f;
	private float sqrArriveDistance;
	public override void OnStart()
	{
		sqrArriveDistance = shareArriveDistance.Value * shareArriveDistance.Value;
	}

	public override TaskStatus OnUpdate()
	{
		if (target == null) {
		
			return TaskStatus.Failure;
		}

		transform.LookAt (target.position);//直接朝向目标位置
		transform.position=Vector3.MoveTowards(transform.position,target.position,sharedSpeed.Value*Time.deltaTime);
		if ((target.position - target.position).sqrMagnitude < sqrArriveDistance) {
		
			return TaskStatus.Success;
		}
		return TaskStatus.Running;
	}
}
