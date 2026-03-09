# Blockly WebGL 2D Game

🎮 **2D WebGL game controlled via Blockly blocks**, designed to teach programming concepts.  
This project was developed as my Bachelor's thesis in Computer Science.

---

## 🎯 Project Overview

This game allows the player to program the main character by arranging **Blockly blocks** in a sequential logic system.  
The blocks represent commands and control the player's actions in the WebGL 2D environment, providing a **hands-on experience in programming logic and sequencing**.  

Key features:
- **2D game environment** built in Unity with WebGL export
- **Blockly-based programming interface** for controlling the character
- Sequential execution of commands: move, jump, interact
- Focused on **educational purposes** and learning programming
- Fully run in a **web browser (HTML/CSS/JS)**

---

## 📹 Demo Video

You can watch a demo of the project here:

! Watch the video
[![Demo Video](https://github.com/user-attachments/assets/3406f1bd-4253-445b-b672-e876879eb282)](https://www.linkedin.com/posts/mathewrocha_educational-game-project-for-learning-programming-ugcPost-7376851929257172995-iP-2?utm_source=share&utm_medium=member_desktop&rcm=ACoAAFXzyXgBYNlATVGA-SynVdinedCOFlK0bpU)

---

## 🗂 Project Structure

```text
blockly-2d-game/
│
├─ README.md                         # Project overview, video, instructions
├─ unity_project/                    # Original Unity project
│   ├─ Assets/
│   ├─ ProjectSettings/
│   ├─ Packages/
│   └─ ... (Unity development files)
│
└─ web_integration/                  # Web interface integrating Blockly with the Unity WebGL game
    ├─ Game/                         # Files related to the game interface or embedded WebGL game
    ├─ HandShake/                    # JavaScript code handling communication between the site and Unity
    │   ├─ CallUnity.js              # Functions to send commands from JS to Unity
    │   └─ ScriptsUnity.js           # Additional helper scripts for Unity interaction
    ├─ ScriptsBlockly/               # JavaScript managing Blockly blocks and program logic
    ├─ styles/                       # CSS styling for Blockly interface and web pages
    │   └─ style.css                 # Main CSS file for the web interface
    ├─ blockly/                      # Blockly library and any custom blocks definitions 
    └─ index.html                    # Main entry point of the web integration interface
