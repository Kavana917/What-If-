# What If?

**A 2D Side-Scrolling Narrative Platformer**

A thought-provoking platformer that challenges players to experience the world through a different lens, exploring societal expectations, historical inequities, and the shared human experience across time.

---

## 📖 Concept

What If? is a 2D side-scrolling platformer built in Unity that explores perspective, empathy, and the evolution of societal roles through history. The game presents a role-reversal world where traditional gender dynamics are inverted, allowing players to experience historical and modern challenges symbolizing the physical, emotional, and social burdens traditionally associated with women — embodied through a male protagonist.

Rather than delivering a direct message through dialogue or cutscenes, the game leverages gameplay mechanics, environmental design, and symbolic obstacles to encourage organic reflection and personal interpretation.

### Core Themes
- **Perspective & Empathy** – Understanding different lived experiences across history
- **Societal Evolution** – How roles, expectations, and challenges have shifted over time
- **Shared Humanity** – The universal struggle for balance and meaning regardless of era
- **Player Agency** – Choices within constraints that mirror real-world limitations

---

## 🎮 Gameplay Overview

Players journey through four distinct historical and cultural eras, each presenting unique challenges tied to their temporal context:

### Era Progression

**1️⃣ Survival Era** – The foundation of human experience
- Setting: Primal/early civilization
- Themes: Nature, endurance, sustainability, physical burden
- Mechanics: Basic platforming, resource gathering, environmental hazards
- Goal: Navigate natural challenges and gather resources for community survival

**2️⃣ Kingdom / Medieval Era** – Hierarchy and duty
- Setting: Medieval society and feudal structures
- Themes: Hierarchy, social restrictions, unfair labor, limited agency
- Mechanics: Navigating social constraints, completing assigned tasks, avoiding punishment
- Goal: Fulfill duties within a rigid social framework while maintaining dignity

**3️⃣ Industrial Era** – Labor pressure and burnout
- Setting: Factory and industrial revolution landscape
- Themes: Labor exploitation, exhaustion, productivity demands, loss of self
- Mechanics: Shift survival timer, demanding deliverables, gradual stamina drain, fatigue penalties
- Goal: Complete an impossible workload within time limits while managing burnout

**4️⃣ Modern Era** – Bias, balance, and identity
- Setting: Contemporary urban environment
- Themes: Subtle bias, work-life balance, identity navigation, systemic pressures
- Mechanics: Competing demands, invisible barriers, choice-based consequences
- Goal: Navigate modern complexities while maintaining personal identity and wellbeing

Each era maintains consistent core mechanics while reinterpreting them through its thematic lens, creating a cohesive yet evolving gameplay experience.

---

## ⚙️ Core Mechanics

### Movement & Platforming
- **2D Navigation** – Run, jump, climb, and traverse obstacles
- **Era-Specific Movement** – Physics and controls shift based on player exhaustion level
- **Environmental Interaction** – Ladders, platforms, hazards that reflect era-specific challenges

### Combat & Conflict Resolution
- **Defense Mechanics** – Combat system focused on evasion and defense rather than offense
- **Opponent Encounters** – Enemies represent authority, restrictions, or social barriers
- **Tactical Movement** – Win through navigation and timing rather than brute force

### Resource Management
- **Pickup & Delivery System** – Collect and transport era-specific items (crops, goods, deliverables)
- **Inventory Constraints** – Limited carrying capacity that affects movement speed
- **Strategic Drops** – Decide what to keep based on immediate vs. long-term needs

### Stamina & Exhaustion System
- **Gradual Drain** – Stamina depletes over time, accelerating through exertion
- **Burnout Mechanic** – Stamina loss causes movement penalties and reduced jump height
- **Recovery Limitations** – Stamina recovery varies by era (scarce in Industrial, abundant in Survival)
- **Visual Feedback** – On-screen indicators reflect player exhaustion through visual effects

### Choice-Driven Consequences
- **Branching Paths** – Multiple routes through levels with different difficulty profiles
- **Resource Tradeoffs** – Collect more items vs. move faster, rest vs. progress
- **Consequence Chains** – Early choices impact later level difficulty and story outcomes
- **No Clear "Right" Answer** – Design encourages multiple playstyles and perspectives

