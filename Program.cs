


ConsoleColor oldBackgroundColor = Console.BackgroundColor;
Console.BackgroundColor = ConsoleColor.Yellow;
void paso0instrucciones()
{
    Console.ForegroundColor = ConsoleColor.Red; 
    Console.WriteLine("\t\t\t\t¡Bienvenido al juego de Batalla Naval!");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\t\tEl objetivo del juego es hundir los cuatro barcos de tu adversario.");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Para hacerlo, debes adivinar la ubicación de los barcos ingresando las coordenadas de la celda que quieras atacar.");
    Console.WriteLine("\tSi adivinas la ubicación de un barco, lo hundirás. Si fallas, la celda se marcará con un asterisco (*).");
    Console.WriteLine("\tGanas el juego si logras hundir todos los barcos en la menor cantidad de intentos posible.");
    Console.WriteLine();
    Console.ResetColor();
}


int[,] tablero1 = new int[10, 10];
int[,] tablero2 = new int[10, 10];
int jugadorActual = 1;
int intentos = 0;
int barcosHundidos = 0;
void paso1creartablero()
{
    for (int f = 0; f < tablero1.GetLength(0); f++)
    {
        for (int c = 0; c < tablero1.GetLength(1); c++)
        {
            tablero1[f, c] = 0;
            tablero2[f, c] = 0;
        }
    }
}

void paso2colocarbarcos2jugadores()
{
    int fila, columna, barcosColocados = 0;

    while (barcosColocados < 8)
    {
        Console.Clear();
        paso0instrucciones();
        Console.WriteLine($"Jugador 1, coloca tus barcos.");

        for (int i = 0; i < 4; i++)
        {
            Console.Write($"Ingresa la fila del barco {i + 1}: ");
            fila = Convert.ToInt32(Console.ReadLine());

            Console.Write($"Ingresa la columna del barco {i + 1}: ");
            columna = Convert.ToInt32(Console.ReadLine());

            if (tablero1[fila, columna] == 0)
            {
                tablero1[fila, columna] = 1;
                barcosColocados++;
            }
            else
            {
                Console.WriteLine("La celda ya está ocupada, intenta de nuevo.");
                i--;
            }
        }

        Console.Clear();
        paso0instrucciones();
        Console.WriteLine($"Jugador 2, coloca tus barcos.");

        for (int i = 0; i < 4; i++)
        {
            Console.Write($"Ingresa la fila del barco {i + 1}: ");
            fila = Convert.ToInt32(Console.ReadLine());

            Console.Write($"Ingresa la columna del barco {i + 1}: ");
            columna = Convert.ToInt32(Console.ReadLine());

            if (tablero2[fila, columna] == 0)
            {
                tablero2[fila, columna] = 1;
                barcosColocados++;

            }
            else
            {
                Console.WriteLine("La celda ya está ocupada, intenta de nuevo.");
                i--;
            }
        }
    }

    Console.WriteLine("Los barcos han sido colocados.");
    Console.WriteLine();
    paso3imprimir(tablero1);
}
void paso3imprimir(int[,] tablero)
{
    string caracteraimprimir = "~";
    for (int f = 0; f < tablero.GetLength(0); f++)
    {
        for (int c = 0; c < tablero.GetLength(1); c++)
        {
            switch (tablero[f, c])
            {
                case 0:
                    caracteraimprimir = "~";
                    break;
                case 1:
                    caracteraimprimir = "b";
                    break;
                case -1:
                    caracteraimprimir = "*";
                    break;
                case -2:
                    caracteraimprimir = "x";
                    break;
                default:
                    caracteraimprimir = "~";
                    break;
            }
            Console.Write(caracteraimprimir + " ");
        }
        Console.WriteLine();
    }
}


void paso4567ingresocordenadas()
{
    int fila, columna, barcosHundidosJugador1 = 0, barcosHundidosJugador2 = 0;
    int turno = 1;
    int tableroActual = 1;
    bool acerto = false;

    do
    {
        Console.Clear();
        Console.WriteLine($"Turno {turno} - Jugador {jugadorActual}");
        Console.WriteLine($"Tablero del jugador {jugadorActual}:");
        if (tableroActual == 1)
        {
            paso3imprimir(tablero1);
        }
        else
        {
            paso3imprimir(tablero2);
        }

        Console.Write($"Jugador {jugadorActual}, ingresa la fila: ");
        fila = Convert.ToInt32(Console.ReadLine());
        Console.Write($"Jugador {jugadorActual}, ingresa la columna: ");
        columna = Convert.ToInt32(Console.ReadLine());

        if (tableroActual == 1)
        {
            if (tablero2[fila, columna] == 1)
            {
                Console.Beep();
                tablero2[fila, columna] = 2;
                Console.WriteLine("¡Has golpeado un barco del jugador 2!");
                Console.ReadLine();
                barcosHundidosJugador1++;
                acerto = true;
            }

            else if (tablero2[fila, columna] == 0)
            {
                Console.Beep();
                tablero2[fila, columna] = -1;
                Console.WriteLine("Aguas.");
                Console.ReadLine();
                acerto = false;
            }
            else
            {
                Console.WriteLine("Ya has intentado en esa posición.");
                acerto = false;
            }
        }
        else
        {
            if (tablero1[fila, columna] == 1)
            {
                Console.Beep();
                tablero1[fila, columna] = 2;
                Console.WriteLine("¡Has golpeado un barco del jugador 1!");
                Console.ReadLine();
                barcosHundidosJugador2++;
                acerto = true;
            }
            else if (tablero1[fila, columna] == 0)
            {
                Console.Beep();
                tablero1[fila, columna] = -1;
                Console.WriteLine("Aguas.");
                Console.ReadLine();
                acerto = false;
            }
            else
            {
                Console.WriteLine("Ya has intentado en esa posición.");
                acerto = false;
            }
        }

        if (acerto)
        {
            intentos++;
        }

        if (barcosHundidosJugador1 == 4)
        {
            Console.WriteLine("¡El juego termino para ti !");
            break;
        }
        else if (barcosHundidosJugador2 == 4)
        {
            Console.WriteLine("¡El juego termino para ti !");
            break;
        }

        if (!acerto)
        {
            jugadorActual = jugadorActual == 1 ? 2 : 1;
            tableroActual = tableroActual == 1 ? 2 : 1;
            turno++;
        }
    } while (true);
}



void paso8()
{
   
   
    bool todosLosBarcosHundidos = false;
    int jugadorActual = 1;

    do
    {
        intentos++;
        barcosHundidos = 0;

        for (int f = 0; f < tablero1.GetLength(0); f++)
        {
            for (int c = 0; c < tablero1.GetLength(1); c++)
            {
                if (tablero1[f, c] == 2)
                {
                    barcosHundidos++;
                }
            }
        }

        if (barcosHundidos == 4)
        {
            Console.WriteLine($"¡Felicidades, Jugador {jugadorActual}! ¡Has hundido todos los barcos en {intentos} intentos!");
            todosLosBarcosHundidos = true;
            break;
        }

        Console.WriteLine($"Turno del Jugador {jugadorActual}:");
        paso4567ingresocordenadas();
        paso3imprimir(tablero1);

        jugadorActual = (jugadorActual == 1) ? 2 : 1; // Alternar entre jugadores
    } while (!todosLosBarcosHundidos);
}


paso1creartablero();
paso2colocarbarcos2jugadores();
paso3imprimir(tablero1);
paso3imprimir(tablero2);
paso8();




