using System;
using System.Collections.Generic;

abstract class ChessPiece
{
    public string Position { get; protected set; }

    public ChessPiece(string position)
    {
        Position = position;
    }

    // Віртуальний метод для перевірки можливості ходу
    public virtual bool CanMoveTo(string targetPosition)
    {
        Console.WriteLine("Базовий клас Пішка(пєшка, ті що на першій лінії): Перевірка можливості руху.");
        return true;
    }

    // Перевизначення методу Equals для порівняння значень, а не посилань
    public override bool Equals(object obj)
    {
        if (obj is ChessPiece other)
        {
            return Position == other.Position;
        }
        return false;
    }
}

// Похідні класи: Кінь
class Knight : ChessPiece
{
    public Knight(string position) : base(position) { }

    public override bool CanMoveTo(string targetPosition)
    {
        Console.WriteLine("Кінь: Перевірка можливості руху коня на вказану позицію.");
        return true;
    }
}
//Слон
class Bishop : ChessPiece
{
    public Bishop(string position) : base(position) { }

    public override bool CanMoveTo(string targetPosition)
    {
        Console.WriteLine("Слон: Перевірка можливості руху слона на вказану позицію.");
        return true;
    }
}
//Тура
class Rook : ChessPiece
{
    public Rook(string position) : base(position) { }

    public override bool CanMoveTo(string targetPosition)
    {
        Console.WriteLine("Тура: Перевірка можливості руху тури на вказану позицію.");
        return true;
    }
}
//Дама
class Queen : ChessPiece
{
    public Queen(string position) : base(position) { }

    public override bool CanMoveTo(string targetPosition)
    {
        Console.WriteLine("Ферзь: Перевірка можливості руху дами на вказану позицію.");
        return true;
    }
}

class Program
{
    static void Main()
    {
        //Додав укр символи
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Визначення конструкторів та створення об'єктів
        ChessPiece[] chessPieces = new ChessPiece[]
        {
            new Knight("g2"),
            new Bishop("c4"),
            new Rook("a1"),
            new Queen("d8")
        };

        // Використання різних методів класів та поліморфізм
        foreach (var piece in chessPieces)
        {
            Console.WriteLine($"Фігура на позиції {piece.Position} може здійснити хід на h3: {piece.CanMoveTo("h3")}");
        }
        Console.WriteLine("_____________________________________early");
        // Раннє зв'язування
        Knight knight = new Knight("b1");
        Console.WriteLine(knight.CanMoveTo("a3"));

        Console.WriteLine("_____________________________________late");
        // Пізнє зв'язування
        ChessPiece dynamicPiece = new Bishop("f5");
        Console.WriteLine(dynamicPiece.CanMoveTo("d7"));
    }
}
