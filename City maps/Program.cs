using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using City_maps.Models;
using MySql.Data.MySqlClient;

namespace City_maps
{
    internal class Program
    {
        private static MySqlConnection _connection;
        private const string ConnectionString = "server=localhost;port=3336;user=root;database=Db;password=1234;";
        
        public static void Main(string[] args)
        {
            var json = File.ReadAllText("../../table_json.json");
            var cities = JsonSerializer.Deserialize<IEnumerable<City>>(json)?.ToList();

            InsertData(cities);
        }

        private static void InsertData(List<City> data)
        {
            _connection = new MySqlConnection(ConnectionString);
            _connection.Open();
            
            foreach (var city in data)
            {
                InsertCoords(city);
                var coordsId = FindCoordsId(city);
                InsertCity(city, coordsId);
            }

            _connection.Close();
        }

        private static void InsertCoords(City city)
        {
            var coordsSql = $"INSERT INTO Coords (lat, lon) VALUE (\'{city.Coords.Lat}\', \'{city.Coords.Lon}\')";
            var command = new MySqlCommand(coordsSql, _connection);
            var reader = command.ExecuteReader();
            Print(reader, 3);
            reader.Close();
        }

        private static int FindCoordsId(City city)
        {
            var sql = $"SELECT id FROM Coords WHERE (lat={city.Coords.Lat} AND lon={city.Coords.Lon}) LIMIT 1";
            Console.Write(sql);
            var command = new MySqlCommand(sql, _connection);
            var reader = command.ExecuteReader();
            var coordsId = reader.Read() ? reader[0] : 0;
            reader.Close();
            return (int) coordsId;
        }

        private static void InsertCity(City city, int coordsId)
        {
            var citiesSql = $"INSERT INTO City (district, name, population, subject, coords_id) VALUE (\'{city.District}\', \'{city.Name}\', \'{city.Population}\', \'{city.Subject}\', \'{coordsId}\')";
            var command = new MySqlCommand(citiesSql, _connection);
            var reader = command.ExecuteReader();
            Print(reader, 6);
            reader.Close();
        }

        private static void Print(MySqlDataReader reader, int columnCount)
        {
            while (reader.Read())
            {
                var row = "";
                for (var i = 0; i < columnCount; i++)
                {
                    row += "\t" + reader[i];
                }

                Console.WriteLine(row);
            }
        }
    }
}