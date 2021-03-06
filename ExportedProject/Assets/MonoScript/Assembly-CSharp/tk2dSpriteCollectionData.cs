using System;
using System.Collections.Generic;
using tk2dRuntime;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Backend/tk2dSpriteCollectionData")]
public class tk2dSpriteCollectionData : MonoBehaviour
{
	public const int CURRENT_VERSION = 3;

	public int version;

	public bool materialIdsValid;

	public bool needMaterialInstance;

	public tk2dSpriteDefinition[] spriteDefinitions;

	[SerializeField]
	public List<int> SpriteIDsWithBagelColliders = new List<int>();

	[SerializeField]
	public List<BagelColliderData> SpriteDefinedBagelColliders = new List<BagelColliderData>();

	[SerializeField]
	public List<int> SpriteIDsWithAttachPoints = new List<int>();

	[SerializeField]
	public List<AttachPointData> SpriteDefinedAttachPoints = new List<AttachPointData>();

	[SerializeField]
	public List<int> SpriteIDsWithNeighborDependencies = new List<int>();

	[SerializeField]
	public List<NeighborDependencyData> SpriteDefinedIndexNeighborDependencies = new List<NeighborDependencyData>();

	[SerializeField]
	public List<int> SpriteIDsWithAnimationSequences = new List<int>();

	[SerializeField]
	public List<SimpleTilesetAnimationSequence> SpriteDefinedAnimationSequences = new List<SimpleTilesetAnimationSequence>();

	private Dictionary<string, int> spriteNameLookupDict;

	public bool premultipliedAlpha;

	public bool shouldGenerateTilemapReflectionData;

	public Material material;

	public Material[] materials;

	[NonSerialized]
	public Material[] materialInsts;

	[NonSerialized]
	public Texture2D[] textureInsts = new Texture2D[0];

	public Texture[] textures;

	public TextAsset[] pngTextures = new TextAsset[0];

	public int[] materialPngTextureId = new int[0];

	public FilterMode textureFilterMode = FilterMode.Bilinear;

	public bool textureMipMaps;

	public bool allowMultipleAtlases;

	public string spriteCollectionGUID;

	public string spriteCollectionName;

	public string assetName = string.Empty;

	public bool loadable;

	public float invOrthoSize = 1f;

	public float halfTargetHeight = 1f;

	public int buildKey;

	public string dataGuid = string.Empty;

	public bool managedSpriteCollection;

	public bool hasPlatformData;

	public string[] spriteCollectionPlatforms;

	public string[] spriteCollectionPlatformGUIDs;

	private tk2dSpriteCollectionData platformSpecificData;

	public bool Transient { get; set; }

	public int Count
	{
		get
		{
			return inst.spriteDefinitions.Length;
		}
	}

	public tk2dSpriteDefinition FirstValidDefinition
	{
		get
		{
			tk2dSpriteDefinition[] array = inst.spriteDefinitions;
			foreach (tk2dSpriteDefinition tk2dSpriteDefinition2 in array)
			{
				if (tk2dSpriteDefinition2.Valid)
				{
					return tk2dSpriteDefinition2;
				}
			}
			return null;
		}
	}

	public int FirstValidDefinitionIndex
	{
		get
		{
			tk2dSpriteCollectionData tk2dSpriteCollectionData2 = inst;
			for (int i = 0; i < tk2dSpriteCollectionData2.spriteDefinitions.Length; i++)
			{
				if (tk2dSpriteCollectionData2.spriteDefinitions[i].Valid)
				{
					return i;
				}
			}
			return -1;
		}
	}

	public tk2dSpriteCollectionData inst
	{
		get
		{
			if (platformSpecificData == null)
			{
				if (hasPlatformData)
				{
					string currentPlatform = tk2dSystem.CurrentPlatform;
					string text = string.Empty;
					for (int i = 0; i < spriteCollectionPlatforms.Length; i++)
					{
						if (spriteCollectionPlatforms[i] == currentPlatform)
						{
							text = spriteCollectionPlatformGUIDs[i];
							break;
						}
					}
					if (text.Length == 0)
					{
						text = spriteCollectionPlatformGUIDs[0];
					}
					platformSpecificData = tk2dSystem.LoadResourceByGUID<tk2dSpriteCollectionData>(text);
				}
				else
				{
					platformSpecificData = this;
				}
			}
			platformSpecificData.Init();
			return platformSpecificData;
		}
	}

