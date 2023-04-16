using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    int shiftX;

    [SerializeField]
    int shiftY;

    public enum TileState
    {
        Empty, 
        Wall,
        OuterWall
    }

    [SerializeField]
    int fieldWidth;

    [SerializeField]
    int fieldHeight;

    [SerializeField]
    GameObject obstacles;

    TileState[,] field;

    [SerializeField]
    List<Vector2> adjFields;

    [SerializeField]
    List<Vector2> walls;

    [SerializeField]
    float prob = 0.5f;

    void Start()
    {
        walls = new List<Vector2> { };
        field = new TileState[fieldWidth, fieldHeight];
        for (int i = 0; i < fieldWidth; i++)
        {
            for (int j = 0; j < fieldHeight; j++)
            {
                if (i == 0 || i == fieldWidth - 1 || j == 0 || j == fieldHeight - 1)
                {
                    field[i, j] = TileState.OuterWall;
                }
                else
                    field[i, j] = TileState.Empty;
            }
        }
    }


    public void generateField(int obst)
    {
        for (int cnt = 0; cnt < obst; cnt++)
        {
            float decision = Random.Range(0, 1f);
            if (decision < prob || cnt == 0)
            {
                while (!addWall()) { };
            }
            else
            {
                while (!addToWall()) { };
            }
        }
        createFromScratch();
    }

    void createFromScratch()
    {
        for (int i = 0; i < fieldWidth; i++)
        {
            for (int j = 0; j < fieldHeight; j++)
            {
                if (field[i, j] == TileState.Wall || field[i, j] == TileState.OuterWall)
                {
                    Instantiate(obstacles, new Vector3(i - shiftX, j - shiftY, 0), obstacles.transform.rotation);
                }
            }
        }
    }

    bool addToWall()
    {
        int wallLen = walls.Count;
        int pos = Random.Range(0, wallLen);
        Vector2 chosenField = walls[pos];
        for (int i = 0; i < adjFields.Count; i++)
        {
            if (isEmptyTile(chosenField + adjFields[i]))
            {
                Vector2 newWall = chosenField + adjFields[i];
                if (isEmptyTile(newWall))
                {
                    Debug.Log(newWall);
                    field[(int)newWall.x, (int)newWall.y] = TileState.Wall;
                    walls.Add(newWall);
                    return true;
                }
            }
        }
        return false;
    }

    bool addWall()
    {
        int x = Random.Range(0, fieldWidth);
        int y = Random.Range(0, fieldHeight);
        Vector2 chosenField = new Vector2(x, y);
        if (isEmptyTile(new Vector2(x, y)))
        {
            for (int i = 0; i < adjFields.Count; i++)
            {
                if (!isEmptyTile(chosenField + adjFields[i]))
                {
                    return false;
                }
            }
            walls.Add(chosenField);
            field[(int)chosenField.x, (int)chosenField.y] = TileState.Wall;
            return true;
        }
        return false;
    }

    bool isEmptyTile(Vector2 pos)
    {
        if (pos.x <= fieldWidth - 1 & pos.x >= 0 & pos.y <= fieldHeight - 1 & pos.y >= 0)
            return field[(int)pos.x, (int)pos.y] == TileState.Empty;
        return false;
    }

    public TileState[,] getField()
    {
        return field;
    }
}
