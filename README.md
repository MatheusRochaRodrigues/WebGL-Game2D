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

[![Watch the video](<img width="1512" height="856" alt="Screenshot_70" src="https://github.com/user-attachments/assets/3406f1bd-4253-445b-b672-e876879eb282" />)](https://www.linkedin.com/posts/mathewrocha_educational-game-project-for-learning-programming-ugcPost-7376851929257172995-iP-2?utm_source=share&utm_medium=member_desktop&rcm=ACoAAFXzyXgBYNlATVGA-SynVdinedCOFlK0bpU)

*(Replace `YOUR_VIDEO_ID` with your YouTube video ID)*

---

## 🗂 Project Structure

```text
blockly-webgl-2d-game/
│
├─ README.md
├─ index.html          # Main HTML page hosting the game
├─ style.css           # Styling for the web interface
├─ blockly/            # Blockly library and custom blocks
├─ game/               # Unity WebGL exported game files
│   ├─ Build/
│   ├─ TemplateData/
├─ scripts/            # JavaScript connecting Blockly to the game
└─ assets/             # Any images, sounds, or assets used
