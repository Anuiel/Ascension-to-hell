using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    public int shiftX;

    [SerializeField]
    public int shiftY;

    public enum TileState
    {
        Empty, 
        Wall,
        OuterWall,
        Red
    }

    readonly List<List<Vector2>> figures = new List<List<Vector2>> { 
        new List<Vector2> { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2), new Vector2(0, 3) }, // line horizontal
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0) }, // line vertical
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) }, // square
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1) }, // z regular
        new List<Vector2> { new Vector2(0, 0), new Vector2(0, 1), new Vector2(-1, 1), new Vector2(-1, 2) }, // z vertical
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, -1), new Vector2(2, -1) }, // s regular
        new List<Vector2> { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 2) }, // s vertical
        new List<Vector2> { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2), new Vector2(1, 2) }, // l regular
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(2, -1) }, // l 90
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 2) }, // l 180
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(0, 1) }, // l 270
        new List<Vector2> { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2), new Vector2(-1, 2) }, // j regular
        new List<Vector2> { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1) }, // j 90
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 2) }, // j 180
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(2, 1) }, // j 270
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 0) }, // t regular
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, -1) }, // t 90
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, -1), new Vector2(2, 0) }, // t 180
        new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, -1) }, // t 270
        new List<Vector2> { 
            new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), 
            new Vector2(0, 1), new Vector2(0, 1), new Vector2(1, 1), 
            new Vector2(2, 1), new Vector2(0, 2), new Vector2(1, 2), new Vector2(2, 2)
        }
    };

    [SerializeField]
    List<float> figProbs = new List<float>();

    List<float> weights = new List<float> { 
        0.5f, 0.5f, 1, 0.5f, 0.5f, 0.5f, 0.5f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.01f
    };

    [SerializeField]
    public int fieldWidth;

    [SerializeField]
    public int fieldHeight;

    [SerializeField]
    GameObject obstacles;

    TileState[,] field;

    [SerializeField]
    List<Vector2> adjFields;

    [SerializeField]
    List<Vector2> walls;

    [SerializeField]
    float prob = 0.5f;

    private GameObject[,] obstacleReference;

    void Start()
    {
        walls = new List<Vector2> { };
        field = new TileState[fieldWidth, fieldHeight];
        obstacleReference = new GameObject[fieldWidth, fieldHeight];
        for (int i = 0; i < fieldWidth; i++)
        FillBorder();
        figProbs = getProbs(weights);
    }

    void FillBorder() {
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

    public void generateFieldMarine(int obst)
    {
        for (int cnt = 0; cnt < obst; cnt++)
        {
            float prob = Random.Range(0f, 1f);
            int pos = 0;
            for (int i = 0; i < figProbs.Count - 1; i++)
            {
                if (figProbs[i] < prob && figProbs[i + 1] > prob)
                    pos = i;
            }
            figureAdd(pos);
        }
        createFromScratchSym();
    }

    void createFromScratch()
    {
        for (int i = 0; i < fieldWidth; i++)
        {
            for (int j = 0; j < fieldHeight; j++)
            {
                if (field[i, j] == TileState.Wall || field[i, j] == TileState.OuterWall)
                {
                    obstacleReference[i, j] = Instantiate(obstacles, new Vector3(i - shiftX, j - shiftY, 0), obstacles.transform.rotation);
                }
            }
        }
    }

    public void clear() {
        for (int i = 0; i < fieldWidth; i++)
        {
            for (int j = 0; j < fieldHeight; j++)
            {
                if (obstacleReference[i, j])
                {
                    Destroy(obstacleReference[i, j]);
                    field[i, j] = TileState.Empty;
                }
            }
        }
        FillBorder();
    }

    void createFromScratchSym()
    {
        vertSym();
        horSym();
        createFromScratch();
    }

    void vertSym()
    {
        for (int i = 0; i < fieldWidth; i++)
        {
            for (int j = fieldHeight / 2; j < fieldHeight; j++)
            {
                field[i, j] = field[i, fieldHeight - 1 - j];
            }
        }
    }

    void horSym()
    {
        for (int i = fieldWidth / 2; i < fieldWidth; i++)
        {
            for (int j = 0; j < fieldHeight; j++)
            {
                field[i, j] = field[fieldWidth - i - 1, j];
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

    bool figureAdd(int idx)
    {
        bool fitted = false;
        int attempt = 0;
        int maxAttempts = 20;
        while (!fitted && attempt < maxAttempts)
        {
            attempt += 1;
            int x = Random.Range(0, fieldWidth);
            int y = Random.Range(0, fieldHeight);
            Vector2 chosenField = new Vector2(x, y);
            fitted = figureFit(chosenField, figures[idx]);
        }
        return fitted;
    }

    bool figureFit(Vector2 pos, List<Vector2> figure)
    {
        for (int i = 0; i < figure.Count; i++)
        {
            if (!isEmptyTile(pos - figure[i]))
            {
                return false;
            }
        }
        for (int i = 0; i < figure.Count; i++)
        {
            field[(int)(pos.x - figure[i].x), (int)(pos.y - figure[i].y)] = TileState.Wall;
        }
        for (int i = 0; i < figure.Count; i++)
        {
            for (int j = 0; j < adjFields.Count; j++)
            {
                if (isEmptyTile(pos - figure[i] + adjFields[j]))
                {
                    field[(int)(pos.x - figure[i].x + adjFields[j].x), (int)(pos.y - figure[i].y + adjFields[j].y)] = TileState.Red;
                }
            }    
        }
        return true;
    }

    List<float> getProbs(List<float> weigh)
    {
        float sum = weigh.Sum();
        List<float> pr = new List<float>();
        for (int i = 0; i < weigh.Count; i++)
        {
            pr.Add(weigh[i] / sum);
        }
        List<float> cumSum = new List<float>();
        cumSum.Add(0);
        for (int i = 0; i < pr.Count; i++)
        {
            cumSum.Add(cumSum[i] + pr[i]);
        }
        cumSum[cumSum.Count - 1] = 1;
        return cumSum;
    }

    public TileState[,] getField()
    {
        return field;
    }
}
