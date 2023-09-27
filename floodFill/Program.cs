using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
   
    static void Main(string[] args)
    {   
        List<char> listTypu = new List<char>();
        int sirka = int.Parse(Console.ReadLine());
        int vyska = int.Parse(Console.ReadLine());
        Deska deska=new Deska(vyska, sirka);
        for (int i = 0; i < vyska; i++)
        {
            string line = Console.ReadLine();
            
            for (int j = 0; j < sirka; j++)
            { 
                deska.pozice[i,j].typ=line[j];
                deska.pozice[i, j].typId = 0;

                if (line[j] != '.'&& line[j] != '#')
                {
                    listTypu.Add(line[j]);
                    deska.pozice[i, j].typId = listTypu.Count;
                }
            }
        }
        do
        {
            deska.nastavBudouciTyp();
        }
        while (deska.premena());
        deska.vypis();
        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");
        //Console.WriteLine("answer");
    }
}
public class Policko
{
    public char typ { get; set; }
    public int typId { get; set; }
    public char budouciTyp { get; set; }
    public int budouciTypId { get; set; }
    public bool changed { get; set; }
    public void nastavBudouciTyp(List<Policko> sousedi)
    {
        bool druhyTyp = false;
        char c = ' ';
        int q = 0;
        budouciTyp = typ;
        budouciTypId = typId;
        if (typ == '.')
        {
            for (int i = 0; i < sousedi.Count; i++)
            {
                if (sousedi[i].typ != '.' && sousedi[i].typ != '#')
                {
                    budouciTyp = sousedi[i].typ;
                    budouciTypId = sousedi[i].typId;
                    if (budouciTyp == c&&budouciTypId==q) continue;
                    if (druhyTyp)
                    {
                        budouciTyp = '+';
                    }
                    druhyTyp = true;
                    c = budouciTyp;
                    q = budouciTypId;
                }
            }
        }
    }
}
public class Deska
{
    public Policko[,] pozice { get; set; }
    private int vyska;
    private int sirka;
    public Deska(int vyska, int sirka)
    {
        this.sirka = sirka;
        this.vyska = vyska;
        pozice = new Policko[vyska, sirka];
        for (int i = 0; i < vyska; i++)
        {
            for (int j = 0; j < sirka; j++)
            {
                pozice[i, j] = new Policko();
            }
        }
    }
    public void nastavBudouciTyp()
    {
        for (int i = 0; i < vyska; i++)
        {
            for (int j = 0; j < sirka; j++)
            {
                pozice[i, j].nastavBudouciTyp(sousedi(i, j));
            }
        }
    }
    public List<Policko> sousedi(int x, int y)
    {
        List<Policko>soused = new List<Policko>();
        if (x > 0)
        {
            soused.Add(pozice[x - 1, y]);
        }
        if (x < vyska-1)
        {
            soused.Add(pozice[x + 1, y]);
        }
        if (y > 0)
        {
            soused.Add(pozice[x , y - 1]);
        }
        if (y < sirka-1)
        {
            soused.Add(pozice[x, y + 1]);
        }
        return soused;
    }
    public bool premena()
    {
        bool changed = false;
        for (int i = 0; i < vyska; i++)
        {
            for (int j = 0; j < sirka; j++)
            {
                if (pozice[i, j].typ != pozice[i, j].budouciTyp)
                {
                    changed = true;
                }

                pozice[i, j].typ = pozice[i, j].budouciTyp;
                pozice[i, j].typId = pozice[i, j].budouciTypId;
            }
        }
        return changed;
    }
    public void vypis() {
        for (int i = 0; i < vyska; i++)
        {
            Console.WriteLine(vypis(i));
        }
    }
    private string vypis(int i)
    {
        string vypis = "";
        for (int j = 0; j < sirka; j++)
        {
            vypis=vypis+pozice[i,j].typ;
        }
        return vypis;
    }



}
