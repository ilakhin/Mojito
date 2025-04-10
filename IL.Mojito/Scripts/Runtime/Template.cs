using System;
using UnityEngine;

namespace IL.Mojito
{
    // TODO: Обернуть в контейнер?
    [Serializable]
    internal sealed class Template<TBinder>
        where TBinder : BaseBinder
    {
        [SerializeField]
        private string _key;

        [SerializeField]
        private TBinder _binder;

        public string Key => _key;

        public TBinder Binder => _binder;
    }
}
