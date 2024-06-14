using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly int _initialAmount;
        private readonly List<T> _pooledObjects;
        
        public ObjectPool(T prefab, int initialAmount)
        {
            _prefab = prefab;
            _initialAmount = initialAmount;
            _pooledObjects = new List<T>();
            CreatePooledObjects();
        }
        
        private void CreatePooledObjects()
        {
            for (var i = 0; i < _initialAmount; i++)
            {
                var obj = Object.Instantiate(_prefab);
                obj.gameObject.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }
        
        public T GetPooledObject()
        {
            return _pooledObjects.FirstOrDefault(obj => !obj.gameObject.activeInHierarchy);
        }
        
        public void ResetPool()
        {
            foreach (var obj in _pooledObjects)
            {
                if (obj != null)
                {
                    Object.Destroy(obj.gameObject);
                }
            }
            _pooledObjects.Clear();
            CreatePooledObjects();
        }
    }
}