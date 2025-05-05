using UnityEngine;

namespace Source.Data
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Data/Grid/Config", order = 1)]
    public class GridConfig : ScriptableObject
    {
        public int Size;
        
        public int MaxSizeDigit;
        public int sizeDigit;
        public int SizeDigit
        {
            get => sizeDigit;
            set
            {
                if (value < Mathf.Pow(10, MaxSizeDigit - 1) || value >= Mathf.Pow(10, MaxSizeDigit))
                {
                    Debug.LogWarning($"Digit must be {MaxSizeDigit}-digit number");
                    return;
                }

                sizeDigit = value;
            }
        }
        
    }
}