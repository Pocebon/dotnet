using System.Collections.Generic;
using System.IO;
using APICsv.DataBase.Models;

namespace APICsv.DataBase
{
    public class DBContext
    {
        private const string PathName =
        "C:\\Users\\Felipe\\Desktop\\PROJETOS_JULIANO\\.net\\APICsv\\APICsv\\animais.txt";


        private readonly List<Animal> _animais = new();

        public DBContext()
        {
            string[] lines =
                  File.ReadAllLines(PathName);

            for (int i = 1; i < lines.Length; i++)
            {

                string[] colums =
                       lines[i].Split(';');

                Animal animal = new();
                animal.Id = int.Parse(colums[0]);
                animal.Name = colums[1];
                animal.Classification = colums[2];
                animal.Origin = colums[3];
                animal.Reproduction = colums[4];
                animal.Feeding = colums[5];

                _animais.Add(animal);
            }
        }

        public List<Animal> Animals => _animais;

    }
}

