extern "C" void RegisterStaticallyLinkedModulesGranular()
{
	void RegisterModule_SharedInternals();
	RegisterModule_SharedInternals();

	void RegisterModule_Core();
	RegisterModule_Core();

	void RegisterModule_Animation();
	RegisterModule_Animation();

	void RegisterModule_Audio();
	RegisterModule_Audio();

	void RegisterModule_GameCenter();
	RegisterModule_GameCenter();

	void RegisterModule_IMGUI();
	RegisterModule_IMGUI();

	void RegisterModule_ImageConversion();
	RegisterModule_ImageConversion();

	void RegisterModule_Input();
	RegisterModule_Input();

	void RegisterModule_InputLegacy();
	RegisterModule_InputLegacy();

	void RegisterModule_JSONSerialize();
	RegisterModule_JSONSerialize();

	void RegisterModule_ParticleSystem();
	RegisterModule_ParticleSystem();

	void RegisterModule_Physics();
	RegisterModule_Physics();

	void RegisterModule_Physics2D();
	RegisterModule_Physics2D();

	void RegisterModule_RuntimeInitializeOnLoadManagerInitializer();
	RegisterModule_RuntimeInitializeOnLoadManagerInitializer();

	void RegisterModule_SpriteMask();
	RegisterModule_SpriteMask();

	void RegisterModule_Subsystems();
	RegisterModule_Subsystems();

	void RegisterModule_TextRendering();
	RegisterModule_TextRendering();

	void RegisterModule_TextCoreFontEngine();
	RegisterModule_TextCoreFontEngine();

	void RegisterModule_TextCoreTextEngine();
	RegisterModule_TextCoreTextEngine();

	void RegisterModule_UI();
	RegisterModule_UI();

	void RegisterModule_UIElements();
	RegisterModule_UIElements();

	void RegisterModule_VFX();
	RegisterModule_VFX();

	void RegisterModule_XR();
	RegisterModule_XR();

	void RegisterModule_VR();
	RegisterModule_VR();

}

template <typename T> void RegisterUnityClass(const char*);
template <typename T> void RegisterStrippedType(int, const char*, const char*);

void InvokeRegisterStaticallyLinkedModuleClasses()
{
	// Do nothing (we're in stripping mode)
}

