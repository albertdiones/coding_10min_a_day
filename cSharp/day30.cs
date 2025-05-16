
using System;
public static int[] roll() {
    Random dice = new Random();

    int dice1 = dice.Next(1,7);

    int dice2 = dice.Next(1,7);

    return [dice1, dice2];
}

int[] result = roll();

Console.WriteLine(
    "Dice: " + 
    result[0].ToString() +
    "," +
    result[1].ToString()
);