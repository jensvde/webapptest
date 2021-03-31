using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class TpPoint : MonoBehaviour, IComparable<TpPoint>
    {
        public int Id;
        public string Name;
        public Vector3 pos; // the object's position

        public int CompareTo(TpPoint tp)
        {
            if (Id < tp.Id) return -1;
            if (Id == tp.Id) return 0;
            return 1;
        }
    }