class AnimationClip; template <> void RegisterUnityClass<AnimationClip>(const char*);
class Animator; template <> void RegisterUnityClass<Animator>(const char*);
class AnimatorController; template <> void RegisterUnityClass<AnimatorController>(const char*);
class AnimatorOverrideController; template <> void RegisterUnityClass<AnimatorOverrideController>(const char*);
class Motion; template <> void RegisterUnityClass<Motion>(const char*);
class RuntimeAnimatorController; template <> void RegisterUnityClass<RuntimeAnimatorController>(const char*);
class AudioBehaviour; template <> void RegisterUnityClass<AudioBehaviour>(const char*);
class AudioClip; template <> void RegisterUnityClass<AudioClip>(const char*);
class AudioListener; template <> void RegisterUnityClass<AudioListener>(const char*);
class AudioManager; template <> void RegisterUnityClass<AudioManager>(const char*);
class AudioMixer; template <> void RegisterUnityClass<AudioMixer>(const char*);
class AudioSource; template <> void RegisterUnityClass<AudioSource>(const char*);
class SampleClip; template <> void RegisterUnityClass<SampleClip>(const char*);
class Behaviour; template <> void RegisterUnityClass<Behaviour>(const char*);
class BuildSettings; template <> void RegisterUnityClass<BuildSettings>(const char*);
class Camera; template <> void RegisterUnityClass<Camera>(const char*);
namespace Unity { class Component; } template <> void RegisterUnityClass<Unity::Component>(const char*);
class ComputeShader; template <> void RegisterUnityClass<ComputeShader>(const char*);
class Cubemap; template <> void RegisterUnityClass<Cubemap>(const char*);
class CubemapArray; template <> void RegisterUnityClass<CubemapArray>(const char*);
class DelayedCallManager; template <> void RegisterUnityClass<DelayedCallManager>(const char*);
class EditorExtension; template <> void RegisterUnityClass<EditorExtension>(const char*);
class GameManager; template <> void RegisterUnityClass<GameManager>(const char*);
class GameObject; template <> void RegisterUnityClass<GameObject>(const char*);
class GlobalGameManager; template <> void RegisterUnityClass<GlobalGameManager>(const char*);
class GraphicsSettings; template <> void RegisterUnityClass<GraphicsSettings>(const char*);
class InputManager; template <> void RegisterUnityClass<InputManager>(const char*);
class LevelGameManager; template <> void RegisterUnityClass<LevelGameManager>(const char*);
class Light; template <> void RegisterUnityClass<Light>(const char*);
class LightProbes; template <> void RegisterUnityClass<LightProbes>(const char*);
class LightingSettings; template <> void RegisterUnityClass<LightingSettings>(const char*);
class LightmapSettings; template <> void RegisterUnityClass<LightmapSettings>(const char*);
class LineRenderer; template <> void RegisterUnityClass<LineRenderer>(const char*);
class LowerResBlitTexture; template <> void RegisterUnityClass<LowerResBlitTexture>(const char*);
class Material; template <> void RegisterUnityClass<Material>(const char*);
class Mesh; template <> void RegisterUnityClass<Mesh>(const char*);
class MeshFilter; template <> void RegisterUnityClass<MeshFilter>(const char*);
class MeshRenderer; template <> void RegisterUnityClass<MeshRenderer>(const char*);
class MonoBehaviour; template <> void RegisterUnityClass<MonoBehaviour>(const char*);
class MonoManager; template <> void RegisterUnityClass<MonoManager>(const char*);
class MonoScript; template <> void RegisterUnityClass<MonoScript>(const char*);
class NamedObject; template <> void RegisterUnityClass<NamedObject>(const char*);
class Object; template <> void RegisterUnityClass<Object>(const char*);
class PlayerSettings; template <> void RegisterUnityClass<PlayerSettings>(const char*);
class PreloadData; template <> void RegisterUnityClass<PreloadData>(const char*);
class QualitySettings; template <> void RegisterUnityClass<QualitySettings>(const char*);
namespace UI { class RectTransform; } template <> void RegisterUnityClass<UI::RectTransform>(const char*);
class ReflectionProbe; template <> void RegisterUnityClass<ReflectionProbe>(const char*);
class RenderSettings; template <> void RegisterUnityClass<RenderSettings>(const char*);
class RenderTexture; template <> void RegisterUnityClass<RenderTexture>(const char*);
class Renderer; template <> void RegisterUnityClass<Renderer>(const char*);
class ResourceManager; template <> void RegisterUnityClass<ResourceManager>(const char*);
class RuntimeInitializeOnLoadManager; template <> void RegisterUnityClass<RuntimeInitializeOnLoadManager>(const char*);
class Shader; template <> void RegisterUnityClass<Shader>(const char*);
class ShaderNameRegistry; template <> void RegisterUnityClass<ShaderNameRegistry>(const char*);
class Skybox; template <> void RegisterUnityClass<Skybox>(const char*);
class SortingGroup; template <> void RegisterUnityClass<SortingGroup>(const char*);
class Sprite; template <> void RegisterUnityClass<Sprite>(const char*);
class SpriteAtlas; template <> void RegisterUnityClass<SpriteAtlas>(const char*);
class SpriteRenderer; template <> void RegisterUnityClass<SpriteRenderer>(const char*);
class TagManager; template <> void RegisterUnityClass<TagManager>(const char*);
class TextAsset; template <> void RegisterUnityClass<TextAsset>(const char*);
class Texture; template <> void RegisterUnityClass<Texture>(const char*);
class Texture2D; template <> void RegisterUnityClass<Texture2D>(const char*);
class Texture2DArray; template <> void RegisterUnityClass<Texture2DArray>(const char*);
class Texture3D; template <> void RegisterUnityClass<Texture3D>(const char*);
class TimeManager; template <> void RegisterUnityClass<TimeManager>(const char*);
class TrailRenderer; template <> void RegisterUnityClass<TrailRenderer>(const char*);
class Transform; template <> void RegisterUnityClass<Transform>(const char*);
class ParticleSystem; template <> void RegisterUnityClass<ParticleSystem>(const char*);
class ParticleSystemRenderer; template <> void RegisterUnityClass<ParticleSystemRenderer>(const char*);
class Collider; template <> void RegisterUnityClass<Collider>(const char*);
class PhysicsManager; template <> void RegisterUnityClass<PhysicsManager>(const char*);
class Rigidbody; template <> void RegisterUnityClass<Rigidbody>(const char*);
class Collider2D; template <> void RegisterUnityClass<Collider2D>(const char*);
class Physics2DSettings; template <> void RegisterUnityClass<Physics2DSettings>(const char*);
class Rigidbody2D; template <> void RegisterUnityClass<Rigidbody2D>(const char*);
class SpriteMask; template <> void RegisterUnityClass<SpriteMask>(const char*);
namespace TextRendering { class Font; } template <> void RegisterUnityClass<TextRendering::Font>(const char*);
namespace UI { class Canvas; } template <> void RegisterUnityClass<UI::Canvas>(const char*);
namespace UI { class CanvasGroup; } template <> void RegisterUnityClass<UI::CanvasGroup>(const char*);
namespace UI { class CanvasRenderer; } template <> void RegisterUnityClass<UI::CanvasRenderer>(const char*);
class VFXManager; template <> void RegisterUnityClass<VFXManager>(const char*);
class VisualEffect; template <> void RegisterUnityClass<VisualEffect>(const char*);
class VisualEffectAsset; template <> void RegisterUnityClass<VisualEffectAsset>(const char*);
class VisualEffectObject; template <> void RegisterUnityClass<VisualEffectObject>(const char*);

