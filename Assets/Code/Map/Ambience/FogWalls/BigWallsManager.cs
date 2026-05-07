using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWallsManager : MonoBehaviour
{
    public static BigWallsManager Instance;

    [SerializeField] private GameObject _bigDoorOne;
    [SerializeField] private GameObject _bigDoorTwo;
    [SerializeField] private GameObject _bigDoorThree;
    [SerializeField] private GameObject _bigDoorFour;

    private bool _secondPuzzleCompleted;
    private bool _thirdPuzzleCompleted;
    private bool _forthPuzzleCompleted;

    private bool _firstSetCompleted;
    private bool _secondSetCompleted;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

    }

    public void PuzzleCompleted(int puzzleNumber)
    {
        switch (puzzleNumber)
        {
            case 1: _firstSetCompleted = true; break;

            case 2: _secondPuzzleCompleted = true; break;
                
            case 3: _thirdPuzzleCompleted = true; break;

            case 4: _forthPuzzleCompleted = true; break;


        }

        if (_secondPuzzleCompleted && _thirdPuzzleCompleted) _secondSetCompleted = true;

        if (_firstSetCompleted) OpenFirstsWallSet();

        if(_secondSetCompleted) OpenSecondWallSet();

        if(_forthPuzzleCompleted) OpenLastWallWall();



    }

    private void OpenFirstsWallSet()
    {
        _bigDoorOne.GetComponent<BigWall>().OpenDoor();
        _bigDoorTwo.GetComponent<BigWall>().OpenDoor();
    }
    private void OpenSecondWallSet()
    {
        _bigDoorThree.GetComponent<BigWall>().OpenDoor();
    }

    private void OpenLastWallWall()
    {
        _bigDoorFour.GetComponent<BigWall>().OpenDoor();
    }

}
