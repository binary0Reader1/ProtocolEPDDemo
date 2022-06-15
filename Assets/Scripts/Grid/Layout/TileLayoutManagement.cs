using Grid.TileFunctional;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Grid;

public class TileLayoutManagement : MonoBehaviour
{
    public Sprite[] CrunchedSpritesHandler => _crunchedSpritesHandler;
    [SerializeField] private Sprite[] _crunchedSpritesHandler;

    private HashSet<SpriteRenderer> _previousDisplayedRenderers = new HashSet<SpriteRenderer>();
    private Dictionary<SpriteRenderer, TileLayoutInfo> _currentDisplayedRenderers = new Dictionary<SpriteRenderer, TileLayoutInfo>();

    private TileLayoutLayer[] _layoutLayers = new TileLayoutLayer[]
    {
        new ModuleCantBePlacedLayoutLayer(),
        new ModuleCanBePlacedLayoutLayer(),
        new ElectrificatedLayoutLayer(),
        new PreElectrficationLayoutLayer()
    };

    public void SetTileLayout(TileLayoutType layoutType, Tile[] requestedTiles)
    {
        TileLayoutLayer requstedTileLayout = GetTileLayoutLayer(layoutType);
        requstedTileLayout.AssignedTilesSpriteRenderers.Clear();
        
        foreach(Tile tile in requestedTiles)
        {
            requstedTileLayout.AssignedTilesSpriteRenderers.Add(tile.TileLayoutReference.SpriteRendererReference);
        }

        CalculateDisplayedRenderers();
    }

    public void ClearTileLayout(TileLayoutType layoutType)
    {
        GetTileLayoutLayer(layoutType).AssignedTilesSpriteRenderers.Clear();

        CalculateDisplayedRenderers();
    }

    public void ClearAllTileLayouts()
    {
        foreach(TileLayoutLayer tileLayoutLayer in _layoutLayers)
        {
            tileLayoutLayer.AssignedTilesSpriteRenderers.Clear();
        }

        CalculateDisplayedRenderers();
    }

    private void Start()
    {
        _layoutLayers = (from layer in _layoutLayers
                         orderby layer.DisplayPriority
                         select layer).ToArray();

        foreach (TileLayoutLayer tileLayoutLayer in _layoutLayers)
        {
            tileLayoutLayer.Initialize();
        }
    }

    private TileLayoutLayer GetTileLayoutLayer(TileLayoutType layoutType)
    {
        foreach (TileLayoutLayer tileLayoutLayer in _layoutLayers)
        {
            if (tileLayoutLayer.AssignedTileLayoutType == layoutType) return tileLayoutLayer;
        }

        throw new NotImplementedException("Layer with requested type: " + layoutType.ToString() + " has not been initialized.");
    }

    private void CalculateDisplayedRenderers()
    {
        foreach(KeyValuePair<SpriteRenderer,TileLayoutInfo> keyValuePair in _currentDisplayedRenderers)
        {
            _previousDisplayedRenderers.Add(keyValuePair.Key);
        }

        _currentDisplayedRenderers.Clear();
        foreach(TileLayoutLayer tileLayoutLayer in _layoutLayers)
        {
            foreach(SpriteRenderer spriteRenderer in tileLayoutLayer.AssignedTilesSpriteRenderers)
            {
                if (_currentDisplayedRenderers.ContainsKey(spriteRenderer))
                {
                    _currentDisplayedRenderers[spriteRenderer] = tileLayoutLayer.AssignedTileLayoutInfo;
                    continue;
                }
                _currentDisplayedRenderers.Add(spriteRenderer, tileLayoutLayer.AssignedTileLayoutInfo);
            }
        }

        RefreshVisualization();
    }
    private void RefreshVisualization()
    {
        foreach(SpriteRenderer spriteRenderer in _previousDisplayedRenderers)
        {
            spriteRenderer.sprite = null;
            spriteRenderer.color = Color.white;
            spriteRenderer.size = new Vector2(1, 1);
        }

        foreach(KeyValuePair<SpriteRenderer, TileLayoutInfo> keyValuePair in _currentDisplayedRenderers)
        {
            keyValuePair.Key.sprite = keyValuePair.Value.Sprite;
            keyValuePair.Key.color = keyValuePair.Value.Color;
            keyValuePair.Key.size = new Vector2(1, 1);
        }
    }

}

public enum TileLayoutType
{
    CantBePlaced,
    CanBePlaced,
    Electrificated,
    PreElectrfication
}

public class TileLayoutInfo
{
    public Sprite Sprite { get; private set; }
    public Color Color { get; private set; }
    public TileLayoutInfo(Sprite sprite, Color color)
    {
        Sprite = sprite;
        Color = color;
    }
}

public abstract class TileLayoutLayer
{
    public abstract TileLayoutType AssignedTileLayoutType { get; protected set; }
    public abstract int DisplayPriority { get; protected set; }
    public HashSet<SpriteRenderer> AssignedTilesSpriteRenderers { get; private set; } = new HashSet<SpriteRenderer>();
    public TileLayoutInfo AssignedTileLayoutInfo { get; private set; }

    private bool _isInitialized = false; 
    public void Initialize()
    {
        if (_isInitialized) return;
        TileLayoutInfo formedTileLayoutInfo = FormTileLayoutInfoInstruction();
        if (formedTileLayoutInfo is null) throw new NotImplementedException(GetType().Name + " does not have an TileInfo instruction.");

        AssignedTileLayoutInfo = formedTileLayoutInfo;

        _isInitialized = true;
    }
    public abstract TileLayoutInfo FormTileLayoutInfoInstruction();
}

public sealed class ModuleCantBePlacedLayoutLayer : TileLayoutLayer
{
    public override TileLayoutType AssignedTileLayoutType { get; protected set; } = TileLayoutType.CantBePlaced;
    public override int DisplayPriority { get; protected set; } = 1000;

    public override TileLayoutInfo FormTileLayoutInfoInstruction()
    {
        return new TileLayoutInfo(UnityEngine.Object.FindObjectOfType<TileLayoutManagement>().CrunchedSpritesHandler[0], Color.white);
    }
}

public sealed class ModuleCanBePlacedLayoutLayer : TileLayoutLayer
{
    public override TileLayoutType AssignedTileLayoutType { get; protected set; } = TileLayoutType.CanBePlaced;
    public override int DisplayPriority { get; protected set; } = 999;

    public override TileLayoutInfo FormTileLayoutInfoInstruction()
    {
        return new TileLayoutInfo(UnityEngine.Object.FindObjectOfType<TileLayoutManagement>().CrunchedSpritesHandler[1], Color.white);
    }
}

public sealed class ElectrificatedLayoutLayer : TileLayoutLayer
{
    public override TileLayoutType AssignedTileLayoutType { get; protected set; } = TileLayoutType.Electrificated;
    public override int DisplayPriority { get; protected set; } = 11;

    public override TileLayoutInfo FormTileLayoutInfoInstruction()
    {
        return new TileLayoutInfo(UnityEngine.Object.FindObjectOfType<TileLayoutManagement>().CrunchedSpritesHandler[2], Color.white);
    }
}

public sealed class PreElectrficationLayoutLayer : TileLayoutLayer
{
    public override TileLayoutType AssignedTileLayoutType { get; protected set; } = TileLayoutType.PreElectrfication;
    public override int DisplayPriority { get; protected set; } = 10;

    public override TileLayoutInfo FormTileLayoutInfoInstruction()
    {
        return new TileLayoutInfo(UnityEngine.Object.FindObjectOfType<TileLayoutManagement>().CrunchedSpritesHandler[2], Color.gray);
    }
}