	public BagelCollider[] GetBagelColliders(int spriteId)
	{
		int num = SpriteIDsWithBagelColliders.IndexOf(spriteId);
		if (num >= 0)
		{
			return SpriteDefinedBagelColliders[num].bagelColliders;
		}
		return null;
	}

	public void SetBagelColliders(int spriteId, BagelCollider[] bcs)
	{
		if (bcs == null || bcs.Length == 0)
		{
			int num = SpriteIDsWithBagelColliders.IndexOf(spriteId);
			if (num >= 0)
			{
				SpriteIDsWithBagelColliders.RemoveAt(num);
				SpriteDefinedBagelColliders.RemoveAt(num);
			}
		}
		else if (SpriteIDsWithBagelColliders.Contains(spriteId))
		{
			SpriteDefinedBagelColliders[SpriteIDsWithBagelColliders.IndexOf(spriteId)] = new BagelColliderData(bcs);
		}
		else
		{
			SpriteIDsWithBagelColliders.Add(spriteId);
			SpriteDefinedBagelColliders.Add(new BagelColliderData(bcs));
		}
	}

	public tk2dSpriteDefinition.AttachPoint[] GetAttachPoints(int spriteId)
	{
		int num = SpriteIDsWithAttachPoints.IndexOf(spriteId);
		if (num >= 0)
		{
			return SpriteDefinedAttachPoints[num].attachPoints;
		}
		return null;
	}

	public void ClearAttachPoints(int spriteId)
	{
		int num = SpriteIDsWithAttachPoints.IndexOf(spriteId);
		if (num >= 0)
		{
			SpriteIDsWithAttachPoints.RemoveAt(num);
			SpriteDefinedAttachPoints.RemoveAt(num);
		}
	}

	public void SetAttachPoints(int spriteId, tk2dSpriteDefinition.AttachPoint[] aps)
	{
		if (aps == null || aps.Length == 0)
		{
			ClearAttachPoints(spriteId);
			return;
		}
		if (SpriteIDsWithAttachPoints.Contains(spriteId))
		{
			SpriteDefinedAttachPoints[SpriteIDsWithAttachPoints.IndexOf(spriteId)] = new AttachPointData(aps);
			return;
		}
		SpriteIDsWithAttachPoints.Add(spriteId);
		SpriteDefinedAttachPoints.Add(new AttachPointData(aps));
	}

	public void ClearDependencies(int spriteId)
	{
		int num = SpriteIDsWithNeighborDependencies.IndexOf(spriteId);
		if (num >= 0)
		{
			SpriteIDsWithNeighborDependencies.RemoveAt(num);
			SpriteDefinedIndexNeighborDependencies.RemoveAt(num);
		}
	}

	public List<IndexNeighborDependency> NewDependencies(int spriteId)
	{
		List<IndexNeighborDependency> dependencies = GetDependencies(spriteId);
		if (dependencies != null)
		{
			return dependencies;
		}
		List<IndexNeighborDependency> list = new List<IndexNeighborDependency>();
		SpriteIDsWithNeighborDependencies.Add(spriteId);
		SpriteDefinedIndexNeighborDependencies.Add(new NeighborDependencyData(list));
		return list;
	}

	public List<IndexNeighborDependency> GetDependencies(int spriteId)
	{
		int num = SpriteIDsWithNeighborDependencies.IndexOf(spriteId);
		if (num >= 0)
		{
			return SpriteDefinedIndexNeighborDependencies[num].neighborDependencies;
		}
		return null;
	}

