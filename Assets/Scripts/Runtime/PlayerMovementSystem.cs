using UnityEngine;
using Unity.Entities;
using UnityEngine.InputSystem;
using Unity.Transforms;
using Unity.Mathematics;

namespace MackySoft.DOTSExamples {
	public class PlayerMovementSystem : SystemBase {

		PlayerInputActions m_Input;
		InputAction m_MoveInput;

		protected override void OnCreate () {
			m_Input = new PlayerInputActions();
			m_Input.Enable();

			m_MoveInput = m_Input.Player.Move;
		}

		protected override void OnUpdate () {
			float deltaTime = UnityEngine.Time.deltaTime;
			Vector2 input = m_MoveInput.ReadValue<Vector2>();
			Vector3 movement = new Vector3(input.x,0f,input.y);
			Entities
				.WithAll<Player,Mover>()
				.ForEach((Mover mover,ref LocalToWorld transform,in Entity entity) => {
					transform.Value = math.mul(transform.Value,float4x4.Translate(movement * mover.Speed * deltaTime));
				})
				.WithBurst()
				.ScheduleParallel();
		}

		protected override void OnDestroy () {
			m_Input.Disable();
			m_Input.Dispose();
		}

	}
}