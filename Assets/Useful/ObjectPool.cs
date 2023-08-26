// 日本語対応
using System;
using System.Collections.Generic;

public static class ObjectPool<T> where T : class
{
    private static readonly Stack<T> _pool = new Stack<T>();

    /// <summary>
    /// オブジェクトを取得する
    /// </summary>
    /// <returns></returns>
    public static T AcquireFromPool()
    {
        if (_pool.Count == 0)
            return CreateInstance();
        else
            return _pool.Pop();
    }
    /// <summary>
    /// オブジェクトをプールに返す
    /// </summary>
    /// <param name="obj"></param>
    public static void ReturnToPool(T obj)
    {
        _pool.Push(obj);
    }

    private static T CreateInstance()
    {
        return Activator.CreateInstance<T>();
    }
}