using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<BasicEnemy> pool;
    
    [SerializeField]
    FieldManager fm;

    FieldManager.TileState[,] field;

    void generateWave(int maxPrice)
    {
        int wavePrice = 0;
        int leftover = maxPrice;
        int tries = 0;
        field = getField();
        while (tries < 20 && leftover < maxPrice)
        {
            int choice = Random.Range(0, pool.Count);
            if (pool[choice].price > leftover)
            {
                tries += 1;
                continue;
            }
            if (createEnemy(choice))
            {
                wavePrice += pool[choice].price;
                leftover -= pool[choice].price;
            }
            else
            {
                tries += 1;
            }
        }
    }

    bool createEnemy(int pos)
    {
        int width = fm.fieldWidth;
        int height = fm.fieldHeight;
        bool flag = false;
        int tryy = 0;
        while (!flag && tryy < 20)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            if (field[x, y] == FieldManager.TileState.Empty || field[x, y] == FieldManager.TileState.Red)
            {
                Instantiate(pool[pos], new Vector2(x - fm.shiftX, y - fm.shiftY), pool[pos].transform.rotation);
                flag = true;
            }
            else
            {
                tryy += 1;
            }
        }
        return flag;
    }

    FieldManager.TileState[,] getField()
    {
        return fm.getField();
    }
}