	public SimpleTilesetAnimationSequence NewAnimationSequence(int spriteId)
	{
		SimpleTilesetAnimationSequence animationSequence = GetAnimationSequence(spriteId);
		if (animationSequence != null)
		{
			return animationSequence;
		}
		SimpleTilesetAnimationSequence simpleTilesetAnimationSequence = new SimpleTilesetAnimationSequence();
		SpriteIDsWithAnimationSequences.Add(spriteId);
		SpriteDefinedAnimationSequences.Add(simpleTilesetAnimationSequence);
		return simpleTilesetAnimationSequence;
	}

	public SimpleTilesetAnimationSequence GetAnimationSequence(int spriteId)
	{
		int num = SpriteIDsWithAnimationSequences.IndexOf(spriteId);
		if (num >= 0)
		{
			return SpriteDefinedAnimationSequences[num];
		}
		return null;
	}

	public int GetSpriteIdByName(string name)
	{
		return GetSpriteIdByName(name, 0);
	}

	public int GetSpriteIdByName(string name, int defaultValue)
	{
		inst.InitDictionary();
		int value = defaultValue;
		if (!inst.spriteNameLookupDict.TryGetValue(name, out value))
		{
			return defaultValue;
		}
		return value;
	}

	public tk2dSpriteDefinition GetSpriteDefinition(string name)
	{
		int spriteIdByName = GetSpriteIdByName(name, -1);
		if (spriteIdByName == -1)
		{
			return null;
		}
		return spriteDefinitions[spriteIdByName];
	}

	public void InitDictionary()
	{
		if (spriteNameLookupDict == null)
		{
			spriteNameLookupDict = new Dictionary<string, int>(spriteDefinitions.Length);
			for (int i = 0; i < spriteDefinitions.Length; i++)
			{
				spriteNameLookupDict[spriteDefinitions[i].name] = i;
			}
		}
	}

	public bool IsValidSpriteId(int id)
	{
		if (id < 0 || id >= inst.spriteDefinitions.Length)
		{
			return false;
		}
		return inst.spriteDefinitions[id].Valid;
	}

	public void InitMaterialIds()
	{
		if (inst.materialIdsValid)
		{
			return;
		}
		int num = -1;
		Dictionary<Material, int> dictionary = new Dictionary<Material, int>();
		for (int i = 0; i < inst.materials.Length; i++)
		{
			if (num == -1 && inst.materials[i] != null)
			{
				num = i;
			}
			dictionary[materials[i]] = i;
		}
		if (num == -1)
		{
			Debug.LogError("Init material ids failed.");
			return;
		}
		tk2dSpriteDefinition[] array = inst.spriteDefinitions;
		foreach (tk2dSpriteDefinition tk2dSpriteDefinition2 in array)
		{
			if (!dictionary.TryGetValue(tk2dSpriteDefinition2.material, out tk2dSpriteDefinition2.materialId))
			{
				tk2dSpriteDefinition2.materialId = num;
			}
		}
		inst.materialIdsValid = true;
	}

