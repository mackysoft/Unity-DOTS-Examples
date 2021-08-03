using UnityEngine;
using Unity.Entities;

namespace MackySoft.DOTSExamples {
	public class PlayerMoveAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

		[SerializeField]
		float m_Speed = 5f;

		public void Convert (Entity entity,EntityManager dstManager,GameObjectConversionSystem conversionSystem) {
			dstManager.AddComponent<Player>(entity);
			dstManager.AddComponentData(entity,new Mover { Speed = m_Speed });
		}

	}
}