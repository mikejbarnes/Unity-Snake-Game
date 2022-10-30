using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour
{
    public GameObject GraphicsManager;
    public GameObject Model;
    
    private GraphicsManagerBehavior _graphicsScripts;
    private ModelBehavior _modelScripts;
    private int _xDirection = 1;
    private int _yDirection = 0;
    private float _timeElapsed;
    private int _resultState;

    void Start()
    {
        _graphicsScripts = GraphicsManager.GetComponent<GraphicsManagerBehavior>();
        _modelScripts = Model.GetComponent<ModelBehavior>();

        _graphicsScripts.RenderSnake(_modelScripts.Snake);
        _graphicsScripts.RenderFood(_modelScripts.Food);
    }

    void FixedUpdate()
    {
        GetInput();

        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= GameBoardParameters.SecondsPerGameTic)
        {
            _resultState = _modelScripts.OnUpdate(_xDirection, _yDirection);

            switch (_resultState)
            {
                case ResultStates.Grow:
                    _graphicsScripts.GrowSnake();
                    _graphicsScripts.RenderFood(_modelScripts.Food);
                    break;
                case ResultStates.Collision:
                    SceneManager.LoadScene(2);
                    break;
                case ResultStates.Win:
                    break;
            }

            _graphicsScripts.RenderSnake(_modelScripts.Snake);

            _timeElapsed -= GameBoardParameters.SecondsPerGameTic;
        }
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _xDirection = 1;
            _yDirection = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _xDirection = -1;
            _yDirection = 0;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            _xDirection = 0;
            _yDirection = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _xDirection = 0;
            _yDirection = -1;
        }
    }
}
