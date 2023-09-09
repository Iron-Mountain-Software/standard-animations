# Standard Animations

_A collection of various animations to jumpstart any project._

EASY SET UP! NO CODING REQUIRED!

Many of my games share a need for simple animations, so this package is a collection of my commonly used animation scripts.

---

### Use Cases:

Quickly add simple animations to GameObjects and UI elements.

---

###Directions for Use:

All of the key components can be used out-of-the box, and each one is pretty straightforward to set up.

One thing to note, SimpleAnimation is an abstract class with methods Enter() and Exit(). Drawer, CanvasGroupAnimator, and ScaleAnimator all inherit from SimpleAnimation, and can be grouped together when spawning or destroying GameObjects.

---

### Key components:

1. **CanvasGroupAnimator**
   * Fade canvas group alpha in or out.


1. **CanvasGroupPulser**
   * Fluctuate canvas group alpha between max and min values.


1. **CanvasGroupAnimateByPosition**
   * ​Set a canvas group alpha based on screen position.

   
1. **Drawer (and DrawerSFX)**
   * ​Animate a RectTransform between 2 positions.


1. ** RectTransformPositionAnimator**
   * ​Animate a RectTransform between any number of positions.


1. **FlippableRect**
   * ​​Flip a RectTransform over and change it's content. (like a double sided playing card)


1. **Rotator**
   * ​Continuously rotate an object.


1. **ScalePulser**
   * ​Fluctuate transform scale between max and min values.


1. ** ScaleUpAndDownAnimator**
   * ​Scale a transform up or down.