### Environmental Storytelling
- **Contextual Details** – Era-specific environments communicate theme without exposition
- **Dynamic Feedback** – World reacts to player choices and exhaustion level
- **Symbolic Hazards** – Obstacles represent abstract concepts (social barriers, systemic pressure)

---

## 🏭 Industrial Era Deep Dive

The Industrial Era serves as the emotional centerpiece, fully showcasing the exhaustion and burnout systems:

- **Shift Timer** – Survive increasingly difficult delivery quotas within a time limit
- **Deliverable Collection** – Gather and transport quota items to designated drop-off points
- **Stamina Drain Acceleration** – Exhaustion intensifies visual feedback and increases movement cost
- **Opponent Encounters** – Supervisors and authorities enforce quotas and restrict movement
- **Fatigue-Based Penalties** – Reduced jump height, slower movement, increased fall vulnerability
- **No Escape** – The quota must be met; the only variables are how you complete it and at what personal cost

---

## 🌱 Design Philosophy

What If? prioritizes mechanics-driven storytelling over traditional narrative:

- **✔ Symbolic Storytelling** – Themes emerge through gameplay rather than exposition
- **✔ Mechanics as Metaphor** – Systems like stamina drain represent real emotional/physical exhaustion
- **✔ Emotional Gameplay Feedback** – Visual and audio cues reflect player state and era tension
- **✔ Reusable, Scalable Systems** – Core mechanics evolve across eras without complete overhauls
- **✔ Minimal Dialogue** – Minimal text/dialogue; players interpret meaning through experience
- **✔ Playstyle Flexibility** – Speedrunning, careful exploration, and optimization are all valid approaches

---

## 🎨 Visual Style

- **Stylized 2D Aesthetic** – Hand-drawn or pixel-art style (era-appropriate) for visual consistency
- **Era-Based Asset Transitions** – Visual design language shifts dramatically between eras
- **Environmental Mood** – Color palette, lighting, and particle effects convey era atmosphere
  - Survival: Warm, natural, organic
  - Medieval: Dull, restrictive, castle-like
  - Industrial: Desaturated, mechanical, factory-like
  - Modern: Clean but sterile, urban, contemporary
- **Symbolic Design** – Enemies, hazards, and environmental elements carry thematic weight
- **Exhaustion Visualization** – Screen effects, player sprite changes, and camera behavior reflect stamina

---

## 🎮 Controls

| Action | Input |
|--------|-------|
| Move Left | A or Left Arrow |
| Move Right | D or Right Arrow |
| Jump | Space, W, or Up Arrow |
| Interact | E (context-dependent) |
| Drop Item | Q |
| Pause | ESC |

**Note:** Controls are era-responsive; high exhaustion may delay or reduce responsiveness.

---

## 🛠 Built With

- **Engine** – Unity (2D)
- **Language** – C#
- **Input System** – Unity Input System (modern input handling)
- **Architecture** – Custom gameplay scripts with modular era systems
- **Version Control** – Git

---

## 🚀 Getting Started

### Prerequisites
- Unity 2022 LTS or later
- Git for version control

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/what-if.git
   ```
2. Open the project in Unity
3. Load the main scene from `Assets/Scenes/MainScene.unity`
4. Press Play to run

### Development Setup
- All game logic is in `Assets/Scripts/`
- Era-specific logic is organized in era-specific folders
- Scenes are organized by era in `Assets/Scenes/`
- Audio assets are in `Assets/Audio/`
- Visual assets (sprites, animations) are in `Assets/Art/`

---

## 📋 Project Status

**Current Focus:** Industrial Era implementation and mechanics refinement

**Completed:**
- Core platforming mechanics
- Stamina/exhaustion system
- Industrial Era level design
- Basic UI framework

**In Progress:**
- Medieval Era implementation
- Visual polish and era transitions
- Audio design and feedback

**Planned:**
- Modern Era level design
- Story branching and consequences
- Performance optimization
- Build and release preparation

---

## 🤝 Contributing

This is an independent game project. Contributions, feedback, and suggestions are welcome.

---

## 📝 License

This project is currently unlicensed. Please contact the creator for usage rights.

---

## 👤 Creator

**NitinBharadwajMVS** – Game Designer & Developer

---

## 🎵 Inspiration & References

- Perspective-driven narrative games
- Social commentary through mechanics
- Historical role-reversal narratives
- Empathy-focused interactive media
