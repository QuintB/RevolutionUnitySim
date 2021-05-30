using UnityEngine;

namespace PathCreation.Examples
{
  // Moves along a path at constant speed.
  // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
  public class PathFollower : MonoBehaviour
  {
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5f;
    public float rotationBuffer = 100f;
    public float honingDistance = 1f;
    public Rigidbody car;
    private float timeCount = 0.0f;
    private float state = 0.0f;
    public float rotSpeed = 0.0f;
    public Vector3 multiplication;

    //states
    //0 = free
    //1 = going to the path
    //2 = reached path, now in self-driving mode

    float distanceTravelled;
    Vector3 rawLocation = new Vector3();
    Vector3 newLocation = new Vector3();

    Vector3 rawTarget = new Vector3();
    Vector3 cleanTarget = new Vector3();

    Vector3 rawPos = new Vector3();
    Vector3 cleanPos = new Vector3();

    void Start() {
      if (pathCreator != null) {
        // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
        pathCreator.pathUpdated += OnPathChanged;
      }
    }

    void Update() {
      rawTarget = pathCreator.path.GetClosestPointOnPath(transform.position);
      cleanTarget = new Vector3(pathCreator.path.GetClosestPointOnPath(transform.position).x, 0, pathCreator.path.GetClosestPointOnPath(transform.position).z);
      rawPos = transform.position;
      cleanPos = new Vector3(transform.position.x, 0, transform.position.z);

      if (pathCreator != null) {
        if (Input.GetKeyDown("joystick button 0")) {
          if (state == 0.0f) {
            state = 1f;
          } else {
            state = 0.0f;
          }
        }

        if (state == 1f){

          Debug.Log("State 1");
          float step = rotSpeed * Time.deltaTime;

          Vector3 lookPos = cleanTarget - cleanPos;
          Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
          //transform.rotation = lookRot;
          transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, step);

          Debug.Log(Vector3.Distance(cleanPos, cleanTarget));
          
          if (Vector3.Distance(cleanPos, cleanTarget) < honingDistance){
            state = 2f;
            Debug.Log("State 2 toggle");
          }

        } else if (state == 2f){
          timeCount = 0.0f;
          Debug.Log("State 2");
          distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
          rawLocation = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
          newLocation = new Vector3(rawLocation.x, transform.position.y, rawLocation.z);
          transform.position = newLocation;

          float eulerYVal = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction).eulerAngles.y;
          Quaternion rotation = Quaternion.Euler(transform.rotation.x, eulerYVal, transform.rotation.z);
          //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
          transform.rotation = rotation;
          Debug.Log(eulerYVal);
          Debug.Log(rotation);

          multiplication = new Vector3(10000,0,10000);

          car.AddRelativeForce(Vector3.forward * 10000);
        }
      }
    }

    void OnPathChanged() {
      distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
  }
}
