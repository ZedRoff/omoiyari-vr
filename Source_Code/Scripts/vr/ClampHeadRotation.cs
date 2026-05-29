using UnityEngine;

public class ClampHeadRotation : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private Transform m_XROriginTransform;
    [SerializeField] private float m_MaxRotationAngle = 90f; // 90° à gauche + 90° à droite = 180°

    void LateUpdate()
    {
        if (m_XROriginTransform == null) return;

        // 1. Obtenir la rotation locale de la caméra par rapport au corps (XR Origin)
        Vector3 cameraForward = transform.forward;
        Vector3 originForward = m_XROriginTransform.forward;

        // Projeter sur le plan horizontal (Y) pour ignorer le fait de regarder en haut/bas
        cameraForward.y = 0;
        originForward.y = 0;

        // 2. Calculer l'angle entre le corps et la tête
        float angle = Vector3.SignedAngle(originForward, cameraForward, Vector3.up);

        // 3. Si l'angle dépasse les bornes, on force la rotation maximale
        if (angle > m_MaxRotationAngle)
        {
            transform.rotation = Quaternion.AngleAxis(m_MaxRotationAngle, Vector3.up) * Quaternion.LookRotation(originForward);
        }
        else if (angle < -m_MaxRotationAngle)
        {
            transform.rotation = Quaternion.AngleAxis(-m_MaxRotationAngle, Vector3.up) * Quaternion.LookRotation(originForward);
        }
    }
}