using UnityEngine;

public class RobotTracksController : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right,
        Forward
    }


    [SerializeField] GameObject trackLeft;
    [SerializeField] GameObject trackRight;

    [SerializeField] public Direction direction = Direction.Forward;

    private float _rotation = 0f;
    public float rotation
    {
        get { return _rotation; }
        set { _rotation = value;

            switch (direction)
            {
                case Direction.Left:
                    animationLeft["Track_animation"].normalizedTime = (rotation * 0.11f) * 2 * 0.5f;
                    animationRight["Track_animation"].normalizedTime = (rotation * 0.11f) * 2 * -0.5f;
                    break;
                case Direction.Right:
                    animationLeft["Track_animation"].normalizedTime = (rotation * 0.11f) * 2 * -0.5f;
                    animationRight["Track_animation"].normalizedTime = (rotation * 0.11f) * 2 * 0.5f;
                    break;
                case Direction.Forward:
                    animationLeft["Track_animation"].normalizedTime = (rotation * 0.11f);
                    animationRight["Track_animation"].normalizedTime = (rotation * 0.11f);
                    break;
            }
        }
    }

    

    private Animation animationLeft;
    private Animation animationRight;

    private void Awake()
    {
        animationLeft  = trackLeft.GetComponent<Animation>();
        animationRight = trackRight.GetComponent<Animation>();

        animationLeft["Track_animation"].speed = 0f;
        animationRight["Track_animation"].speed = 0f;
    }

}
