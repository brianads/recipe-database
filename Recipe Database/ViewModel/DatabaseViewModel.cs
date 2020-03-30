using GalaSoft.MvvmLight;
using Recipe_Database.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe_Database.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private string DbPath = "D:\\RecipeDatabase";
        private string DbPathName = "D:\\RecipeDatabase\\RecipeDatabase.db";

        SQLiteConnection m_dbConnection;
        void InitalizeDataBase()
        {
            if (!System.IO.File.Exists(DbPathName))
            {
                Directory.CreateDirectory(DbPath);
                SQLiteConnection.CreateFile(DbPathName);

                m_dbConnection = new SQLiteConnection("Data Source=" + DbPathName + ";Version=3;");
                m_dbConnection.Open();
                
                string sql = @"CREATE TABLE Recipes ( 
                            Name TEXT,
                            Type TEXT,
                            Origin TEXT,
                            Ingredients TEXT,
                            Procedure TEXT,
                            Notes TEXT
                            )";

                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }
            else
            {
                m_dbConnection = new SQLiteConnection("Data Source=D:\\RecipeDatabase\\RecipeDatabase.db;Version=3;");
                m_dbConnection.Open();
            }
        }

        public void GetRecipeList(ObservableCollection<Recipe> collection)
        {
            GetRecipeList(collection, null);
        }

        public void GetRecipeList(ObservableCollection<Recipe> collection, string filter)
        {
            if (collection == null)
            {
                collection = new ObservableCollection<Recipe>();
            }
            else
            {
                collection.Clear();
            }
            var sql = "SELECT * FROM Recipes";

            if (filter != null)
            {
                sql += string.Format(" WHERE Type = '{0}'", filter);
            }
            sql += " ORDER BY Name COLLATE NOCASE ASC";
            var command = new SQLiteCommand(sql, m_dbConnection);

            using (SQLiteDataReader sqReader = command.ExecuteReader())
            {
                while (sqReader.Read())
                {
                    collection.Add(new Recipe
                    {
                        Name = sqReader.GetString(0),
                        Type = sqReader.GetString(1),
                        Origin = sqReader.GetString(2),
                        Ingredients = sqReader.GetString(3),
                        Procedure = sqReader.GetString(4),
                        Notes = sqReader.GetString(5)
                    });
                }
            }
        }

        void GetSearchResultList(ObservableCollection<Recipe> collection, string searchTerm)
        {
            if (collection == null)
            {
                collection = new ObservableCollection<Recipe>();
            }
            else
            {
                collection.Clear();
            }
            var sql = "SELECT * FROM Recipes";

            searchTerm = searchTerm.Replace("'", "''");
            if (searchTerm != null)
            {
                sql += string.Format(" WHERE Name LIKE '%{0}%' OR Type LIKE '%{0}%' OR Origin LIKE '%{0}%' OR Ingredients LIKE '%{0}%' OR Procedure LIKE '%{0}%' OR NOTES LIKE '%{0}%' ", searchTerm);
            }
            sql += " ORDER BY Name COLLATE NOCASE ASC";
            var command = new SQLiteCommand(sql, m_dbConnection);

            using (SQLiteDataReader sqReader = command.ExecuteReader())
            {
                while (sqReader.Read())
                {
                    collection.Add(new Recipe
                    {
                        Name = sqReader.GetString(0),
                        Type = sqReader.GetString(1),
                        Origin = sqReader.GetString(2),
                        Ingredients = sqReader.GetString(3),
                        Procedure = sqReader.GetString(4),
                        Notes = sqReader.GetString(5)
                    });
                }
            }
        }

        void InsertRecipeInDatabase(Recipe recipe)
        {
            Recipe dbRecipe = ModifyStringsForDatabase(recipe);

            var sql = string.Format("INSERT INTO Recipes (Name, Type, Origin, Ingredients, Procedure, Notes) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')",
                dbRecipe.Name, dbRecipe.Type, dbRecipe.Origin, dbRecipe.Ingredients, dbRecipe.Procedure, dbRecipe.Notes);

            var command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        void UpdateRecipeInDatabase(Recipe recipe)
        {
            Recipe dbRecipe = ModifyStringsForDatabase(recipe);

            var sql = string.Format("UPDATE Recipes SET Name='{0}', Type='{1}', Origin='{2}', Ingredients='{3}', Procedure='{4}', Notes = '{5}' WHERE Name = '{0}'",
                dbRecipe.Name, dbRecipe.Type, dbRecipe.Origin, dbRecipe.Ingredients, dbRecipe.Procedure, dbRecipe.Notes);

            var command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        void DeleteRecipeInDatabase(Recipe recipe)
        {
            Recipe dbRecipe = ModifyStringsForDatabase(recipe);

            var sql = string.Format("DELETE FROM Recipes WHERE Name = '{0}'",
                dbRecipe.Name);

            var command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        private Recipe ModifyStringsForDatabase(Recipe recipe)
        {
            Recipe dbRecipe = new Recipe();
            dbRecipe.Name = recipe.Name.Replace("'","''");
            dbRecipe.Type = recipe.Type.Replace("'", "''");
            dbRecipe.Origin = recipe.Origin.Replace("'", "''");
            dbRecipe.Ingredients = recipe.Ingredients.Replace("'", "''");
            dbRecipe.Procedure = recipe.Procedure.Replace("'", "''");
            dbRecipe.Notes = recipe.Notes.Replace("'", "''");

            return dbRecipe;
        }
    }
}
