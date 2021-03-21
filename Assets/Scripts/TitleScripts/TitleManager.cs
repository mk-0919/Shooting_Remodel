using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TitleManager : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public TitlePlayerMovement TitlePlayerMovement;

    private void Start()
    {
        TitlePlayerMovement.PlayerMove(6);
        VideoPlayer.Play();
    }
}
