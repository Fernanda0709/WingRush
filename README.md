# 🐔 Wing Rush

## Descripción general
Wing Rush es un videojuego multijugador de carreras para 2 jugadores desarrollado en Unity. 
Cada jugador controla un pollo y debe llegar primero a la meta presionando la tecla SPACE 
repetidamente. Cuanto más rápido presiones, más rápido corre tu pollo.

## Cómo se comunica el cliente con el servidor
La comunicación se realiza mediante peticiones HTTP desde Unity hacia un servidor Flask 
corriendo en Docker.

### Endpoints utilizados
- **POST** `http://localhost:5005/server/{game_id}/{player_id}`  
  Envía la posición actual del jugador al servidor.
```json
  {
    "posX": 5.0,
    "posY": 0.0,
    "posZ": 0.0
  }
```

- **GET** `http://localhost:5005/server/{game_id}/{player_id}`  
  Obtiene la posición del otro jugador mediante polling cada 0.1 segundos.

### Sistema de Polling
Cada cliente consulta periódicamente la posición del otro jugador usando coroutines de Unity,
evitando bloqueos en el hilo principal del juego.

## Instrucciones para ejecutar el juego

### Requisitos previos
- Docker Desktop instalado y corriendo
- Unity 2022.3.31f1 o superior

### Paso 1 — Levantar el servidor
Clona el servidor original
```bash
git clone https://github.com/memin2522/DedicatedServer-Api.git
cd DedicatedServer-Api
docker compose up
```

### Paso 2 — Verificar el servidor
Abre el navegador y ve a:

http://localhost:5005/swagger-ui

### Paso 3 — Ejecutar el juego
- Abre el archivo `WingRush.exe` dos veces
- Primera ventana → selecciona **Player 1**
- Segunda ventana → selecciona **Player 2**
- Ambas ventanas deben estar en la misma red

### Controles
| Acción | Tecla |
|--------|-------|
| Avanzar | SPACE |

## Requisitos técnicos
- Unity 2022.3.31f1 o superior
- Docker Desktop
- Windows 10/11
- Conexión a la misma red local para ambos jugadores

## Limitaciones conocidas
- Ambos jugadores deben estar conectados a la misma red WiFi
- El servidor debe estar corriendo antes de iniciar el juego
- Si un jugador se desconecta, el juego no tiene sistema de reconexión
- El servidor almacena datos en memoria, por lo que se pierden al reiniciarlo
- La sincronización depende de la velocidad de la red local
