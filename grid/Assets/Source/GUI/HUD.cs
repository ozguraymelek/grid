using System;
using UnityEngine;
using System.Collections.Generic;
using Source.Core.Utils;
using Source.Data;
using Source.Grid;
using Zenject;

namespace Source.GUI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private float guiScale;    
        private string _gridSizeInput;

        [SerializeField] private GridConfig gridConfig;
        private IBuilder _builder;
        
        [Inject]
        public void Construct(IBuilder builder, GridConfig gridConfig)
        {
            _builder = builder;
            this.gridConfig = gridConfig;

            SLog.InjectionStatus(this,
                (nameof(_builder), _builder),
                (nameof(this.gridConfig), this.gridConfig)
            );
        }
        
        private void Awake()
        {
            _gridSizeInput = gridConfig.Size.ToString();
        }

        private void OnGUI()
        {
            UnityEngine.GUI.matrix = Matrix4x4.Scale(Vector3.one * guiScale);

            var gridSizeInputFieldStyle = new GUIStyle(UnityEngine.GUI.skin.textField)
            {
                fontSize = 50,
                padding = new RectOffset(15, 18, 6, 6),
                alignment = TextAnchor.MiddleCenter
            };

            var rebuildButtonStyle = new GUIStyle(UnityEngine.GUI.skin.button)
            {
                fontSize = 50,
                padding = new RectOffset(15, 18, 6, 6)
            };

            var gridSizeRect  = new Rect(20, 120, 300, 100);
            var rebuildButtonRect = new Rect(350, 20, 560, 100);

            UnityEngine.GUI.Label(new Rect(20, 20, 300, 100), "N x N", gridSizeInputFieldStyle);

            _gridSizeInput = UnityEngine.GUI.TextField(gridSizeRect, _gridSizeInput, 3, gridSizeInputFieldStyle);

            if (UnityEngine.GUI.Button(rebuildButtonRect, "Rebuild", rebuildButtonStyle))
            {
                if (int.TryParse(_gridSizeInput, out int newSize) && newSize > 0)
                {
                    gridConfig.Size = newSize;
                    _builder.Regenerate();
                }
                else
                {
                    _gridSizeInput = gridConfig.Size.ToString();
                }
            }

        }
    }
}