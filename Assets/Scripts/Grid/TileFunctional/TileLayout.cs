using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid.TileFunctional
{
    public class TileLayout : MonoBehaviour
    {
        public SpriteRenderer SpriteRendererReference => _spriteRenderer;
        [SerializeField] private SpriteRenderer _spriteRenderer;
    }
}
