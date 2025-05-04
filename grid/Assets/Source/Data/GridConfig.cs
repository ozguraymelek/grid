using UnityEngine;

namespace Source.Data
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Data/Grid/Config", order = 1)]
    public class GridConfig : ScriptableObject
    {
        public int Size;
    }
}