	public List<Tuple<int, TilesetIndexMetadata>> GetIndicesForTileType(TilesetIndexMetadata.TilesetFlagType flagType)
	{
		if (spriteDefinitions == null)
		{
			return null;
		}
		List<Tuple<int, TilesetIndexMetadata>> list = new List<Tuple<int, TilesetIndexMetadata>>();
		for (int i = 0; i < spriteDefinitions.Length; i++)
		{
			if (spriteDefinitions[i].metadata != null && (spriteDefinitions[i].metadata.type & flagType) != 0)
			{
				Tuple<int, TilesetIndexMetadata> item = new Tuple<int, TilesetIndexMetadata>(i, spriteDefinitions[i].metadata);
				list.Add(item);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list;
	}

	private void Init()
	{
		if (materialInsts != null)
		{
			return;
		}
		if (spriteDefinitions == null)
		{
			spriteDefinitions = new tk2dSpriteDefinition[0];
		}
		if (materials == null)
		{
			materials = new Material[0];
		}
		materialInsts = new Material[materials.Length];
		if (needMaterialInstance)
		{
			if (tk2dSystem.OverrideBuildMaterial)
			{
				for (int i = 0; i < materials.Length; i++)
				{
					materialInsts[i] = new Material(Shader.Find("tk2d/BlendVertexColor"));
				}
			}
			else
			{
				bool flag = false;
				if (pngTextures.Length > 0)
				{
					flag = true;
					textureInsts = new Texture2D[pngTextures.Length];
					for (int j = 0; j < pngTextures.Length; j++)
					{
						Texture2D texture2D = new Texture2D(4, 4, TextureFormat.ARGB32, textureMipMaps);
						texture2D.LoadImage(pngTextures[j].bytes);
						textureInsts[j] = texture2D;
						texture2D.filterMode = textureFilterMode;
					}
				}
				for (int k = 0; k < materials.Length; k++)
				{
					materialInsts[k] = UnityEngine.Object.Instantiate(materials[k]);
					if (flag)
					{
						int num = ((materialPngTextureId.Length != 0) ? materialPngTextureId[k] : 0);
						materialInsts[k].mainTexture = textureInsts[num];
					}
				}
			}
			for (int l = 0; l < spriteDefinitions.Length; l++)
			{
				tk2dSpriteDefinition tk2dSpriteDefinition2 = spriteDefinitions[l];
				tk2dSpriteDefinition2.materialInst = materialInsts[tk2dSpriteDefinition2.materialId];
			}
		}
		else
		{
			for (int m = 0; m < materials.Length; m++)
			{
				materialInsts[m] = materials[m];
			}
			for (int n = 0; n < spriteDefinitions.Length; n++)
			{
				tk2dSpriteDefinition tk2dSpriteDefinition3 = spriteDefinitions[n];
				tk2dSpriteDefinition3.materialInst = tk2dSpriteDefinition3.material;
			}
		}
		tk2dEditorSpriteDataUnloader.Register(this);
	}

	public static tk2dSpriteCollectionData CreateFromTexture(Texture texture, tk2dSpriteCollectionSize size, string[] names, Rect[] regions, Vector2[] anchors)
	{
		return SpriteCollectionGenerator.CreateFromTexture(texture, size, names, regions, anchors);
	}

	public static tk2dSpriteCollectionData CreateFromTexturePacker(tk2dSpriteCollectionSize size, string texturePackerData, Texture texture)
	{
		return SpriteCollectionGenerator.CreateFromTexturePacker(size, texturePackerData, texture);
	}

	public void ResetPlatformData()
	{
		tk2dEditorSpriteDataUnloader.Unregister(this);
		if (platformSpecificData != null)
		{
			platformSpecificData.DestroyTextureInsts();
		}
		DestroyTextureInsts();
		if ((bool)platformSpecificData)
		{
			platformSpecificData = null;
		}
		materialInsts = null;
	}

	private void DestroyTextureInsts()
	{
		Texture2D[] array = textureInsts;
		foreach (Texture2D obj in array)
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
		textureInsts = new Texture2D[0];
	}

	public void UnloadTextures()
	{
		tk2dSpriteCollectionData tk2dSpriteCollectionData2 = inst;
		Texture[] array = tk2dSpriteCollectionData2.textures;
		for (int i = 0; i < array.Length; i++)
		{
			Texture2D assetToUnload = (Texture2D)array[i];
			Resources.UnloadAsset(assetToUnload);
		}
		tk2dSpriteCollectionData2.DestroyTextureInsts();
	}

	private void OnDestroy()
	{
		if (Transient)
		{
			Material[] array = materials;
			foreach (Material obj in array)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
		else if (needMaterialInstance)
		{
			Material[] array2 = materialInsts;
			foreach (Material obj2 in array2)
			{
				UnityEngine.Object.DestroyImmediate(obj2);
			}
			materialInsts = new Material[0];
			Texture2D[] array3 = textureInsts;
			foreach (Texture2D obj3 in array3)
			{
				UnityEngine.Object.DestroyImmediate(obj3);
			}
			textureInsts = new Texture2D[0];
		}
		ResetPlatformData();
	}
}
