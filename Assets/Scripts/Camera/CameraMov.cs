using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
  public Transform player;
  void Update()
  {
      transform.position = new Vector3 (player.position.x-2, player.position.y+18, player.position.z-10); // Camera follows the player but 6 to the right
  }
}
