using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    GameObject player;

    void LookForPlayer() {
        player = GameObject.Find("Player(Clone)");
    }

    private void LateUpdate() {
        if (player != null) {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        }
        else {
            LookForPlayer();
        }
    }
}
