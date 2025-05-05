using UnityEngine;
using System.Linq;
using Source.Core.Utils;
using Source.Data;
using Source.Interfaces;
using Zenject;

namespace Source.GUI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private float guiScale = 1f;
        private IGridBuilder _builder;
        private IGridProvider _provider;
        private GridConfig _config;
        private string _input;
        
        [Inject]
        public void Construct(IGridBuilder builder, IGridProvider provider, GridConfig config)
        {
            _builder = builder;
            _provider = provider;
            _config = config;

            SLog.InjectionStatus(this,
                (nameof(_builder), _builder),
                (nameof(_provider), _provider),
                (nameof(_config), _config)
            );
        }

        private void Awake() => _input = _config.Size.ToString();

        private void OnGUI()
        {
            UnityEngine.GUI.matrix = Matrix4x4.Scale(Vector3.one * guiScale);

            // styles
            var fieldStyle = new GUIStyle(UnityEngine.GUI.skin.textField)
            {
                fontSize = 50,
                padding = new RectOffset(15, 18, 6, 6),
                alignment = TextAnchor.MiddleCenter
            };
            
            var labelStyle = new GUIStyle(UnityEngine.GUI.skin.textField)
            {
                fontSize = 50,
                padding = new RectOffset(15, 18, 6, 6),
                alignment = TextAnchor.MiddleCenter
            };
            
            var regenerateButtonStyle = new GUIStyle(UnityEngine.GUI.skin.button)
            {
                fontSize = 50,
                padding = new RectOffset(15, 18, 6, 6)
            };
            ///////////
            
            // rects
            var gridSizeRect  = new Rect(20, 120, 300, 100);
            var sizeRect = new Rect(20, 20, 300, 100);
            var regenerateButtonRect = new Rect(350, 20, 500, 100);
            var matchCountRect = new Rect(350, 130, 500, 100);
            ////////
            
            UnityEngine.GUI.Label(sizeRect, "N x N", fieldStyle);
            
            var rawInput = UnityEngine.GUI.TextField(gridSizeRect, _input, _config.MaxSizeDigit, fieldStyle);

            InputRestriction(rawInput, out _input);
            
            if (UnityEngine.GUI.Button(regenerateButtonRect, "Regenerate", regenerateButtonStyle))
            {
                if (int.TryParse(_input, out int newSize) && newSize > 0)
                {
                    _config.Size = newSize;
                    _builder.Regenerate();
                }
                else
                {
                    _input = _config.Size.ToString();
                }
            }
            
            UnityEngine.GUI.Label(matchCountRect, $"Match Count: {_provider.MatchCount.ToString()}", labelStyle);
        }

        // ignores all input except digits 0-9
        private void InputRestriction(string rawInput, out string processedInput)
        {
            processedInput = new string(rawInput.Where(char.IsDigit).ToArray());
        }
    }
}