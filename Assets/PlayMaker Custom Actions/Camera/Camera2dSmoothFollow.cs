// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=11266.0


using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Camera 2d  SmoothFollow")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11266.0")]
	public class Camera2dSmoothFollow : FsmStateAction
	{
		[RequiredField]
		public FsmGameObject gameObject;
		public  FsmFloat dampTime;
		private Vector3 velocity = Vector3.zero;
		public FsmGameObject targetGameObject;
		private Transform target;
		public FsmBool useFixedUpdate;

		public override void Reset()
		{
			dampTime = 0.15f;
			useFixedUpdate = false;
		}

		public override void Awake()
		{
			if (useFixedUpdate.Value) Fsm.HandleFixedUpdate = true;
		}		

		public override void OnEnter()
		{
			if (useFixedUpdate.Value) OnFixedUpdate () ;
				else OnLateUpdate ();
		}

		public override void OnFixedUpdate () 
		{
			if (useFixedUpdate.Value) SmoothCamera () ;
			
		}

			public override void OnLateUpdate () 
		{
			if (!useFixedUpdate.Value) SmoothCamera () ;
		}
	
		void SmoothCamera () 
		{
			target = targetGameObject.Value.GetComponent<Transform>();
			Vector3 point = gameObject.Value.GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - gameObject.Value.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = gameObject.Value.transform.position + delta;
			gameObject.Value.transform.position = Vector3.SmoothDamp(gameObject.Value.transform.position, destination, ref velocity, dampTime.Value);
			
		}


	}
}

