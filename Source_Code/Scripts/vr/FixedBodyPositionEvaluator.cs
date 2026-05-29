using UnityEngine;
using Unity.XR.CoreUtils;

namespace UnityEngine.XR.Interaction.Toolkit.Locomotion
{
    public class FixedBodyPositionEvaluator : MonoBehaviour, IXRBodyPositionEvaluator
    {
        [SerializeField] private XROrigin m_XROrigin;

        // 1. Position globale du corps (utilisée pour le déplacement)
        public Vector3 GetBodyPosition(XRMovableBody movableBody)
        {
            if (m_XROrigin != null)
            {
                return m_XROrigin.transform.position;
            }
            return movableBody.xrOrigin.transform.position;
        }

        // 2. Position locale par rapport au sol de l'XR Origin (la méthode manquante !)
        public Vector3 GetBodyGroundLocalPosition(XROrigin xrOrigin)
        {
            // On renvoie Vector3.zero pour dire que le corps est PILE au niveau 
            // du pivot de l'XR Origin (ses pieds), sans aucun décalage dû à la caméra.
            return Vector3.zero;
        }
    }
}