using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern
{
    public abstract class PooledObject : MonoBehaviour
    {
        public ObjectPool ObjPool { get; private set; }

        public void PooledInit(ObjectPool objPool)  // 생성할때 자기자신을 넣어줄 수 있도록 public
        {
            ObjPool = objPool;
        }

        public void ReturnPool()
        {
            ObjPool.PushPool(this);
        }

    }
}

