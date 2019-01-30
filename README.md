# Virtual Reality Maze
## Autores

* [carlosdg](https://github.com/carlosdg)
* [DauteRR](https://github.com/DauteRR)

## Descripción

Como todo videojuego el objetivo principal es entretener al jugador.

En nuestro caso el jugador tiene que escapar de laberintos generados proceduralmente que están llenos de enemigos.

El jugador puede disparar y usar consumibles para facilitar las peleas contra los enemigos o esquivarlos.

El juego cuenta con dos escenas:
* Escena de laberinto: En la escena del laberinto el jugador deberá sobrevivir y llegar al punto final, hemos implementado un ciclo día-noche.
* Escena de menú principal: En la escena del menú principal el jugador puede iniciar una nueva partida (sucesión de laberintos), interactuar con un chatbot para resolver dudas y comprobar sus estadísticas.


## Recomendaciones sobre Realidad virtual

Las recomendaciones sobre aplicaciones de RV que hemos seguido son:
* El usuario tiene control total sobre el movimiento dentro del juego
* Hacemos uso de una retícula como punto de referencia
* Mantenemos una velocidad constante para no generar sensación de fatiga ni mareo
* Hemos incluido sonidos y efectos visuales de confirmación a los elementos interactuables
* La distancia al usuario de las interfaces gráficas y del HUD es la óptima (3m)

##  Cuestiones importantes de uso

Para poder jugar se necesita un mando de PlayStation 4 o mapear los botones en unity para poder utilizar otro tipo de mando:

| Botón 	| PC 	                | Mobile 	                |
|:------:	|:--:	                |--------	                |
|square   	| joystick button 0    	|   joystick button 0     	|
|   x     	| joystick button 1  	|   joystick button 1    	|
|  circle   | joystick button 2  	|   joystick button 13     	|
|triangle   | joystick button 3   	|   joystick button 2     	|
|    L1    	| joystick button 4 	|   joystick button 3     	|
|    R1  	| joystick button 5    	|   joystick button 14     	|
|    L2    	| joystick button 6    	|   joystick button 4     	|
|    R2    	| joystick button 7    	|   joystick button 5     	|
|    share  | joystick button 8    	|   joystick button 6     	|
|    options| joystick button 9    	|   joystick button 7     	|
|     L3   	| joystick button 10    |   joystick button 11     	|
|     R3   	| joystick button 11    |   joystick button 10     	|
|     PS   	| joystick button 12    |   joystick button 12     	|
|     PAD   | joystick button 13    |   joystick button 8     	|

## Jugador

El jugador dispone de:
* Nivel de salud (Inicialmente 100 unidades)
* Nivel de escudo (Inicialmente 0 unidades)
* Un arma (Sus proyectiles hacen 20 de daño a los enemigos)
* Una linterna a pilas (Inicialmente tiene 20 segundos)


Los controles del jugador son los siguientes:
* Se puede mover usando el joystick izquierdo
* Puede rotar usando L2 y R2
* Puede interactuar con elementos de las interfaces gráficas con el botón equis
* Puede interactuar con elementos del juego con el botón cuadrado
* Cuando el jugador esté en el laberinto, si pulsa el botón PS saldrá al menú principal
* Pulsando L3 el jugador apagará/encenderá su linterna
* Pulsando R1 el jugador disparará su arma

## Enemigos

Los enemigos aparecen en ciertas posiciones predeterminadas tras haber transcurrido entre 5 y 15 segundos.

Estos puntos pueden ser desactivados por el jugador con el botón cuadrado, cuando están desactivados no aparecen enemigos. Los puntos permanecen desactivados entre 10 y 30 segundos.

Cada enemigo tiene tres estados posibles:
* Idle
* Caminando
* Atacando
* Muerto

Hay tres tipos de enemigos:
* Ratones, son capaces de oír al jugador. Inflingen 15 de daño por golpe, tienen 80 de vida y una velocidad de 2,2
* Esqueletos, son capaces de ver al jugador. Inflingen 20 de daño por golpe, tienen 100 de vida y una velocidad de 1,7
* Caballeros, son capaces de ver y oír al jugador. Inflingen 30 de daño por golpe, tienen 150 de vida y una velocidad de 1,3


### Sistema de visión

* Cogemos todos los collider en un radio
* Nos quedamos con los que estén dentro de un cierto ángulo de visión
* Si no hay obstáculos en medio es que lo está viendo (Physics.Raycast)
* Se diferencian los obstáculos(laberinto) y el jugador haciendo uso de capas

### Sistema de escucha

* Cuando el jugador realiza una acción que conlleva un ruido se aumenta el volumen de un collider esférico que lo envuelve
* Los enemigos tienen un collider esférico que representa la distancia a la que oyen ruidos
* Cuando ambos collider chocan el jugador es detectado

## Consumibles

Para recoger los consumibles se ha de pulsar el botón cuadrado.

Los consumibles aparecen en unos puntos determinados (consumable locations) cada cierto tiempo (de 20 a 60 segundos).

Existen 8 tipos de consumibles:
* Vida: Aumenta la salud del jugador en 25 unidades (instantáneo)
* Escudo: Aumenta el escudo del jugador en 20 unidades (instantáneo)
* Pilas: Aumenta el uso de la linterna en 20 segundos (instantáneo)
* Aumentar daño: Aumenta el daño de los proyectiles en 10 unidades durante 15 segundos
* Jugador inaudible: No permite que los enemigos oigan al jugador durante 20 segundos
* Jugador invisible: No permite que los enemigos vean al jugador durante 20 segundos
* Pista: Muestra una pista visual en la localización del punto final del laberinto durante 10 segundos
* Visión de rayos x: Hace que las paredes del laberinto sean transparentes durante 10 segundos

## Dificultades y soluciones

Dificultades:
* La primera y mayor fue el trabajo colaborativo, no pudimos trabajar cómodamente
* Ausencia de assets gratuitos
* Que Unity incluya tantas funcionalidades es un arma de doble filo, puedes hacer cosas impresionantes pero solo si sabes bien como. Debido a nuestra inexperiencia ésto jugó en nuestra contra
* La mayoría de tareas que quisimos realizar llevaron mucho tiempo
* Ausencia de Hardware (cardboard y mando de ps4)

¿Cómo solventamos las dificultades:
* Haciendo un uso intensivo de la documentación
* Haciendo un uso intensivo de tutoriales en plataformas como Youtube
* Resolviendo mucho de los problemas leyendo en foros a usuarios con circunstancias parecidas
* Modelamos 7 de los consumibles desde cero usando Blender (modelos muy simples)
* Nos creamos nuestros assets a medida (filtro de partículas, sonidos, texturas, materiales ….)

## Distribución de tareas:
* Generación del laberinto: Daute
* Consumibles: Carlos y Daute
    * Scripting: Carlos
    * Modelado: Daute
* Ciclo día noche: Carlos
* Detección de daño entre jugador y enemigos: Daute
* Búsqueda de assets: Carlos
* Desarrollo del controlador de jugador: Daute
* Sistema de reaparición: Carlos y Daute
    * Desarrollo del spawn system: Carlos
    * Desarrollo de enemy respawn y consumable location: Daute
* Desarrollo de sistemas de visión y audición de los enemigos: Carlos
* Desarrollo del controlador de enemigos: Daute
* Desarrollo de chatbot: Carlos
* Creación de interfaces: Carlos

## Assets externos utilizados
* GoogleVR: para hacer el proyecto de RV con las cardboard
* ApiAiSDK: para el chatbot usando Dialogflow
* NavMeshComponents: para el movimiento de los enemigos
* DayNightCycle:
* Enemigo esqueleto
* Enemigo caballero
* Enemigo ratón
* Modelo del chatbot(robot)
* Modelo del consumible escudo
* Materiales para los disparos
* Arma del jugador

## Ejecución

<p align="center">
    <img src="Docs/1.gif" width="600" height="314"/>
</p>
<p align="center">
    <img src="Docs/2.gif" width="600" height="314"/>
</p>
<p align="center">
    <img src="Docs/3.gif" width="600" height="314"/>
</p>


## Referencias
* [VR Player Movement](https://www.youtube.com/watch?v=UBowqGbZ9a4&feature=youtu.be)
* [Procedural generation of mazes](https://www.youtube.com/watch?v=gXpi1czz5NA)
* [Enemy wandering system](https://www.youtube.com/watch?v=gXpi1czz5NA)
* [PS4 controller map for Unity](https://www.youtube.com/watch?v=gXpi1czz5NA)
* [Enemy vision system](https://www.youtube.com/watch?v=gXpi1czz5NA)
* [How to make a spoon in Blender](https://www.youtube.com/watch?v=unTHnOnvvZY)
* [Basic AI](https://www.youtube.com/watch?v=gXpi1czz5NA)
* [Game effects with particle systems](https://www.youtube.com/watch?v=iMcGkgP0P-M)
* [Unity Scripting API](https://docs.unity3d.com/ScriptReference/)
