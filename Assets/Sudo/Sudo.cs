using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这是一个数独的算法
/// </summary>
public class Sudo
{
    //天天天
    public const int SQR_COUNT = 3;
    public const int COL_COUNT = 9;
    public const int MAX_COUNT = 81;
    public const int MAX_RESULT = 5;
    
    //原始数据
    private int[] _origin= new int[MAX_COUNT];
    //缓存数据
    private int[] _caches= new int[MAX_COUNT];
    //结果数据
    private List<int[]> _results = new List<int[]>();

    private int[] _rows = new int[COL_COUNT];
    private int[] _cols = new int[COL_COUNT];
    private int[] _quar = new int[COL_COUNT];

    public int GetIndex(int x, int y)
    {
        return y * COL_COUNT + x;
    }

    public void GetXY(int index, ref int x, ref int y)
    {
        x = index % COL_COUNT;
        y = index / COL_COUNT;
    }
    public void ResetOrigin()
    {
        if(_origin == null)
            _origin = new int[MAX_COUNT];
        for (int i = 0; i < MAX_COUNT; ++i)
        {
            _origin[i] = 0;
        }
    }
    public void AddOrigin(int x, int y, int value)
    {
        AddOrigin(GetIndex(x, y), value);
    }
    public void AddOrigin(int index, int value)
    {
        if (index < 0 || index >= MAX_COUNT)
            return;
        if (value < 1 || value > COL_COUNT)
            return;
        _origin[index] = value;
    }
    public List<int[]> GetResults()
    {
        return _results;
    }
    
    public void Solve()
    {
        //初始化数据
        for (int i = 0; i < COL_COUNT; ++i)
        {
            _rows[i] = 0;
            _cols[i] = 0;
            _quar[i] = 0;
        }
        for (int i = 0; i < MAX_COUNT; ++i)
        {
            _caches[i] = _origin[i];
            int x=0,y=0;
            GetXY(i, ref x, ref y);
            _AddFlag(x,y, _caches[i]);
        }
        _results.Clear();
        //开始递归
        _SolveRecursion(0, 0);
    }

    void _SolveRecursion(int x, int y)
    {
        //Debug.Log($"_SolveRecursion {x} {y} ");
        if (x >= COL_COUNT)
        {
            x = 0;
            ++y;
        }
        int index = 0;
        do
        {
            index = GetIndex(x, y);
            //Debug.Log(index);
            if (index >= MAX_COUNT)
            {
                //成功了
                _results.Add(_CloneData(_caches));
                return;
            }
            if (_origin[index] == 0)
                break;
            ++x;
            if (x >= COL_COUNT)
            {
                x = 0;
                ++y;
            }
        } while (true);

//        int value = _FindNextNum(x, y, 1);
//        //Debug.Log($"get next num {x} {y} {value}");
//        while (value != -1)
//        {
//            _caches[index] = value;
//            //Debug.Log($"solve try {x} {y} {value}");
//            _AddFlag(x, y, value);
//            _SolveRecursion(x + 1, y);
//            _DelFlag(x, y, value);
//            
//            if (_results.Count >= MAX_RESULT)
//                return;
//            
//            value = _FindNextNum(x, y, _caches[index] + 1);
//        }

        for (int i = 1; i <= COL_COUNT; ++i)
        {
            if (!_CheckFlag(x,y,i))
                continue;
            _caches[index] = i;
            //Debug.Log($"solve try {x} {y} {value}");
            _AddFlag(x, y, i);
            _SolveRecursion(x + 1, y);
            _DelFlag(x, y, i);
            
            if (_results.Count >= MAX_RESULT)
                return;
        }

    }
    
    bool _CheckFlag(int x, int y, int num)
    {
        int mask = 1 << (num - 1);
        int index = x / SQR_COUNT + y / SQR_COUNT * SQR_COUNT;
        return (_cols[x] & mask) == 0 &&
               (_rows[y] & mask) == 0 &&
               (_quar[index] & mask) == 0;
    }

    void _AddFlag(int x, int y, int num)
    {
        int mask = 1 << (num - 1);
        int index = x / SQR_COUNT + y / SQR_COUNT * SQR_COUNT;
        _cols[x] = _cols[x] | mask;
        _rows[y] = _rows[y] | mask;
        _quar[index] = _quar[index] | mask;
    }
    void _DelFlag(int x, int y, int num)
    {
        int mask = 1 << (num - 1);
        int index = x / SQR_COUNT + y / SQR_COUNT * SQR_COUNT;
        _cols[x] = _cols[x] ^ mask;
        _rows[y] = _rows[y] ^ mask;
        _quar[index] = _quar[index] ^ mask;
    }

    int _FindNextNum(int x,int y, int value)
    {
        for (int i = value; i <= COL_COUNT; ++i)
        {
            if (_CheckFlag(x, y, i))
                return i;
        }
        return -1;
    }

    int[] _CloneData(int[] data)
    {
        var result = new int[data.Length];
        for (int i = 0; i < data.Length; ++i)
            result[i] = data[i];
        return result;
    }
}
