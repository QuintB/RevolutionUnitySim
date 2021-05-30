using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class woahBuddySlowDown : MonoBehaviour
{

  public float maxSpeed = 30f;
  public Text text;
  public float exaggerationValue;
  private float speedValue;
  public Transform needleTranform;


  private Rigidbody localRgb;


  public const float MAX_SPEED_ANGLE = 0;
  public const float ZERO_SPEED_ANGLE = 180;

  private Transform speedLabelTemplateTransform;


  public float speedMax;
  public float speed;

  // Start is called before the first frame update
  void Start()
  {
    localRgb = GetComponent<Rigidbody>();
    speed = 0f;
    speedMax = 90f;
  }
  void FixedUpdate(){
    if(localRgb.velocity.magnitude > maxSpeed) {
      localRgb.velocity = Vector3.ClampMagnitude(localRgb.velocity, maxSpeed);
    }

    speedValue = Mathf.Round(localRgb.velocity.magnitude*exaggerationValue);
    text.text = speedValue.ToString();

    speed = localRgb.velocity.magnitude*exaggerationValue;

    needleTranform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    Debug.Log(GetSpeedRotation());
  }

  private float GetSpeedRotation() {
      float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

      float speedNormalized = speed / speedMax;

      return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
  }


}
