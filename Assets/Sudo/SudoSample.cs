using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这就是一个数独的例子
public class SudoSample : MonoBehaviour
{
    private Sudo _Sudo = new Sudo();

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int[] origin = new[]
            {
                0,0,0,0,1,3,0,6,0,
                0,4,7,0,0,0,8,5,0,
                0,6,1,0,0,0,0,0,0,
                4,0,0,7,2,0,0,0,0,
                0,0,0,0,0,0,0,0,0,
                0,0,0,0,8,5,0,0,3,
                0,0,0,0,0,0,7,2,0,
                0,9,4,0,0,0,1,3,0,
                0,7,0,8,6,0,0,0,0,
            };
            _Sudo.ResetOrigin();
            for (int i = 0; i < origin.Length; ++i)
            {
                _Sudo.AddOrigin(i, origin[i]);
            }
            _Sudo.Solve();

            var results = _Sudo.GetResults();
            if (results.Count > 0)
            {
                foreach (var result in results)
                {
                    Print(result, Sudo.COL_COUNT, origin);
                }
            }
            else
            {
                Debug.Log("no result");
            }
        }
    }

    public void Print(int[] data, int col, int[] origine)
    {
        var text = string.Empty;
        for (int i = 0; i < data.Length; ++i)
        {
            var value = string.Empty;
            if (origine != null && origine[i] != 0)
            {
                value = origine[i].ToString();
            }
            else
            {
                value = $"<color=#00ff00ff>{data[i].ToString()}</color>";
            }
            if ((i + 1) % col == 0)
            {
                text += value + "\n";
            }
            else
            {
                text += value + ",";
            }
        }
        Debug.Log(text);
    }
}
