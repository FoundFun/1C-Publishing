using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Factory
{
    public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        private IObjectResolver _objectResolver;
        
        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        protected T Create(T prefab, Vector3 position, Quaternion rotation)
        {
            return _objectResolver.Instantiate(prefab, position, rotation);
        }
    }
}