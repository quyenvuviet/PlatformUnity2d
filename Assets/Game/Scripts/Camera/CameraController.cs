using UnityEngine;
namespace Game.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float speed;
        private float currentPosX;
        private Vector3 velocity = Vector3.zero;

        [SerializeField]
        private GameObject player;

        [SerializeField]
        private float aheadDistance;

        [SerializeField]
        private float cammerSpeed;

        private float lookAheadl;

        private void Update()
        {
            transform.position = new Vector3(player.transform.position.x + lookAheadl, transform.position.y, transform.position.z);
            lookAheadl = Mathf.Lerp(lookAheadl, (aheadDistance * player.transform.localScale.x), Time.deltaTime * cammerSpeed);
        }

        public void MovetoNextRoom(Transform _newRom)
        {
            currentPosX = _newRom.position.x;
        }
    }
}
