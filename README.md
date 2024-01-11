# II-proyecto-final-Aether-Worlds

### Cuestiones importantes para el uso del programa 

#### Controles de mando
El programa esta diseñado teniendo en cuenta el uso de un mando conectado por bluetooth, el movimiento y las acciones del usuario estan enteramente ligadas al uso del mando a excepción de la reticula del google cardboard.
Si se dispone de un mando para poder probar el apk a continuación se listan los controles usados:

Joystick izquierdo: Utilizado para mover al jugador o al coche dependiendo de la escena. 
Gatillo izquierdo: Marcha atras del coche en la escena vaporwave.
Gatillo derecho: Acelerar el coche en la escena vaporwave.
Botón A: Entrar en las diferentes zonas al mirar los portales | Interactuar y lanzar los patos de goma.
Botón B: Soltar a los patos de goma.
Botón X: Activar el microfono.
Botón Y: Desactivar el microfono. 

#### Paquetes a instalar en caso de modificar el proyecto
Pese a que la exportación del proyecto presenta casi toda las dependencias a necesitar. Algunos assets utilizados no se guardan en el paquete exportado:

- Paquetes importados por Git URL
  - Google Cardboard
  - Hugging Face

- Paquetes propios de Unity
  - PostProcessing
  - CinemaMachine

### Hitos de programación relacionados con los contenidos impartidos 
(Aprovechar y ponerlos aquí Ejemplos de la ejecución del programa)  

#### Postprocesado

#### Modelado 3D


#### Audio y doblaje

Utilizando varios componentes AudioSource y el AudioListener se pueden gestionar de manera secuencial para la realización de dialogos. 
En la Zona pirata se han utilizado varias boxCollider para representar un dialogo con la calavera

[Poner gif de la calavera spawneando]

#### Simulación de físicas
Para probar los conceptos acerca de física se ha ideado la creación de patos de goma que reboten y le afecte el viento.

Se ha programado el script DuckBehaviour para realizar la interacción jugador con los patos de goma. 
El jugador es capaz de tomar el pato utilizando el botón A al mirarlo. 
Volver a pulsar el botón A lanzará al pato en la dirección de la cámara.
Si se tiene al pato y se presiona el Botón B se soltará al pato sin ninguna fuerza adicional.

[Poner GIF de la Ejecución]

Adicionalmente a la física de los patos, se ha creado un script FanController que añade una fuerza, simulado viento, a los patos.
permitiendo elevar o mover a los patos en la dirección a la que observe el ventilador. Esto se consigue gracias a Raycast esfericos.

[Poner GIF de la Ejecución]

#### Reconocimiento de voz, utilización de microfono y eventos.

Aprovechando la API de reconocimiento de voz explicado en las sesiones de práctica se ha ideado un minijuego que utilice la voz del jugador para realizar acciones.
Se han creado los scripts VoiceMinigame, DuckVoiceResponse y SpeechRecognition.

El script SpeechRecognition recibe el input de activación del microfono y la voz del jugador y activa el Evento MoveDuck.
El script VoiceMinigame genera y controla la ejecución del minijuego.
El script DuckVoiceResponse se mantiene a la escucha del evento MoveDuck y cambia la posición de los patos dentro del tablero.

[Poner GIF de la Ejecución]

#### Utilización de sensor: Giroscopio.

Utilizando los sensores del movil se puede aprovechar el giroscopio para obtener la velocidad ángular del jugador y así comprobar si se esta moviendo el movil.
Unido a la utilización de las google Cardboard se puede simular el efecto de mover la cabeza dentro del juego.

[Ejecución GIF]

### Aspectos a destacar 

#### Agradecimientos

#### Creación de modelados propios y problemas de Ejes 

### Reparto del trabajo

La idea general para la elaboración de este proyecto es la realización del trabajo separado en varias escenas creadas cada uno por un integrante. 
Por tanto se ha llevado a cabo la siguiente distribución:

#### Tareas realizadas en conjunto:
##### Creación de una escena central
El proyecto cuenta con un escenario central donde poder escoger una de las tres posibles zonas. Para representar los contenidos de cada una de las zonas jugables se ha tomado la decisión de crear pequeñas islas alrededor del espacio central que ilustren el estilo y minijuegos que se encuentran dentro. La creación de las islas se desglosa de la siguiente manera:
- Modelo de la isla central: Saúl Sosa diaz.
- Modelo de la isla con tematica vaporwave: Saúl Sosa diaz.
  - Decoración de la isla: Jorge Gonzales Delgado.
- Modelo de la isla con tematica cientifica: Saúl Sosa diaz.
  - Decoración de la isla: Raimon Mejías Hernández.
- Modelo de la isla con tematica pirata: Saúl Sosa diaz.
  - Decoración de la isla: Saúl Sosa diaz.
- Creación de scripts para la escena: Saúl Sosa diaz, Raimon Mejías Hernández y Jorge Gonzales Delgado.

#### Tareas realizadas por separado
Además de la escena central se han creado 3 escenas adicionales, cada zona del juego ha sido creado por uno de los integrantes.

- Isla Vaporwave: Jorge Gonzales Delgado.
- Isla Cientifica: Raimon Mejías Hernández.
- Isla Pirata: Saúl Sosa Diaz. 


