using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyVal<TKey, TVal>
{
    public TKey Key { get; set; }
    public TVal Value { get; set; }

    public KeyVal() { }

    public KeyVal(TKey key, TVal val)
    {
        this.Key = key;
        this.Value = val;
    }
}