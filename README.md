# 🚀 Space Travel | High-Speed Arcade Experience

![Unity](https://img.shields.io/badge/Unity-2021.3+-black?logo=unity)
![Platform](https://img.shields.io/badge/Platform-Mobile%20%2F%20PC-brightgreen)
![Physics](https://img.shields.io/badge/Physics-Rigidbody--Based-blue)

**Space Travel** is a fast-paced arcade game focused on precision movement and satisfying "Game Feel." Players navigate through hazardous environments where momentum and physics-based handling are the core challenges.

## 🕹️ Gameplay & Mechanics

The project explores the balance between challenging physics and intuitive controls, ensuring the player feels in total control of the ship's inertia.

* **Momentum-Based Navigation:** A custom flight model that rewards precise thrusting and careful braking.
* **Haptic Feedback System:** Integrated vibration and screen-shake patterns that correlate with the ship's velocity and impact force, enhancing the sensory experience.
* **Dynamic Hazard System:** Procedural or modular obstacle placement designed to test the player's reflexes at high speeds.

## 🔧 Technical Highlights

### 1. Physics & Velocity Tracking
Instead of using simple transform movements, Space Travel relies on a robust **Rigidbody-based system**:
* **Velocity Tracker:** A custom utility that monitors acceleration and G-forces to trigger visual and haptic effects (VFX/SFX) dynamically.
* **Optimized Collisions:** Fine-tuned collision detection to ensure high-speed impacts are handled accurately without clipping, even on mobile hardware.

### 2. Mobile Optimization
Designed with performance in mind for mobile devices:
* **Object Pooling:** Efficient management of particles, projectiles, and hazards to keep a steady 60 FPS by avoiding "Garbage Collection" spikes.
* **Modular State Management:** Using a simplified version of the *Ultimate Controller* logic to toggle game states (Active/Inactive/Paused) with minimal overhead.

### 3. Game Feel (Juice)
* **Vibration Engine:** Specialized scripts to handle different haptic profiles depending on the event (near-misses, light bumps, or total destruction).
* **Adaptive Camera:** A camera system that adjusts its FOV and "lead distance" based on the ship's current velocity to increase the sense of speed.

## 📂 Project Structure

* **/Scripts/Core**: Main flight logic and game state management.
* **/Scripts/Physics**: Velocity tracking and collision handling.
* **/Scripts/FX**: Haptics, vibration controllers, and visual feedback.
* **/Prefabs**: Optimized game entities ready for pooling.

## 👨‍💻 Author
<div aling="left">  
  <h4>Jesús Carrero - Unity Gameplay Engineer</h1>
  <a href="https://jesuscarrero.netlify.app/">
    <img src="https://img.shields.io/badge/Portfolio-a83333?style=for-the-badge&logo=netlify&logoColor=white" width="150" />
  </a>
</div>
