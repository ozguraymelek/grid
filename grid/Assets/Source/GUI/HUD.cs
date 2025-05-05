using System;
using UnityEngine;
using System.Collections.Generic;
using Source.Core.Utils;
using Source.Data;
using Source.Grid;
using Source.Interfaces;
using Zenject;

namespace Source.GUI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private float _guiScale = 1f;
        private IGridBuilder _builder;
        private GridConfig _config;
        private string _input;
        
        [Inject]
        public void Construct(IGridBuilder builder, GridConfig config)
        {
            _builder = builder;
            _config = config;

            SLog.InjectionStatus(this,
                (nameof(_builder), _builder),
                (nameof(_config), _config)
            );
        }

        private void Awake() => _input = _config.Size.ToString();

        private void OnGUI()
        {
            UnityEngine.GUI.matrix = Matrix4x4.Scale(Vector3.one * _guiScale);
            UnityEngine.GUI.Label(new Rect(20,20,100,50), "Size:");
            _input = UnityEngine.GUI.TextField(new Rect(120,20,100,50), _input);
            if (UnityEngine.GUI.Button(new Rect(240,20,100,50), "Rebuild"))
            {
                if (int.TryParse(_input, out int s) && s>0)
                {
                    _config.Size = s;
                    _builder.Regenerate();
                }
                else _input = _config.Size.ToString();
            }
        }
        
        // [Inject]
        // public void Construct(IBuilder builder, GridConfig gridConfig)
        // {
        //     _builder = builder;
        //     this.gridConfig = gridConfig;
        //
        //     SLog.InjectionStatus(this,
        //         (nameof(_builder), _builder),
        //         (nameof(this.gridConfig), this.gridConfig)
        //     );
        // }
        //
        // private void Awake()
        // {
        //     _gridSizeInput = gridConfig.Size.ToString();
        // }
        //
        // private void OnGUI()
        // {
        //     UnityEngine.GUI.matrix = Matrix4x4.Scale(Vector3.one * guiScale);
        //
        //     var gridSizeInputFieldStyle = new GUIStyle(UnityEngine.GUI.skin.textField)
        //     {
        //         fontSize = 50,
        //         padding = new RectOffset(15, 18, 6, 6),
        //         alignment = TextAnchor.MiddleCenter
        //     };
        //
        //     var rebuildButtonStyle = new GUIStyle(UnityEngine.GUI.skin.button)
        //     {
        //         fontSize = 50,
        //         padding = new RectOffset(15, 18, 6, 6)
        //     };
        //
        //     var gridSizeRect  = new Rect(20, 120, 300, 100);
        //     var rebuildButtonRect = new Rect(350, 20, 560, 100);
        //
        //     UnityEngine.GUI.Label(new Rect(20, 20, 300, 100), "N x N", gridSizeInputFieldStyle);
        //
        //     _gridSizeInput = UnityEngine.GUI.TextField(gridSizeRect, _gridSizeInput, 3, gridSizeInputFieldStyle);
        //
        //     if (UnityEngine.GUI.Button(rebuildButtonRect, "Rebuild", rebuildButtonStyle))
        //     {
        //         if (int.TryParse(_gridSizeInput, out int newSize) && newSize > 0)
        //         {
        //             gridConfig.Size = newSize;
        //             _builder.Regenerate();
        //         }
        //         else
        //         {
        //             _gridSizeInput = gridConfig.Size.ToString();
        //         }
        //     }
        //
        // }
    }
}