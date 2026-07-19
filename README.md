# What is this repository?
This is my hobby project to explore software architecture. I am a developer in the field of Manufacturing Execution Systems (MES) and thus simply use it as a domain to experiment with.

# Under construction
Everything in this repository is under construction, potentially experimentative and this not taken too seriously. However I try to aim at achieving functioning software with high qualitative standards.

# Goals
My goals with this project span from the system and software architecture over technologies implementing architectural paradigms to hosting and scalability.

## System and software architecture
- Implement a cloud-native MES with
  - a hub system as the heart of the MES, that is hosted on the cloud
  - a shopfloor system as an executive system on the shopfloor level, that is hosted locally in every shopfloor connected to the hub
- Prioritize simple hosting
- Prioritize simple and flexible scalability
- Prioritize simplicity in architecture, design and technologies
- Avoid high levels of abstractions and observe what happens when youre not obsessing over abstracting everything
- Keep a small, open-source and free tech stack

## Technological interests
- How is event sourcing best used in a system and how does it fare in MES?
- How is event driven architecture (EDA) best implemented, what technologies are best available and how does EDA fare in MES?
- What exactly should clean architecture, implemented using vertical slicing, look like and how does it scale?
- How are architectural paradrigms such as EDA, event sourcing, clean architecture as vertical slicing and others best combined?
- How can software be designed, so the developer does not have to traverse through layers and layers and layers of abstractions of generic *services*, *managers* or *providers*?
- Does one really need a repository?
