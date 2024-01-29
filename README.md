# Standard Animations
*Version: 1.2.6*
## Description: 
A library for various coded animations.
## Use Cases: 
* Quickly add simple animations to GameObjects and UI elements.
## Directions for Use: 
All of the key components can be used out-of-the box, and each one is pretty straightforward to set up. One thing to note, StandardAnimation is an abstract class with methods Enter(), EnterImmediate(), Exit(), and ExitImmediate(). Drawer, CanvasGroupAnimator, and ScaleAnimator all inherit from StandardAnimation. StandardAnimationGroup is a simple component that can be used to group and control multiple StandardAnimations.
## Package Mirrors: 
[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODg3LnBuZw==/original/npRUfq.png'>](https://github.com/Iron-Mountain-Software/standard-animations.git)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODkyLnBuZw==/original/Fq0ORM.png'>](https://www.npmjs.com/package/com.iron-mountain.standard-animations)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODk4LnBuZw==/original/Rv4m96.png'>](https://iron-mountain.itch.io/standard-animations)
---
## Key Scripts & Components: 
1. public abstract class **StandardAnimation** : MonoBehaviour
   * Properties: 
      * public float ***Seconds***  { get; }
   * Methods: 
      * public abstract void ***Enter***()
      * public abstract void ***Enter***(Action onComplete)
      * public abstract void ***Enter***(float animationSeconds, Action onComplete)
      * public abstract void ***EnterImmediate***()
      * public abstract void ***EnterImmediate***(Action onComplete)
      * public abstract void ***Exit***()
      * public abstract void ***Exit***(Action onComplete)
      * public abstract void ***Exit***(float animationSeconds, Action onComplete)
      * public abstract void ***ExitImmediate***()
      * public abstract void ***ExitImmediate***(Action onComplete)
1. public class **StandardAnimationGroup** : MonoBehaviour
   * Properties: 
      * public StandardAnimation[] ***StandardAnimations***  { get; }
   * Methods: 
      * public void ***ExitImmediate***()
      * public void ***Exit***()
      * public void ***EnterImmediate***()
      * public void ***Enter***()
### Aim Constraints
1. public class **LocalAimConstraint** : MonoBehaviour
   * Properties: 
      * public Transform ***Target***  { get; set; }
### Cameras
1. public class **CameraFieldOfViewAnimator** : MonoBehaviour
   * Properties: 
      * public float ***Seconds***  { get; }
      * public float ***ZoomedOutFieldOfView***  { get; }
      * public float ***ZoomedInFieldOfView***  { get; }
      * public float ***CurrentFieldOfView***  { get; }
   * Methods: 
      * public void ***ZoomIn***()
      * public void ***ZoomIn***(Action onComplete)
      * public void ***ZoomIn***(float animationSeconds, Action onComplete)
      * public void ***ZoomInImmediate***()
      * public void ***ZoomInImmediate***(Action onComplete)
      * public void ***ZoomOut***()
      * public void ***ZoomOut***(Action onComplete)
      * public void ***ZoomOut***(float animationSeconds, Action onComplete)
      * public void ***ZoomOutImmediate***()
      * public void ***ZoomOutImmediate***(Action onComplete)
      * public void ***ZoomTo***(float fieldOfView)
      * public void ***ZoomTo***(float fieldOfView, Action onComplete)
      * public void ***ZoomTo***(float fieldOfView, float animationSeconds, Action onComplete)
      * public void ***ZoomToImmediate***(float fieldOfView)
      * public void ***ZoomToImmediate***(float fieldOfView, Action onComplete)
### Canvas Groups
1. public class **CanvasGroupAnimateByPosition** : MonoBehaviour
1. public class **CanvasGroupAnimator** : StandardAnimation
   * Methods: 
      * public override void ***Enter***()
      * public override void ***Enter***(Action onComplete)
      * public override void ***Enter***(float animationSeconds, Action onComplete)
      * public void ***FadeIn***()
      * public void ***FadeIn***(Action onComplete)
      * public void ***FadeIn***(float animationSeconds, Action onComplete)
      * public override void ***EnterImmediate***()
      * public override void ***EnterImmediate***(Action onComplete)
      * public void ***FadeInImmediate***()
      * public void ***FadeInImmediate***(Action onComplete)
      * public override void ***Exit***()
      * public override void ***Exit***(Action onComplete)
      * public override void ***Exit***(float animationSeconds, Action onComplete)
      * public void ***FadeOut***()
      * public void ***FadeOut***(Action onComplete)
      * public void ***FadeOut***(float animationSeconds, Action onComplete)
      * public override void ***ExitImmediate***()
      * public override void ***ExitImmediate***(Action onComplete)
      * public void ***FadeOutImmediate***()
      * public void ***FadeOutImmediate***(Action onComplete)
1. public class **CanvasGroupPulser** : MonoBehaviour
### Rect Transforms
1. public class **Drawer** : StandardAnimation
   * Actions: 
      * public event Action ***OnCurrentTargetChanged*** 
      * public event Action ***OnMoving*** 
   * Properties: 
      * public float ***PreviousTarget***  { get; }
      * public float ***CurrentTarget***  { get; }
   * Methods: 
      * public Drawer ***Initialize***(float seconds, Vector2 anchorMinOpen, Vector2 anchorMaxOpen, Vector2 anchorMinClosed, Vector2 anchorMaxClosed)
      * public override void ***Enter***()
      * public override void ***Enter***(Action onComplete)
      * public override void ***Enter***(float animationSeconds, Action onComplete)
      * public float ***Open***()
      * public float ***Open***(Action onComplete)
      * public float ***Open***(float animationSeconds, Action onComplete)
      * public override void ***EnterImmediate***()
      * public override void ***EnterImmediate***(Action onComplete)
      * public void ***OpenImmediate***()
      * public void ***OpenImmediate***(Action onComplete)
      * public override void ***Exit***()
      * public override void ***Exit***(Action onComplete)
      * public override void ***Exit***(float animationSeconds, Action onComplete)
      * public float ***Close***()
      * public float ***Close***(Action onComplete)
      * public float ***Close***(float animationSeconds, Action onComplete)
      * public override void ***ExitImmediate***()
      * public override void ***ExitImmediate***(Action onComplete)
      * public void ***CloseImmediate***()
      * public void ***CloseImmediate***(Action onComplete)
      * public float ***SetTarget***(float target, Action onComplete)
      * public void ***SetTargetImmediate***(float target)
1. public class **DrawerSFX** : MonoBehaviour
1. public class **FlippableRect** : MonoBehaviour
   * Properties: 
      * public GameObject ***FrontSide***  { get; }
      * public GameObject ***BackSide***  { get; }
   * Methods: 
      * public void ***Flip***()
      * public void ***FlipToFront***()
      * public void ***FlipToBack***()
1. public struct **Padding**
   * Properties: 
      * public float ***Top***  { get; }
      * public float ***Right***  { get; }
      * public float ***Bottom***  { get; }
      * public float ***Left***  { get; }
1. public class **RectTransformMatcher** : MonoBehaviour
   * Methods: 
      * public void ***LateUpdate***()
1. public class **RectTransformPointFollower** : MonoBehaviour
   * Methods: 
      * public void ***LateUpdate***()
1. public class **RectTransformPositionAnimator** : MonoBehaviour
   * Methods: 
      * public void ***Reset***()
      * public void ***SetPosition***(Vector2 anchorMin, Vector2 anchorMax)
### Rotation
1. public class **Rotator** : MonoBehaviour
### Scale
1. public class **ScalePulser** : MonoBehaviour
1. public class **ScaleUpAndDownAnimator** : StandardAnimation
   * Methods: 
      * public override void ***Enter***()
      * public override void ***Enter***(Action onComplete)
      * public override void ***Enter***(float animationSeconds, Action onComplete)
      * public void ***ScaleUp***()
      * public void ***ScaleUp***(Action onComplete)
      * public void ***ScaleUp***(float animationSeconds, Action onComplete)
      * public override void ***EnterImmediate***()
      * public override void ***EnterImmediate***(Action onComplete)
      * public void ***ScaleUpImmediate***()
      * public void ***ScaleUpImmediate***(Action onComplete)
      * public override void ***Exit***()
      * public override void ***Exit***(Action onComplete)
      * public override void ***Exit***(float animationSeconds, Action onComplete)
      * public void ***ScaleDown***()
      * public void ***ScaleDown***(Action onComplete)
      * public void ***ScaleDown***(float animationSeconds, Action onComplete)
      * public override void ***ExitImmediate***()
      * public override void ***ExitImmediate***(Action onComplete)
      * public void ***ScaleDownImmediate***()
      * public void ***ScaleDownImmediate***(Action onComplete)
