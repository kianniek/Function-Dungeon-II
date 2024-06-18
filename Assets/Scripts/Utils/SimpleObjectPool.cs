using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class SimpleObjectPool<T> 
        where T : Behaviour
    {
        private readonly List<T> _pooledObjects = new();
        
        private readonly T _prefab;
        private readonly int _initialAmount;
        private readonly Transform _parent;
        
        public SimpleObjectPool(T prefab, int initialAmount, Transform parent)
        {
            _prefab = prefab;
            _initialAmount = initialAmount;
            _parent = parent;
            
            CreatePooledObjects();
        }
        
        public SimpleObjectPool(T prefab, int initialAmount)
        {
            _prefab = prefab;
            _initialAmount = initialAmount;
            
            CreatePooledObjects();
        }
        
        private void CreatePooledObjects()
        {
            for (var i = 0; i < _initialAmount; i++)
            {
                var obj = _parent ? Object.Instantiate(_prefab, _parent) : Object.Instantiate(_prefab);
                
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
            foreach (var obj in _pooledObjects.Where(obj => obj)) 
                Object.Destroy(obj.gameObject);
            
            _pooledObjects.Clear();
            
            CreatePooledObjects();
        }
    }
}