void RegisterAllClasses()
{
void RegisterBuiltinTypes();
RegisterBuiltinTypes();
	//Total: 86 non stripped classes
	//0. AnimationClip
	RegisterUnityClass<AnimationClip>("Animation");
	//1. Animator
	RegisterUnityClass<Animator>("Animation");
	//2. AnimatorController
	RegisterUnityClass<AnimatorController>("Animation");
	//3. AnimatorOverrideController
	RegisterUnityClass<AnimatorOverrideController>("Animation");
	//4. Motion
	RegisterUnityClass<Motion>("Animation");
	//5. RuntimeAnimatorController
	RegisterUnityClass<RuntimeAnimatorController>("Animation");
	//6. AudioBehaviour
	RegisterUnityClass<AudioBehaviour>("Audio");
	//7. AudioClip
	RegisterUnityClass<AudioClip>("Audio");
	//8. AudioListener
	RegisterUnityClass<AudioListener>("Audio");
	//9. AudioManager
	RegisterUnityClass<AudioManager>("Audio");
	//10. AudioMixer
	RegisterUnityClass<AudioMixer>("Audio");
	//11. AudioSource
	RegisterUnityClass<AudioSource>("Audio");
	//12. SampleClip
	RegisterUnityClass<SampleClip>("Audio");
	//13. Behaviour
	RegisterUnityClass<Behaviour>("Core");
	//14. BuildSettings
	RegisterUnityClass<BuildSettings>("Core");
	//15. Camera
	RegisterUnityClass<Camera>("Core");
	//16. Component
	RegisterUnityClass<Unity::Component>("Core");
	//17. ComputeShader
	RegisterUnityClass<ComputeShader>("Core");
	//18. Cubemap
	RegisterUnityClass<Cubemap>("Core");
	//19. CubemapArray
	RegisterUnityClass<CubemapArray>("Core");
	//20. DelayedCallManager
	RegisterUnityClass<DelayedCallManager>("Core");
	//21. EditorExtension
	RegisterUnityClass<EditorExtension>("Core");
	//22. GameManager
	RegisterUnityClass<GameManager>("Core");
	//23. GameObject
	RegisterUnityClass<GameObject>("Core");
	//24. GlobalGameManager
	RegisterUnityClass<GlobalGameManager>("Core");
	//25. GraphicsSettings
	RegisterUnityClass<GraphicsSettings>("Core");
	//26. InputManager
	RegisterUnityClass<InputManager>("Core");
	//27. LevelGameManager
	RegisterUnityClass<LevelGameManager>("Core");
	//28. Light
	RegisterUnityClass<Light>("Core");
	//29. LightProbes
	RegisterUnityClass<LightProbes>("Core");
	//30. LightingSettings
	RegisterUnityClass<LightingSettings>("Core");
	//31. LightmapSettings
	RegisterUnityClass<LightmapSettings>("Core");
	//32. LineRenderer
	RegisterUnityClass<LineRenderer>("Core");
	//33. LowerResBlitTexture
	RegisterUnityClass<LowerResBlitTexture>("Core");
	//34. Material
	RegisterUnityClass<Material>("Core");
	//35. Mesh
	RegisterUnityClass<Mesh>("Core");
	//36. MeshFilter
	RegisterUnityClass<MeshFilter>("Core");
	//37. MeshRenderer
	RegisterUnityClass<MeshRenderer>("Core");
	//38. MonoBehaviour
	RegisterUnityClass<MonoBehaviour>("Core");
	//39. MonoManager
	RegisterUnityClass<MonoManager>("Core");
	//40. MonoScript
	RegisterUnityClass<MonoScript>("Core");
	//41. NamedObject
	RegisterUnityClass<NamedObject>("Core");
	//42. Object
	//Skipping Object
	//43. PlayerSettings
	RegisterUnityClass<PlayerSettings>("Core");
	//44. PreloadData
	RegisterUnityClass<PreloadData>("Core");
	//45. QualitySettings
	RegisterUnityClass<QualitySettings>("Core");
	//46. RectTransform
	RegisterUnityClass<UI::RectTransform>("Core");
	//47. ReflectionProbe
	RegisterUnityClass<ReflectionProbe>("Core");
	//48. RenderSettings
	RegisterUnityClass<RenderSettings>("Core");
	//49. RenderTexture
	RegisterUnityClass<RenderTexture>("Core");
	//50. Renderer
	RegisterUnityClass<Renderer>("Core");
	//51. ResourceManager
	RegisterUnityClass<ResourceManager>("Core");
	//52. RuntimeInitializeOnLoadManager
	RegisterUnityClass<RuntimeInitializeOnLoadManager>("Core");
	//53. Shader
	RegisterUnityClass<Shader>("Core");
	//54. ShaderNameRegistry
	RegisterUnityClass<ShaderNameRegistry>("Core");
	//55. Skybox
	RegisterUnityClass<Skybox>("Core");
	//56. SortingGroup
	RegisterUnityClass<SortingGroup>("Core");
	//57. Sprite
	RegisterUnityClass<Sprite>("Core");
	//58. SpriteAtlas
	RegisterUnityClass<SpriteAtlas>("Core");
	//59. SpriteRenderer
	RegisterUnityClass<SpriteRenderer>("Core");
	//60. TagManager
	RegisterUnityClass<TagManager>("Core");
	//61. TextAsset
	RegisterUnityClass<TextAsset>("Core");
	//62. Texture
	RegisterUnityClass<Texture>("Core");
	//63. Texture2D
	RegisterUnityClass<Texture2D>("Core");
	//64. Texture2DArray
	RegisterUnityClass<Texture2DArray>("Core");
	//65. Texture3D
	RegisterUnityClass<Texture3D>("Core");
	//66. TimeManager
	RegisterUnityClass<TimeManager>("Core");
	//67. TrailRenderer
	RegisterUnityClass<TrailRenderer>("Core");
	//68. Transform
	RegisterUnityClass<Transform>("Core");
	//69. ParticleSystem
	RegisterUnityClass<ParticleSystem>("ParticleSystem");
	//70. ParticleSystemRenderer
	RegisterUnityClass<ParticleSystemRenderer>("ParticleSystem");
	//71. Collider
	RegisterUnityClass<Collider>("Physics");
	//72. PhysicsManager
	RegisterUnityClass<PhysicsManager>("Physics");
	//73. Rigidbody
	RegisterUnityClass<Rigidbody>("Physics");
	//74. Collider2D
	RegisterUnityClass<Collider2D>("Physics2D");
	//75. Physics2DSettings
	RegisterUnityClass<Physics2DSettings>("Physics2D");
	//76. Rigidbody2D
	RegisterUnityClass<Rigidbody2D>("Physics2D");
	//77. SpriteMask
	RegisterUnityClass<SpriteMask>("SpriteMask");
	//78. Font
	RegisterUnityClass<TextRendering::Font>("TextRendering");
	//79. Canvas
	RegisterUnityClass<UI::Canvas>("UI");
	//80. CanvasGroup
	RegisterUnityClass<UI::CanvasGroup>("UI");
	//81. CanvasRenderer
	RegisterUnityClass<UI::CanvasRenderer>("UI");
	//82. VFXManager
	RegisterUnityClass<VFXManager>("VFX");
	//83. VisualEffect
	RegisterUnityClass<VisualEffect>("VFX");
	//84. VisualEffectAsset
	RegisterUnityClass<VisualEffectAsset>("VFX");
	//85. VisualEffectObject
	RegisterUnityClass<VisualEffectObject>("VFX");

}
