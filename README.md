# Bowling-Unity-Game
# "Bowl Blast Chaos"

> Jogo de bowling competitivo local para dois jogadores desenvolvido em Unity no âmbito da unidade curricular de Laboratório de Aplicações de Multimédia (Universidade Portucalense - UPT)

---

## Sobre o Projeto
**Bowl Blast Chaos** pega nas regras tradicionais do bowling (10 frames, strikes, spares e cálculo automático de pontuação) e adiciona uma camada caótica: **power-ups estratégicos** que afetam o adversário no turno seguinte, tornando cada jogada imprevisível.

O projeto incluiu desenvolvimento completo em C#, gestão de física da Unity, sistemas de turnos locais e modelação 3D própria em Blender (bola e pinos).

---

## Roadmap e Futuras Melhorias
Como projeto pessoal em evolução contínua, estão planeadas várias melhorias para futuras versões:
* **Scoreboard Detalhado:** Substituição da exibição atual de pontos por uma interface em formato de tabela completa e imersiva ao estilo profissional de bowling.
* **Evolução Visual dos Power-ups:** Transição das zonas atuais (cubos *flat* no chão) para elementos visuais dinâmicos e flutuantes (com animação suave de subida e descida) para maior destaque em ambiente arcade neon.
* **Polimento de UI/UX e Animações:** Refinamento dos menus e transições de câmara.
(...)

---

## Controlos (Dois Jogadores no Mesmo Teclado)

| Jogador | Ação | Teclas |
| :--- | :--- | :--- |
| **Jogador 1** | Mover mira (Esq. / Dir.) | `A` / `D` |
| **Jogador 1** | Lançar a bola | `Espaço` |
| **Jogador 1** | Spin (Esq. / Dir.) | `Q` / `E` |
| **Jogador 2** | Mover mira (Esq. / Dir.) | `J` / `L` |
| **Jogador 2** | Lançar a bola | `Enter`|
| **Jogador 2** | Spin (Esq. / Dir.) | `Q` / `E` |
| **Ambos** | Ciclar câmaras | `C` |
| **Ambos** | Pausa | `Escape` |

---

## Power-ups e Obstáculos
Ao recolher zonas especiais na pista, aplicas efeitos dinâmicos ao adversário no seu próximo turno:
* **Gelo (`IceZone`):** Aumenta a velocidade da bola em 8% e aplica forças laterais contínuas, desestabilizando a trajetória.
* **Lama (`MudZone`):** Reduz drasticamente a velocidade linear e angular para 45%, amortecendo o impacto nos pinos.
* **Obstáculos:** Instancia blocos físicos estáticos na pista, obrigando a calcular trajetórias alternativas.
* **Bola Pequena:** Reduz a escala da bola para 60% e a massa para 80%, diminuindo a transferência de força.

---

## Tecnologias e Ferramentas
* **Motor:** Unity (C#)
* **Modelação 3D:** Blender (Bola de bowling e pinos modelados pelo autor)
* **Estilo Visual:** Ambiente arcade com iluminação neon e efeitos de partículas (VFX).

---

## Autor
* **Rodrigo Miguel dos Santos Sousa**
* Licenciatura em Engenharia Informática — **Universidade Portucalense (UPT)**
