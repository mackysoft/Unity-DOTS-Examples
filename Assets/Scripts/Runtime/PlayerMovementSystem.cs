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
			Vector2 input = m_MoveInput.ReadValue<Vector2>();
			float3 movement = new float3(input.x,0f,input.y);

			float deltaTime = UnityEngine.Time.deltaTime;
			Entities
				.WithAll<Player>()
				.ForEach((ref Translation translation,in Mover mover) => {
					translation.Value += movement * mover.Speed * deltaTime;
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