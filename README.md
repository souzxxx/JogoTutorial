# Coletor Estelar

Projeto "Introdução ao Unity" — Jogos Digitais, Insper 2026.

**Autor:** Leonardo de Souza Lima e Silva

Jogo 2D feito em Unity 6 (6000.6.4f1, Universal 2D) a partir do tutorial "Introdução Rápida" da disciplina, expandido com novas mecânicas.

## História

Sua nave ficou presa no cinturão de asteroides e o oxigênio está acabando. Colete todas as estrelas para recarregar o motor de salto e escapar antes que o O2 chegue a zero.

## Como jogar

| Ação | Teclado | Controle (Xbox) |
|------|---------|-----------------|
| Mover | WASD / Setas | Analógico esquerdo / D-pad |
| Dash | Espaço | A |
| Pausar | ESC / P | Start |
| Navegar nos menus | Setas + Enter / Mouse | D-pad + A |

- Colete as **12 estrelas** para vencer.
- Cada estrela vale **100 pontos** e devolve **+2s de oxigênio**.
- Os **raios azuis** dão **+8s** de oxigênio, 50 pontos e recarregam toda a stamina do dash.
- O **dash** gasta stamina (barra no canto inferior esquerdo), que recarrega com o tempo.
- **Discos voadores** patrulham rotas fixas; o **caçador vermelho** patrulha e começa a perseguir quando você chega perto, ficando mais rápido conforme o tempo passa.
- Você tem **3 vidas**. Ao ser atingido, fica alguns instantes invencível.
- O jogo termina com vitória ao coletar todas as estrelas, ou com derrota se o oxigênio acabar ou as vidas zerarem. O oxigênio que sobrar vira pontos extras (10 por segundo).
- A tela final mostra pontuação, estrelas coletadas e o tempo final da partida.

## Funcionalidades

- Menu inicial com tela de instruções.
- Cronômetro de oxigênio regressivo, com alerta sonoro e visual nos últimos 10 segundos.
- Tempo como mecânica: estrelas e raios aumentam o oxigênio; o caçador acelera com o tempo; o tempo restante vira pontuação.
- Inimigos com patrulha (rotas pré-definidas) e com perseguição ao jogador.
- HUD com pontuação, estrelas, oxigênio, vidas e stamina.
- Menu de pausa e tela de fim de jogo com opções de jogar novamente ou voltar ao menu.
- Suporte a teclado e controle (Input System).
- Música de fundo e efeitos sonoros para coleta, bônus, dash, dano, alerta de tempo, vitória, derrota e cliques.

## Estrutura

- `Assets/Scenes`: `MenuInicial`, `Jogo`, `FimDeJogo`
- `Assets/Scripts`: lógica do jogo (`PlayerMovement`, `GameController`, `GameManager` estático, inimigos, menus)
- `Assets/Prefabs`: `Player`, `Coletavel`, `Raio`

## Créditos de assets

Todos os assets de terceiros são de domínio público (CC0), por [Kenney](https://www.kenney.nl):

- Sprites (naves, discos, meteoros, estrelas, raios, ícones de vida, botões, fundo): [Space Shooter Remastered](https://kenney.nl/assets/space-shooter-remastered)
- Fontes Kenney Future e Kenney Future Narrow: [Kenney Fonts](https://kenney.nl/assets/kenney-fonts)
- Sons de dano, bônus e derrota: [Space Shooter Remastered](https://kenney.nl/assets/space-shooter-remastered)
- Som do dash: [Sci-fi Sounds](https://kenney.nl/assets/sci-fi-sounds)
- Sons de coleta, alerta e clique: [Interface Sounds](https://kenney.nl/assets/interface-sounds)
- Jingle de vitória: [Music Jingles](https://kenney.nl/assets/music-jingles)

Música de fundo (`trilha.wav`): chiptune sintetizado proceduralmente para o projeto.

Base do projeto: tutorial "Introdução Rápida" da disciplina de Jogos Digitais do Insper.
