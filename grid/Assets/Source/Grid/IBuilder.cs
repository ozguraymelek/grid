using UnityEngine;

namespace Source.Grid
{
    public interface IBuilder
    {
        void Generate();
        void Regenerate();
        void Clear();
    }
}
